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
        [SerializeField] Button homeBtn;
        [SerializeField] TMP_Text matchText;
        [SerializeField] TMP_Text turnText;
        [SerializeField] UIScreenBase homeScreen;

        private void OnEnable()
        {
            homeBtn.onClick.AddListener(OnHomeButttonClick);
        }

        private void OnDisable()
        {
            homeBtn.onClick.RemoveListener(OnHomeButttonClick);
        }

        private void OnHomeButttonClick()
        {
            // show confirmation and the exit

            homeScreen.Activate();
            Deactivate();
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
    }
}