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

        public event Action<int, int> OnGameStart;
        public event Action OnGameOver;
        public event Action OnGameOut;


        private ScoreManager _scoreManager;

        private static GamePlayManager instance;

        private int _expectedMatchCount;
        private GameState _gameState;

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
            _scoreManager = ScoreManager.GetInstance;
        }

        public void StartGame(int row, int column)
        {
            _row = row;
            _col = column;
            _expectedMatchCount = _row * _col / 2;
            _gameState = GameState.Playing;
            _scoreManager.Reset();
            OnGameStart?.Invoke(row, column);
        }

        public void AddMatch()
        {
            _scoreManager.UpdateMatchCount();
        }

        public void AddTurn()
        {
            _scoreManager.UpdateTurnCount();
        }

        public void GameOver()
        {
            _gameState = GameState.GameOver;
            OnGameOver?.Invoke();
        }

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
            }
        }

        public void GameOut()
        {
            OnGameOut?.Invoke();
        }
    }

    public enum GameState
    {
        None,
        Playing,
        GameOver
    }
}