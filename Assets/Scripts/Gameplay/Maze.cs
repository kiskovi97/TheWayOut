using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheWayOut.Gameplay
{
    class Maze : MonoBehaviour
    {
        [SerializeField] private int column;
        [SerializeField] private int startRow;
        [SerializeField] private int endRow;

        public int Column => column;
        public int StartIndex => startRow * column;
        public int EndIndex => (endRow + 1) * column - 1;

        public event Action OnFinished;

        private Dictionary<int, PuzzlePeace> placedPeaces = new Dictionary<int, PuzzlePeace>();

        private HashSet<int> available = new HashSet<int>();

        public void GenerateRoot()
        {
            available = new HashSet<int>();
            if (placedPeaces.ContainsKey(StartIndex))
                GenerateRoot(StartIndex);
        }

        public void GenerateRoot(int index)
        {
            if (index == EndIndex)
            {
                GameFinished();
                return;
            }

            if (!placedPeaces.ContainsKey(index))
            {
                return;
            }

            available.Add(index);
            var peace = placedPeaces[index];
            peace.transform.localScale = new Vector3(1f, 1f, 1f);
            var next = NextIndexes(peace);
            foreach (var nextIndex in next)
            {
                if (!available.Contains(nextIndex))
                    GenerateRoot(nextIndex);
            }
        }

        private void GameFinished()
        {
            OnFinished?.Invoke();
            foreach (var objectum in placedPeaces.Values)
                Destroy(objectum.gameObject);
            placedPeaces.Clear();
        }

        public bool TryAddPeace(PuzzlePeace peace)
        {
            if (placedPeaces.ContainsKey(peace.Index))
                return false;
            placedPeaces.Add(peace.Index, peace);
            return true;
        }

        private int[] NextIndexes(PuzzlePeace peace)
        {
            var index = peace.Index;
            if (!placedPeaces.ContainsKey(index))
                return new int[0];
            var listIndexes = new List<int>();
            if (index % column != 0 && placedPeaces.ContainsKey(index - 1) && peace.IsFreeWay(0) && placedPeaces[index - 1].IsFreeWay(2))
                listIndexes.Add(index - 1);


            if (placedPeaces.ContainsKey(index - column) && peace.IsFreeWay(1) && placedPeaces[index - column].IsFreeWay(3))
                listIndexes.Add(index - column);

            if (index % column != column - 1 && placedPeaces.ContainsKey(index + 1) && peace.IsFreeWay(2) && placedPeaces[index + 1].IsFreeWay(0))
                listIndexes.Add(index + 1);

            if (placedPeaces.ContainsKey(index + column) && peace.IsFreeWay(3) && placedPeaces[index + column].IsFreeWay(1))
                listIndexes.Add(index + column);

            return listIndexes.ToArray();
        }
    }
}
