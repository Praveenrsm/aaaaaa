import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loginFailed, setLoginFailed] = useState(false);
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.post('http://localhost:5274/api/Account/login', {
                email,
                password
            });
// Log the entire response to see its structure
console.log('Login response:', response.data);

const { role, token } = response.data; // Ensure the token is retrieved from the response

// Log the token to check its value
console.log('Token received:', token);

// Save the token in localStorage or sessionStorage
localStorage.setItem('token', token);
            if (role === 'admin') {
                navigate('/ForgotPassword'); 
            } else if (role === 'supplier') {
                navigate('/supplier'); 
            } else {
                setLoginFailed(true); 
            }
        } catch (error) {
            setLoginFailed(true); 
        }
    };

    return (
        <div>
            <h2>Login</h2>
            <form onSubmit={handleLogin}>
                <input
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                />
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                />
                <button type="submit">Login</button>
            </form>
            {loginFailed && <p style={{ color: 'red' }}>Login failed. Invalid credentials.</p>}
        </div>
    );
};

export default Login;
