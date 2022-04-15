using TheWayOut.Main;
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

        private void ButtonClicked()
        {
            AdsShower.LoadAd(OnFinished);
        }

        private void OnFinished(bool obj)
        {
            if (obj)
            {
                ClearAllButton.StaticSpecialOne += 3;
                ClearMazeButton.StaticSpecialOne += 1;
            }
        }
    }
}
