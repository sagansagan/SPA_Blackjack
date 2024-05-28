import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { useState, useEffect } from 'react';
import Home from './components/Home';
import Blackjack from './components/Blackjack';
import LoginPage from './components/LoginPage';
import Navbar from './components/Navbar';
import AuthRoutes from './components/AuthRoutes';
import Profile from './components/Profile';

function App() {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        checkAuthStatus();
    }, []);

    const checkAuthStatus = () => {
        const token = localStorage.getItem('authToken');
        if (token) {
            setIsAuthenticated(true);
        } else {
            console.log('Token verification failed');
            localStorage.removeItem('authToken');
            setIsAuthenticated(false);
        }
        setLoading(false);
    };

    const handleLogin = () => {
        setIsAuthenticated(true);
    };

    const handleLogout = () => {
        localStorage.removeItem('authToken');
        setIsAuthenticated(false);
    };

    if (loading) {
        return <div>Loading...</div>;
    }

    return (
        <div className="wrapper">
            <Router>
                <Navbar isAuthenticated={isAuthenticated} onLogout={handleLogout} />
                <Routes>
                    <Route path="/" element={<Home isAuthenticated={isAuthenticated} />} />
                    <Route path="/login" element={<LoginPage onLogin={handleLogin} />} />
                    <Route element={<AuthRoutes isAuthenticated={isAuthenticated} />}>
                        <Route path="/blackjack" element={<Blackjack/>} />
                        <Route path="/profile" element={<Profile />} />
                    </Route>
                </Routes>
            </Router>
        </div>
    );
}

export default App;