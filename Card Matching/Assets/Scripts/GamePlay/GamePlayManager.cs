using System;
using UnityEngine;

namespace CardMatching.GamePlay
{
    public class GamePlayManager : MonoBehaviour
    {
        private int _row;
        private int _col;


        private void OnEnable()
        {
            GameEvent.OnGameStart += OnGameStart;
        }

        private void OnDisable()
        {
            GameEvent.OnGameStart -= OnGameStart;
        }

        private void OnGameStart(int row, int column)
        {
            _row = row;
            _col = column;
        }
    }


    public class GameEvent
    {
        public static Action<int, int> OnGameStart;

        public static Action<GameResult> OnGameOver;

        public static Action GoToHomeScreen;
    }
}