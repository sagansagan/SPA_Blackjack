import React, { useState, useEffect } from 'react';
import axios from 'axios';

function Profile() {
    const [profileData, setProfileData] = useState({ gamesPlayed: 0, wins: 0 });

    useEffect(() => {
        fetchProfileData();
    }, []);

    const fetchProfileData = async () => {
        try {
            const token = localStorage.getItem('authToken');
            const response = await axios.get('/api/highscores/userhighscore', {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            setProfileData(response.data);
        } catch (error) {
            console.error('Error fetching profile data:', error);
        }
    };

    return (
        <div className="profile-wrapper">
            <h1>Profile</h1>
            <p>Games Played: {profileData.gamesPlayed}</p>
            <p>Wins: {profileData.wins}</p>
        </div>
    );
}

export default Profile;