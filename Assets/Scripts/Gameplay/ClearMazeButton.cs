
using UnityEngine;

namespace TheWayOut.Gameplay
{
    public class ClearMazeButton : SpecialButton
    {
        public override int SpecialOne
        {
            get => StaticSpecialOne;
            set => StaticSpecialOne = value;
        }

        public static int StaticSpecialOne
        {
            get => PlayerPrefs.GetInt(nameof(ClearMazeButton), 2);
            set => PlayerPrefs.SetInt(nameof(ClearMazeButton), value);
        }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            PeaceGeneration.ClearLevel();
        }
    }
}
