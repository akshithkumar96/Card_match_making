using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CardMatching.GamePlay;

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

        [SerializeField] GameAssetScriptableObject gameAssets;

        private void Awake()
        {
            Intialize();
        }

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
            (var row, var column) = ExtractBothNumbers(layoutDropDown.options[layoutDropDown.value].text);
            GameEvent.OnGameStart?.Invoke(row, column);
            //Do play
        }

        private void Intialize()
        {
            layoutDropDown.ClearOptions();

            layoutDropDown.AddOptions(gameAssets.gridLevel);
        }

        private void OnDropdownValueChange(int value)
        {
            //Do something
        }

        public static (int, int) ExtractBothNumbers(string input)
        {
            Debug.Log(input);
            string[] parts = input.Split('*');
            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int firstNumber) &&
                int.TryParse(parts[1], out int secondNumber))
            {
                return (firstNumber, secondNumber);
            }

            return (0, 0);
        }
    }
}