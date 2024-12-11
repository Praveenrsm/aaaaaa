using FarmTradeEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FarmTradeDataLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        FarmContext _farmcontext;
        public UserRepository(FarmContext context)
        {
            _farmcontext = context;
        }

        public void Adduser(User user)
        {
            #region ADD ADMIN,SUPPLIER,USER
            user.password = Hash(user.password);
            _farmcontext.users.Add(user);
            _farmcontext.SaveChanges();
            #endregion
        }
        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public string Login(User users)
        {
            #region LOGIN 
            List<User> list = new List<User>();
            list = _farmcontext.users.ToList();
            foreach (var user in list)
            {
                if (user.Email == users.Email)
                {
                    if (BCrypt.Net.BCrypt.Verify(users.password, user.password))
                    {
                        string role = user.role;
                        return role;
                    }
                }
            }
            return "Invalid";
            #endregion

        }
        public void Updateuser(User user)
        {
            #region EDIT PROFILE AFTER LOGIN 
            _farmcontext.Entry(user).State = EntityState.Modified;
            _farmcontext.SaveChanges();
            #endregion
        }

        public User GetUserById(Guid userId)
        {
            #region GET UNIQUE PROFILE 
            var result = _farmcontext.users.ToList();
            var user = result.Where(obj => obj.UserId == userId).FirstOrDefault();
            return user;
            #endregion

        }

        public IEnumerable<User> Getusers(int pageNumber,int pageSize)
        {
            #region GET All USER
            return _farmcontext.users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            #endregion
        }
        public void DeleteUser(Guid userId)
        {
            #region DELETE USER
            var user = _farmcontext.users.Find(userId);
            _farmcontext.users.Remove(user);
            _farmcontext.SaveChanges();
            #endregion
        }
    }
}
