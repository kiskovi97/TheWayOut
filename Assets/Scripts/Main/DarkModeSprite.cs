using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace TheWayOut.Main
{
    [RequireComponent(typeof(Image))]
    class DarkModeSprite : MonoBehaviour
    {
        public Sprite spriteDark;
        public Sprite spriteLight;

        public Color colorDark = Color.white;
        public Color colorLight = Color.white;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
            if (DarkMode.IsDarkMode)
                image.sprite = spriteDark;
            else
                image.sprite = spriteLight;
            if (DarkMode.IsDarkMode)
                image.color = colorDark;
            else
                image.color = colorLight;
            DarkMode.OnDarkModeSwitch += DarkMode_OnDarkModeSwitch;
        }

        private void OnDestroy()
        {
            DarkMode.OnDarkModeSwitch -= DarkMode_OnDarkModeSwitch;
        }

        private void DarkMode_OnDarkModeSwitch()
        {
            if (DarkMode.IsDarkMode)
                image.sprite = spriteDark;
            else
                image.sprite = spriteLight;
            if (DarkMode.IsDarkMode)
                image.color = colorDark;
            else
                image.color = colorLight;
        }
    }
}
