using TheWayOut.Core;

using UnityEngine;
using UnityEngine.UI;

namespace TheWayOut.Gameplay
{
    [RequireComponent(typeof(Button))]
    class SpecialAdd : MonoBehaviour
    {
        protected Button button;
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(ButtonClicked);
        }

        private async void ButtonClicked()
        {
            var success = await AdsInitializer.LoadRewardAd();
            if (success || Application.isEditor)
            {
                ClearAllButton.StaticSpecialOne += 3;
                ClearMazeButton.StaticSpecialOne += 1;
            }
        }
    }
}
