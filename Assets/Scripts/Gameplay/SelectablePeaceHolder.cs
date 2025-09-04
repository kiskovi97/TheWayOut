using Kiskovi.Core;

using UnityEngine;

using Zenject;

namespace TheWayOut.Gameplay
{
    public class Peace : IData
    {
        public PeaceInfo peaceInfo;
    }
    internal class SelectablePeaceHolder : DataHolder<Peace>
    {
        public Transform peaceParent;
        public PuzzlePeace peacesPrefab;

        [Inject] private DiContainer container;

        private PuzzlePeace prevPeace;

        public override void SetData(IData itemData)
        {
            base.SetData(itemData);

            if (Data == null)
            {
                if (prevPeace != null)
                    prevPeace = null;
                return;
            }

            if (prevPeace == null || prevPeace.transform.parent != peaceParent)
            {
                prevPeace = container.InstantiatePrefabForComponent<PuzzlePeace>(peacesPrefab, peaceParent);
            }

            prevPeace.SetInfo(Data.peaceInfo);
        }
    }
}
