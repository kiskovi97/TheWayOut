
using System;
using UnityEngine;

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
        [SerializeField] private bool[] freeWay;

        [Inject] private SignalBus signalBus;

        internal int Index => InPlace.transform.GetSiblingIndex();

        public bool IsInPlace => InPlace != null;

        internal override void SetInPlace(DragPlacement placement)
        {
            base.SetInPlace(placement);
            if (placement != null)
                signalBus.Fire(new PeacePlacedSignal() { placement = placement, peace = this });
        }

        internal bool IsFreeWay(int index)
        {
            index = (int)Mathf.Repeat(index, freeWay.Length);
            var rotation = transform.rotation.eulerAngles.z / 90;

            if (index >= freeWay.Length)
                return false;

            var realIndex = Mathf.FloorToInt(Mathf.Repeat(index + rotation, 3.9f));

            return freeWay[realIndex];
        }
    }
}
