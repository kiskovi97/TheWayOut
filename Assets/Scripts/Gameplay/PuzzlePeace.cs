﻿
using System;
using UnityEngine;

namespace TheWayOut.Gameplay
{
    class PuzzlePeace : DragObject
    {
        internal Action<PuzzlePeace, DragPlacement> OnPeacePlaced;

        [SerializeField] private bool[] freeWay;

        internal override void SetInPlace(DragPlacement placement)
        {
            base.SetInPlace(placement);
            if (placement != null)
                OnPeacePlaced?.Invoke(this, placement);
        }

        internal bool IsFreeWay(int index)
        {
            var rotation = transform.rotation.eulerAngles.z / 90;

            if (index >= freeWay.Length)
                return false;

            var realIndex = Mathf.FloorToInt(Mathf.Repeat(index + rotation, 3.9f));

            return freeWay[realIndex];
        }
    }
}
