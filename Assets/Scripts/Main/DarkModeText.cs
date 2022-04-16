using UnityEngine;
using UnityEngine.UI;

namespace TheWayOut.Main
{
    [RequireComponent(typeof(Text))]
    class DarkModeText : MonoBehaviour
    {
        public Color colorDark = Color.white;
        public Color colorLight = Color.white;

        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
            if (DarkMode.IsDarkMode)
                text.color = colorDark;
            else
                text.color = colorLight;
            DarkMode.OnDarkModeSwitch += DarkMode_OnDarkModeSwitch;
        }

        private void OnDestroy()
        {
            DarkMode.OnDarkModeSwitch -= DarkMode_OnDarkModeSwitch;
        }

        private void DarkMode_OnDarkModeSwitch()
        {
            if (DarkMode.IsDarkMode)
                text.color = colorDark;
            else
                text.color = colorLight;
        }
    }
}
