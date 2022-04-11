
using UnityEngine;

namespace TheWayOut.Gameplay
{
    public class ClearAllButton : SpecialButton
    {
        public override int SpecialOne
        {
            get => StaticSpecialOne;
            set => StaticSpecialOne = value;
        }

        public static int StaticSpecialOne
        {
            get => PlayerPrefs.GetInt(nameof(ClearAllButton), 2);
            set => PlayerPrefs.SetInt(nameof(ClearAllButton), value);
        }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            PeaceGeneration.ClearCurrentPeaces();
        }
    }
}
