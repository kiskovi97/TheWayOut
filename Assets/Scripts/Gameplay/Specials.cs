using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace TheWayOut.Gameplay
{
    class Specials : MonoBehaviour
    {
        public SpecialButton[] specialButtons;

        private void Awake()
        {
            foreach (var button in specialButtons)
            {
                button.Initialize();
            }
        }
    }
}
