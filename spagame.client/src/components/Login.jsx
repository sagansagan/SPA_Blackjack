import { useState } from 'react';
import axios from 'axios';

function Login(props) {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    
    const onLogin = async (e) => {
        e.preventDefault();

        try {
        
            const response = await axios.post('/login', { email, password });

            // Om inloggningen är framgångsrik, navigera till spel-sidan
            console.log('Login successful');
            const token = response.data.accessToken;
            localStorage.setItem('authToken', token);
            window.location.href = "/blackjack";
        } catch (error) {
            console.error('Login failed:', error);
        }
    };


    return (
        <section className='auth-form-container'>
        <div>
            <h2>Login</h2>
            <form className='login-form' onSubmit={onLogin}>
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
                <button type="submit">Login</button>
            </form>
            <p>
                Don't have an account? 
                <button className='switch-button' onClick={() => props.onFormSwitch('register')}>Register</button>
            </p>
        </div>
        </section>
    );


}

export default Login;