using Kiskovi.Core;

using UnityEngine;

using Zenject;

namespace TheWayOut.Gameplay
{
    public class Peace : IData
    {
        public PuzzlePeace puzzlePeace;
        public float rotation;
    }
    internal class SelectablePeaceHolder : DataHolder<Peace>
    {
        public Transform peaceParent;

        [Inject] private DiContainer container;

        public override void SetData(IData itemData)
        {
            base.SetData(itemData);

            if (Data == null || Data.puzzlePeace == null)
            {
                foreach (Transform child in peaceParent)
                    Destroy(child.gameObject);
                return;
            }

            if (Data.puzzlePeace.transform.parent != peaceParent)
            {
                foreach (Transform child in peaceParent)
                    Destroy(child.gameObject);
                Data.puzzlePeace = container.InstantiatePrefabForComponent<PuzzlePeace>(Data.puzzlePeace, peaceParent);
            }

            Data.puzzlePeace.transform.rotation = Quaternion.Euler(0f, 0f, Data.rotation);
        }
    }
}
