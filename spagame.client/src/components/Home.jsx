import { useEffect, useState } from 'react';
import axios from 'axios';


function Home({ isAuthenticated}) { 
    const [topWins, setTopWins] = useState([]);
    const [topGamesPlayed, setTopGamesPlayed] = useState([]);

    useEffect(() => {
        fetchHighScores();
    }, []);

    const fetchHighScores = async () => {
        try {
            const winsResponse = await axios.get('api/highscores/topwins');
            setTopWins(winsResponse.data);

            const gamesPlayedResponse = await axios.get('api/highscores/topgamesplayed');
            setTopGamesPlayed(gamesPlayedResponse.data);
        } catch (error) {
            console.error('Error fetching high scores:', error);
        }
    };

    return (
        <div className='home-wrapper'>
            <div>
                {!isAuthenticated && (
                    <>
                        <h1>Welcome</h1>
                        <h2>Login or register to start a blackjackgame!</h2>
                    </>
                )}
            </div>
            <div className='home-content'>
                <div>
                    <h1>Highscore List</h1>
                    <h2>Top 3 Wins:</h2>
                    <ol>
                        {topWins.map((score, index) => (
                            <li key={index}>
                                {score.user.userName}: {score.wins} wins
                            </li>
                        ))}
                    </ol>
                    <h2>Top 3 Games Played:</h2>
                    <ol>
                        {topGamesPlayed.map((score, index) => (
                            <li key={index}>
                                {score.user.userName}: {score.gamesPlayed} games played
                            </li>
                        ))}
                    </ol>
                </div>
            </div>
        </div>
    );


}

export default Home;