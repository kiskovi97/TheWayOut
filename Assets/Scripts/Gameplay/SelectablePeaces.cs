using System;
using System.Collections.Generic;
using System.Linq;

using Kiskovi.Core;

using UnityEngine;

using Zenject;

namespace TheWayOut.Gameplay
{
    internal class SelectablePeaces : DataList<Peace>
    {
        public PeaceInfoSet PeaceInfoSet;
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
                foreach (var peace in peaces)
                {
                    if (peace.peaceInfo == signal.peace.Info)
                    {
                        peace.peaceInfo = GenerateNew();
                        break;
                    }
                }
                Clear();
                foreach (var peace in peaces)
                    AddItem(peace);
            }
            else
            {
                signal.peace.ReturnToPosition();
            }
        }

        private PeaceInfo GenerateNew()
        {
            var okPeaces = PeaceInfoSet.peaceInfos.Where(item => Maze.IsTherePlace(item) && !peaces.Any(peace => peace.peaceInfo == item)).ToArray();
            if (okPeaces.Any()) 
                return okPeaces[UnityEngine.Random.Range(0, okPeaces.Length)];
            Debug.LogWarning("No available peace, trying notSame");
            var notSame = PeaceInfoSet.peaceInfos.Where(item => !peaces.Any(peace => peace.peaceInfo == item)).ToArray();
            if (notSame.Any())
                return notSame[UnityEngine.Random.Range(0, notSame.Length)];
            Debug.LogWarning("No available notsame peace, returning random");
            return PeaceInfoSet.peaceInfos[UnityEngine.Random.Range(0, PeaceInfoSet.peaceInfos.Count)];
        }

        internal void ClearAll()
        {
            peaces.Clear();
            while (peaces.Count < 3)
            {
                peaces.Add(new Peace() { peaceInfo = GenerateNew() });
            }
            Clear();
            foreach (var peace in peaces)
                AddItem(peace);
        }
    }
}
