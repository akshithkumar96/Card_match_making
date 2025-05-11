using CardMatching.GamePlay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatching.UI
{
    /// <summary>
    /// gameplay screen script
    /// </summary>
    public class GamePlayScreen : UIScreenBase
    {
        #region serializeFields
        [SerializeField] Button homeBtn;
        [SerializeField] TMP_Text matchText;
        [SerializeField] TMP_Text turnText;
        [SerializeField] UIScreenBase homeScreen;
        [SerializeField] UIScreenBase gameoverScreen;
        #endregion

        //manager reference
        private ScoreManager _scoreManager;
        private GamePlayManager _gameplayManager;

        //refernce for grid level
        private int _row, _column;

        #region Unity Callback
        private void Awake()
        {
            _scoreManager = ScoreManager.GetInstance;
            _gameplayManager = GamePlayManager.GetInstance;
        }

        private void OnEnable()
        {
            homeBtn.onClick.AddListener(OnHomeButttonClick);
            _scoreManager.OnTurnUpdate += UpdateTurn;
            _scoreManager.OnMatchUpdate += UpdateMatch;
            _gameplayManager.OnGameOver += GoToGameOverScreen;
            _gameplayManager.OnGameStart += OnGameStart;
        }

        private void OnDisable()
        {
            homeBtn.onClick.RemoveListener(OnHomeButttonClick);
            _scoreManager.OnTurnUpdate -= UpdateTurn;
            _scoreManager.OnMatchUpdate -= UpdateMatch;
            _gameplayManager.OnGameOver -= GoToGameOverScreen;
            _gameplayManager.OnGameStart -= OnGameStart;
        }

        #endregion

        /// <summary>
        /// Resets 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colurm"></param>
        private void OnGameStart(int row, int colurm)
        {
            _row = row;
            _column = colurm;
            //Reset score
            UpdateMatch(0);
            UpdateTurn(0);
        }

        private void OnHomeButttonClick()
        {
            // show confirmation and the exit

            homeScreen.Activate();
            Deactivate();
            _gameplayManager.GameOut();
        }

        /// <summary>
        /// Updated the turn value
        /// </summary>
        /// <param name="value"></param>
        private void UpdateTurn(int value)
        {
            turnText.text = value.ToString();
        }

        /// <summary>
        /// Update the match value
        /// </summary>
        /// <param name="value"></param>
        private void UpdateMatch(int value)
        {
            matchText.text = value.ToString();
        }

        /// <summary>
        /// On Game over go to game over screen
        /// </summary>
        private void GoToGameOverScreen()
        {
            var gameplayScreen = (GameoverScreen)gameoverScreen;
            gameplayScreen.ShowResults(new GameResult()
            {
                matchCount = _scoreManager.GetMatchCount,
                turnCount = _scoreManager.GetTurnCount,
            });
            gameoverScreen.Activate();
            Deactivate();
        }
    }
}