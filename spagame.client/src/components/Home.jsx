import { useEffect, useState } from 'react';
import Login from './Login';
import Register from './Register';

function Home() {
    const [currentForm, setCurrentForm] = useState('login'); 

    const toggleform = (f) => {
        setCurrentForm(f);
    }

    return (
        <div className='home'>
            <header>
                <h1>Welcome</h1>
            </header>
            {currentForm == 'login' ? <Login onFormSwitch={toggleform}/> : <Register onFormSwitch={toggleform}/>}

        </div>
    );


}

export default Home;