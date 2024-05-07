import { useEffect, useState } from 'react';
import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './components/Login';

function App() {
    
    //const [token, setToken] = useState();

    //if(!token) {
    //  return <Login setToken={setToken} />
    //}
    return (
        <div className="wrapper">
        <h1>Application</h1>
        <Router>
          <Routes>
          <Route path="/login" element={<Login/>} />
          </Routes>
        </Router>
      </div>
    );
    
    
}

export default App;