
using System;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace TheWayOut.Gameplay
{
    public class PeacePlacedSignal
    {
        public DragPlacement placement;
        public PuzzlePeace peace;
    }
    public class PuzzlePeace : DragObject
    {
        public Image mainImage;
        private PeaceInfo _info;

        public PeaceInfo Info => _info;

        [Inject] private SignalBus signalBus;

        internal int Index => InPlace.transform.GetSiblingIndex();

        public bool IsInPlace => InPlace != null;

        internal override void SetInPlace(DragPlacement placement)
        {
            base.SetInPlace(placement);
            if (placement != null)
                signalBus.Fire(new PeacePlacedSignal() { placement = placement, peace = this });
        }

        internal bool IsFreeWay(PeaceDirection index)
        {
            switch (index)
            {
                case PeaceDirection.Left:
                    return _info.freeLeft;
                case PeaceDirection.Right:
                    return _info.freeRight;
                case PeaceDirection.Up:
                    return _info.freeUp;
                case PeaceDirection.Down:
                    return _info.freeDown;
            }

            return false;
        }

        internal void SetInfo(PeaceInfo peaceInfo)
        {
            if (peaceInfo == null) return;
            _info = peaceInfo;
            mainImage.sprite = peaceInfo.sprite;
        }
    }
}
