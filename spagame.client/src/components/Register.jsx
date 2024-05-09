import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';


function Register(props) {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    
    const handleRegister = async (e) => {
        e.preventDefault();
    
        try {
            // Anropa API för att registrera användaren
            const response = await axios.post('/register', { email, password });
    
            // Om registreringen är framgångsrik, navigera till app-sidan
            console.log('Registration successful')
        } catch (error) {
            console.error('Registration failed:', error);
        }
    };

    
    return (
        <div>
            <h2>Register</h2>
            <form onSubmit={handleRegister}>
                <div>
                    <label>Email:</label>
                    <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        />
                </div>
                    <div>
                        <label>Password:</label>
                        <input
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                          />
                    </div>
                    <button type="submit">Register</button>
                </form>
                <button onClick={() => props.onFormSwitch('login')}>Login</button>
                
        </div>
    );
    



}

export default Register;