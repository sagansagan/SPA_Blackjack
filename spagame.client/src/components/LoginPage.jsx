import React, { useState } from 'react';
import Login from './Login';
import Register from './Register';

function LoginPage({ onLogin }) {
    const [currentForm, setCurrentForm] = useState('login');

    const toggleForm = (form) => {
        setCurrentForm(form);
    };

    return (
        <div className='login-page'>
            {currentForm === 'login' ? (
                <Login onFormSwitch={toggleForm} onLogin={onLogin} />
            ) : (
                <Register onFormSwitch={toggleForm} />
            )}
        </div>
    );
}

export default LoginPage;