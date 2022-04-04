
using System;

namespace TheWayOut.Gameplay
{
    class PuzzlePeace : DragObject
    {
        internal Action<PuzzlePeace, DragPlacement> OnPeacePlaced;

        internal override void SetInPlace(DragPlacement placement)
        {
            base.SetInPlace(placement);
            if (placement != null)
                OnPeacePlaced?.Invoke(this, placement);
        }
    }
}
