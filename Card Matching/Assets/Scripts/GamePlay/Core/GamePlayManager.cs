using System;
using UnityEngine;

namespace CardMatching.GamePlay
{
    public class GamePlayManager
    {
        /// <summary>
        /// store game details
        /// </summary>
        private int _row;
        private int _col;

        /// <summary>
        /// Game start Event
        /// </summary>
        public event Action<int, int> OnGameStart;
        /// <summary>
        /// Game over event
        /// </summary>
        public event Action OnGameOver;
        /// <summary>
        /// Game out event 
        /// when you quit without completing the game
        /// </summary>
        public event Action OnGameOut;

        //Score manager for updating score
        private ScoreManager _scoreManager;
        //_total match count to finish the game
        private int _expectedMatchCount;
        //Game state
        private GameState _gameState;

        //Singleton instance
        private static GamePlayManager instance;

        //Property to get the instance
        public static GamePlayManager GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GamePlayManager();
                }
                return instance;
            }
        }

        private GamePlayManager()
        {
            //Inject score manager
            _scoreManager = ScoreManager.GetInstance;
        }

        /// <summary>
        /// start the game 
        /// </summary>
        /// <param name="row">number of rows</param>
        /// <param name="column"> number of column in the game</param>
        public void StartGame(int row, int column)
        {
            _row = row;
            _col = column;
            //get expected match count
            _expectedMatchCount = _row * _col / 2;
            _gameState = GameState.Playing;
            _scoreManager.Reset();
            OnGameStart?.Invoke(row, column);
        }

        /// <summary>
        /// increment the match after successful event
        /// </summary>
        public void AddMatch()
        {
            _scoreManager.UpdateMatchCount();
        }

        /// <summary>
        /// Update the turn after each event
        /// </summary>
        public void AddTurn()
        {
            _scoreManager.UpdateTurnCount();
        }

        /// <summary>
        /// Game over after matching all
        /// </summary>
        public void GameOver()
        {
            _gameState = GameState.GameOver;
            OnGameOver?.Invoke();
        }

        /// <summary>
        /// check for game over 
        /// </summary>
        /// <returns></returns>
        public bool CheckGameOver()
        {
            if (IsGameOver())
            {
                GameOver();
                return true;
            }
            return false;
        }

        public bool IsGameOver()
        {
            return _scoreManager.GetMatchCount == _expectedMatchCount;
        }

        public void Replay()
        {
            if (_row > 0 && _col > 0)
            {
                StartGame(_row, _col);
            }
            else
            {
                //GotoHome screen
                Debug.Log("Error while replaying select Level");
            }
        }

        /// <summary>
        /// On closing game in mid
        /// </summary>
        public void GameOut()
        {
            OnGameOut?.Invoke();
        }
    }

    /// <summary>
    /// Game state types
    /// </summary>
    public enum GameState
    {
        None,
        Playing,
        GameOver
    }
}