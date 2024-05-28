import React from 'react';
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';

function Navbar({ isAuthenticated, onLogout }) {
    const navigate = useNavigate();

    const handleLogout = async (e) => {
        e.preventDefault(); 
        try {
            const token = localStorage.getItem('authToken');
            await axios.post('/logout', {}, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            localStorage.removeItem('authToken');
            onLogout();
            navigate('/'); 
        } catch (error) {
            console.error('Error logging out:', error);
        }
    };

    console.log('isAuthenticated:', isAuthenticated);

    return (
        <nav>
            <h2 style={{ cursor: 'pointer' }} onClick={() => navigate('/')}>Blackjack Game</h2>
            <ul>
                {!isAuthenticated ? (
                    <li>
                        <Link to="/login">Login</Link>
                    </li>
                ) : (
                    <>
                        <li>
                            <Link to="/profile">Profile</Link>
                        </li>
                        <li>
                            <Link to="/blackjack">Play</Link>
                        </li>
                        <li>
                            <a href="/" onClick={handleLogout}>Logout</a>
                        </li>
                    </>
                )}
            </ul>
        </nav>
    );
}

export default Navbar;