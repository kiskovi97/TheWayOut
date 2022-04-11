using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TheWayOut.Gameplay
{
    class Specials : MonoBehaviour
    {
        [SerializeField]
        private SpecialButton[] specialButtons;
        [SerializeField]
        private Transform specialsParent;

        private void Awake()
        {
            foreach (var button in specialButtons)
            {
                var btn = Instantiate(button, specialsParent);
                btn.Initialize();
            }
        }
    }
}
