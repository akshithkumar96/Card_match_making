using CardMatching.GamePlay;
using CardMatching.Souds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatching.UI
{
    /// <summary>
    /// GameOver screen script
    /// </summary>
    public class GameoverScreen : UIScreenBase
    {
        #region Serialize feilds
        [SerializeField] Button replayBtn;
        [SerializeField] Button homeBtn;
        [SerializeField] UIScreenBase gameplayScreen;
        [SerializeField] UIScreenBase homeScreen;
        [SerializeField] TMP_Text turnText;
        [SerializeField] TMP_Text matchText;
        [SerializeField] TMP_Text resultText;
        #endregion


        private GamePlayManager _gamePlayManager;
        private SoundManager _soundManager;

        #region Constants
        private const string GAME_OVER = " GAME OVER";
        private const string VICTORY = " VICTORY";

        #endregion

        private void Awake()
        {
            _gamePlayManager = GamePlayManager.GetInstance;
        }

        private void OnEnable()
        {
            replayBtn.onClick.AddListener(OnReplayBtnClick);
            homeBtn.onClick.AddListener(OnHomeBtnClick);
        }

        private void OnDisable()
        {
            replayBtn.onClick.RemoveListener(OnReplayBtnClick);
            homeBtn.onClick.RemoveListener(OnHomeBtnClick);
        }

        private void OnHomeBtnClick()
        {
            homeScreen.Activate();
            Deactivate();
        }

        private void OnReplayBtnClick()
        {
            gameplayScreen.Activate();
            Deactivate();
            _gamePlayManager.Replay();
        }

        /// <summary>
        /// To show results
        /// </summary>
        /// <param name="result"></param>
        public void ShowResults(GameResult result)
        {
            matchText.text = result.matchCount.ToString();
            turnText.text = result.turnCount.ToString();
            resultText.text = VictoryValidation(result) ? VICTORY : GAME_OVER;
            SoundManager.GetInstance.Play(Souds.AudioType.GameOver);
        }


        /// <summary>
        /// Victory condition 
        /// TODO: change condition based on requirement
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool VictoryValidation(GameResult result)
        {
            if (result.matchCount == 0)
            {
                return false;
            }
            if (result.turnCount==result.matchCount)
            {
                return true;
            }
            return false;
        }
    }
}