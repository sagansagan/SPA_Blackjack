import { useEffect, useState } from 'react';
import axios from 'axios';

function Login(props) {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    
    const handleLogin = async (e) => {
        e.preventDefault();

        try {
        
            const response = await axios.post('/login', { email, password });

            // Om inloggningen är framgångsrik, navigera till app-sidan
            console.log('Login successful');
        } catch (error) {
            console.error('Login failed:', error);
        }
    };


    return (
        <section className='login-page-wrapper'>
        <div>
            <h2>Login</h2>
            <form onSubmit={handleLogin}>
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
                <button onClick={() => props.onFormSwitch('register')}>Register</button>
            </p>
        </div>
        </section>
    );


}

export default Login;