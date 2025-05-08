using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatching.UI
{
    /// <summary>
    /// Home screen screen Item
    /// </summary>
    public class HomeScreen : UIScreenBase
    {
        [SerializeField] Button playBtn;

        [SerializeField] TMP_Dropdown layoutDropDown;

        [SerializeField] UIScreenBase gamePlayScreen;

        private int layoutId;

        private void OnEnable()
        {
            playBtn.onClick.AddListener(OnPlayButtonClick);
            layoutDropDown.onValueChanged.AddListener(OnDropdownValueChange);
        }

        private void OnDisable()
        {
            playBtn.onClick.RemoveListener(OnPlayButtonClick);
            layoutDropDown.onValueChanged.RemoveListener(OnDropdownValueChange);
        }

        private void OnPlayButtonClick()
        {
            gamePlayScreen.Activate();
            Deactivate();
            //Do play
        }

        private void OnDropdownValueChange(int value)
        {

        }
    }
}