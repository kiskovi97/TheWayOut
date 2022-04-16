using System;
using UnityEngine;

namespace TheWayOut.Main
{
    public class DarkMode : MonoBehaviour
    {
        public static bool IsDarkMode => DarkModeInt == 1;

        public static event Action OnDarkModeSwitch;

        private static int DarkModeInt
        {
            get => PlayerPrefs.GetInt(nameof(DarkModeInt), 1);
            set => PlayerPrefs.SetInt(nameof(DarkModeInt), value);
        }

        public void SwitchDarkMode()
        {
            _SwitchDarkMode();
        }

        public static void _SwitchDarkMode()
        {
            if (DarkModeInt == 1)
                DarkModeInt = 0;
            else
                DarkModeInt = 1;
            OnDarkModeSwitch?.Invoke();
        }
    }
}
