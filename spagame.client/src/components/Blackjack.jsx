import { useEffect, useState } from 'react';
import axios from 'axios';

function Blackjack() {

    const [game, setGame] = useState(null);
    const [inProgressGames, setInProgressGames] = useState([]);

    useEffect(() => {
        fetchInProgressGames();
    }, []);

    const fetchInProgressGames = async () => {
        try {
            const token = localStorage.getItem('authToken');
            if (!token) {
                throw new Error('Ingen autentiseringstoken funnen.');
            }
            const response = await axios.get('api/blackjack/inprogress', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setInProgressGames(response.data);
        } catch (error) {
            console.error('Error fetching in-progress games:', error);
        }
    };


    const startGame = async () => {
        try {
            const token = localStorage.getItem('authToken');
            if (!token) {
              throw new Error('Ingen autentiseringstoken funnen.');
            }
            const response = await axios.post('api/blackjack/start', {}, {
                headers: {
                  Authorization: `Bearer ${token}`,
                  "Content-Type" : "application/json",
                  "Accept": "application/json"
                }
              });
            setGame(response.data);
            fetchInProgressGames();
            console.log('game started:', response.data);
          } catch (error) {
            console.error('Error starting game:', error);
          }
      };

      const continueGame = async (gameId) => {
        try {
            const token = localStorage.getItem('authToken');
            if (!token) {
                throw new Error('Ingen autentiseringstoken funnen.');
            }
            const response = await axios.get(`api/blackjack/${gameId}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setGame(response.data);
            console.log('Continuing game:', response.data);
        } catch (error) {
            console.error('Error continuing game:', error);
        }
    };

      const handleAction = async (action) => {
        if (!game) return;
        try {
            const token = localStorage.getItem('authToken');
            if (!token) {
                throw new Error('Ingen autentiseringstoken funnen.');
            }
            const response = await axios.post(`api/blackjack/${action}/${game.id}`, {}, {
                headers: {
                    Authorization: `Bearer ${token}`,
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            });
            setGame(response.data);
            if (response.data.status !== 'In Progress') {
                fetchInProgressGames();
            }
            console.log(response.data);
        } catch (error) {
            console.error(`Error performing action: ${action}`, error);
        }
    };
    
    const getCardImageUrl = (card) => {
        const cardImagePath = `/cards/${card.rank}-${card.suit}.png`;
        return cardImagePath;
    };

    return (
    <div className='game-wrapper'>
        <div>
        <h2>Blackjack Game</h2>
        {(!game || game.status !== 'In Progress') && (
                <div>
                    <button onClick={startGame}>Start New Game</button>
                    {inProgressGames.length > 0 && (
                        <div>
                            <h3>Continue Game:</h3>
                            <ul>
                                {inProgressGames.map((inProgressGame) => (
                                    <li key={inProgressGame.id}>
                                        <button className='continue-button' onClick={() => continueGame(inProgressGame.id)}>
                                            Game ID: {inProgressGame.id}
                                        </button>
                                    </li>
                                ))}
                            </ul>
                        </div>
                    )}
                </div>
            )}
            {game && game.playerHand && game.dealerHand && (
                    <div>
                        <h3>Dealer's Hand:</h3>
                        <div>
                            {game.dealerHand.cards.map((card, index) => (
                                <img
                                    key={index}
                                    src={getCardImageUrl(card)}
                                    alt={`${card.rank} of ${card.suit}`}
                                    style={{ width: '100px', marginRight: '10px' }}
                                />
                            ))}
                        </div>
                        <h3>Player's Hand:</h3>
                        <div>
                            {game.playerHand.cards.map((card, index) => (
                                <img
                                    key={index}
                                    src={getCardImageUrl(card)}
                                    alt={`${card.rank} of ${card.suit}`}
                                    style={{ width: '100px', marginRight: '10px' }}
                                />
                            ))}
                        </div>
                        {game.status !== 'In Progress' && (
                            <h2>{game.status}!</h2>
                        )}
                        {game.status === 'In Progress' && (
                            <div>
                                <button onClick={() => handleAction('hit')}>Hit</button>
                                <button onClick={() => handleAction('stand')}>Stand</button>
                            </div>
                        )}
                    </div>
                )}
    </div>
    </div>
    );


}

export default Blackjack;