import React, { useState } from 'react';
import axios from 'axios';

const ForgotPassword = () => {
  // State variables to manage form input and responses
  const [email, setEmail] = useState('');
  const [message, setMessage] = useState('');
  const [error, setError] = useState('');

  // Handle email input change
  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  // Handle form submission (forgot password)
  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage('');
    setError('');

    try {
      // Make the POST request to the backend API
      const response = await axios.post('http://localhost:5274/api/ForgotPassword/forgot-password', email, {
        headers: {
          'Content-Type': 'application/json',
        },
      });

      // Handle success response
      if (response.status === 200) {
        setMessage(response.data);  // Success message from API
      }
    } catch (err) {
      // Handle error response
      if (err.response && err.response.data) {
        setError(err.response.data);
      } else {
        setError('An unexpected error occurred. Please try again later.');
      }
    }
  };

  return (
    <div className="forgot-password-container">
      <h2>Forgot Password</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="email">Enter your email address:</label>
          <input
            type="email"
            id="email"
            value={email}
            onChange={handleEmailChange}
            required
            placeholder="Email address"
          />
        </div>
        <button type="submit">Send Reset Link</button>
      </form>

      {/* Display success message */}
      {message && <p style={{ color: 'green' }}>{message}</p>}

      {/* Display error message */}
      {error && <p style={{ color: 'red' }}>{error}</p>}
    </div>
  );
};

export default ForgotPassword;
