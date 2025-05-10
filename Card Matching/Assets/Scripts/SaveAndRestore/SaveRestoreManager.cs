using CardMatching.GamePlay;
using CardMatching.UserData;
using UnityEngine;

namespace CardMatching.Saverestore
{
    /// <summary>
    /// to Manager saving and restoring user data
    /// </summary>
    public class SaveRestoreManager : MonoBehaviour
    {
        private const string SAVE_FILE_NAME = "userData";

        //System for saving and restoring
        private ISaveSystem _saveSystem;
        private IRestoreSystem _restoreSystem;

        //manager instance
        private GamePlayManager _gameplayManager;
        private ScoreManager _scoreManager;

        private UserLevel _userLevel;

        private void Awake()
        {
            // save path where to store the user data
            var savePath = Application.persistentDataPath;
            _saveSystem = new SaveSystem(savePath);
            _restoreSystem = new RestoreSystem(savePath);

            _userLevel = _restoreSystem.Load<UserLevel>(SAVE_FILE_NAME);

            if (_userLevel == null)
            {
                _userLevel = new UserLevel();
                _userLevel.GamePlayed = 0;
                _userLevel.HighScore = 0;
                _saveSystem.Save<UserLevel>(_userLevel, SAVE_FILE_NAME);
            }

            _gameplayManager = GamePlayManager.GetInstance;
            _scoreManager = ScoreManager.GetInstance;
        }

        private void OnEnable()
        {
            _gameplayManager.OnGameOver += UpdateUserLevel;
        }

        private void OnDisable()
        {
            _gameplayManager.OnGameOver -= UpdateUserLevel;
            //save the state before closing the application
            _saveSystem.Save<UserLevel>(_userLevel, SAVE_FILE_NAME);
        }

        /// <summary>
        /// Update the user level data after each game
        /// </summary>
        /// <param name="score"></param>
        private void UpdateUserLevel()
        {
            var score = _scoreManager.GetMatchCount;
            if (_userLevel.HighScore < score)
            {
                _userLevel.HighScore = score;
            }
            _userLevel.GamePlayed++;
        }
    }
}