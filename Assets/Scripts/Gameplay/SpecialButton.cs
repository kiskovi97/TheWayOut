using System;
using UnityEngine;
using UnityEngine.UI;

namespace TheWayOut.Gameplay
{
    [RequireComponent(typeof(Button))]
    public abstract class SpecialButton : MonoBehaviour
    {
        [SerializeField] private Text countText;

        protected Button button;

        public abstract int SpecialOne { get; set; }

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(ButtonClicked);
        }

        internal virtual void Initialize()
        {
            countText.text = SpecialOne.ToString();
            button.interactable = SpecialOne > 0;
        }

        protected virtual void ButtonClicked()
        {
            SpecialOne -= 1;
            Initialize();
        }

        internal bool HasAny()
        {
            return SpecialOne > 0;
        }

        private void Update()
        {
            Initialize();
        }
    }
}
