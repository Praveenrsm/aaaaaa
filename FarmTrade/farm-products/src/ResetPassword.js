import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate, useLocation } from 'react-router-dom';

const ResetPassword = () => {
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [message, setMessage] = useState('');
  const navigate = useNavigate();
  const location = useLocation();

  // Get the token from the URL query parameters
  const queryParams = new URLSearchParams(location.search);
  const token = queryParams.get('token');

  const handleResetPassword = async (e) => {
    e.preventDefault();
    if (newPassword !== confirmPassword) {
      setMessage("Passwords don't match!");
      return;
    }
  
    try {
      const response = await axios.post(
        `http://localhost:5274/api/ForgotPassword/reset-password?token=${token}`,
        newPassword,  // Send plain string
        {
          headers: {
            'Content-Type': 'application/json', // Ensure content type is JSON
          },
        }
      );
      setMessage('Password reset successful!');
      navigate(''); // Redirect to login page after successful reset
    } catch (error) {
      setMessage('Failed to reset password. The link might be expired or invalid.');
    }
  };
  
  

  return (
    <div>
      <h2>Reset Password</h2>
      <form onSubmit={handleResetPassword}>
        <input
          type="password"
          value={newPassword}
          onChange={(e) => setNewPassword(e.target.value)}
          placeholder="Enter new password"
          required
        />
        <input
          type="password"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
          placeholder="Confirm new password"
          required
        />
        <button type="submit">Reset Password</button>
      </form>
      {message && <p>{message}</p>}
    </div>
  );
};

export default ResetPassword;
