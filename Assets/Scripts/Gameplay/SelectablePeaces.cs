using System.Collections.Generic;

using Kiskovi.Core;

using UnityEngine;

using Zenject;

namespace TheWayOut.Gameplay
{
    internal class SelectablePeaces : DataList<Peace>
    {
        public PuzzlePeace[] peacesPrefabs;
        public Transform goalParent;
        public List<Peace> peaces = new List<Peace>();

        [Inject] private SignalBus signalBus;

        private void OnEnable()
        {
            signalBus.Subscribe<PeacePlacedSignal>(OnPeacePlacedSignal);
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<PeacePlacedSignal>(OnPeacePlacedSignal);
        }

        private void OnPeacePlacedSignal(PeacePlacedSignal signal)
        {
            if (Maze.TryAddPeace(signal.peace))
            {
                signal.peace.transform.localScale = new Vector3(1f, 1f, 1f);
                signal.peace.transform.SetParent(goalParent);
                signal.peace.isDragable = false;
                Maze.CheckPathFinding();
                GenerateNew();
            }
            else
            {
                signal.peace.ReturnToPosition();
            }
        }

        internal void ClearAll()
        {
            peaces.Clear();
            while (peaces.Count < 3)
            {
                peaces.Add(new Peace());
            }
            Clear();
            foreach (var peace in peaces)
                AddItem(peace);
        }

        internal void GenerateNew()
        {
            var randomIndex = Mathf.FloorToInt(UnityEngine.Random.value * peacesPrefabs.Length);
            var selected = peacesPrefabs[randomIndex];
            while (peaces.Count < 3)
            {
                peaces.Add(new Peace());
            }
            foreach (var peace in peaces)
            {
                if (peace.puzzlePeace == null || peace.puzzlePeace.IsInPlace)
                {
                    peace.puzzlePeace = selected;
                    peace.rotation = Random.Range(0, 3) * 90;
                    break;
                }
            }
            Clear();
            foreach(var peace in peaces)
                AddItem(peace);
        }
    }
}
