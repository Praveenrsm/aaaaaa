using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using FarmTradeDataLayer;
using FarmTradeEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FarmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly FarmContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        public ForgotPasswordController(FarmContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        // Forgot Password Endpoint
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var user = _context.users.FirstOrDefault(u => u.Email == email);
            if (user == null) return BadRequest("Email does not exist.");

            // Generate a reset token
            user.ResetToken = GenerateToken();
            user.ResetTokenExpiry = DateTime.Now.AddHours(2);
            await _context.SaveChangesAsync();

            // Send reset email
            var resetUrl = $"http://localhost:3000/ResetPassword?token={user.ResetToken}";
            SendEmail(email, "Password Reset", $"Please reset your password using this link: {resetUrl}");
            //new { ResetToken = user.ResetToken }
            return Ok("password send successfully");
        }

        private string GenerateToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var tokenBytes = new byte[32];
                rng.GetBytes(tokenBytes);
                return Convert.ToBase64String(tokenBytes);
            }
        }
       
        private void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("praveenrsm1234@gmail.com", "nfgf tcbp fyrc ewsq"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("praveenrsm1234@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] string newPassword, [FromQuery] string token)
        {
            var user = _context.users.FirstOrDefault(u => u.ResetToken == token);
            if (user == null || user.ResetTokenExpiry < DateTime.Now)
            {
                return BadRequest("Invalid or expired token.");
            }
            else if (user.password == newPassword)
            {
                return BadRequest("Old and New password are same");
            }
            else if (user.ResetToken == null)
            {
                return BadRequest("your password already password reset");
            }
            else {
                // Hash the new password
                //user.password = _passwordHasher.HashPassword(user, newPassword);
                user.password = BCrypt.Net.BCrypt.HashPassword(newPassword);

                // Clear the reset token and expiry
                user.ResetToken = null;
                user.ResetTokenExpiry = null;

                await _context.SaveChangesAsync();

                return Ok("Password has been reset successfully.");
                }
        }


    }
}