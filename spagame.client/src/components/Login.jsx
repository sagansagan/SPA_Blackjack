import { useEffect, useState } from 'react';

function Login() {

    useEffect(() => {
        const user = localStorage.getItem("user");
        if (user) {
            document.location = "/";
        }
    }, []);


    return (
        <section className='login-page-wrapper'>
            <div className='login-page'>
                <header>
                    <h1>Login Page</h1>
                </header>
                <p className='message'></p>
                <div className='form-holder'>
                    <form action="#" className='login'>
                        <label htmlFor="email">Email</label>
                        <br />
                        <input type="email" name='Email' id='email' required />
                        <br />
                        <label htmlFor="password">Password</label>
                        <br />
                        <input type="password" name='Password' id='password' required />
                        <br />
                        <br />
                        <input type="submit" value="Login" className='login btn' />
                    </form>
                </div>
                <div className='my-5'>
                    <span>Or </span>
                    <a href="/register">Register</a>
                </div>
            </div>
        </section>
    );


}

export default Login;