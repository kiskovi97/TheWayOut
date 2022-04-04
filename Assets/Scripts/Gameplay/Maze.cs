﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheWayOut.Gameplay
{
    class Maze : MonoBehaviour
    {
        [SerializeField] private int column;

        private Dictionary<int, PuzzlePeace> placedPeaces = new Dictionary<int, PuzzlePeace>();

        private HashSet<int> available = new HashSet<int>();

        public void GenerateRoot()
        {
            available = new HashSet<int>();
            var first = placedPeaces.Keys.Min();
            GenerateRoot(first);
        }

        public void GenerateRoot(int index)
        {
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

        public bool TryAddPeace(PuzzlePeace peace)
        {
            if (placedPeaces.ContainsKey(peace.Index))
                return false;
            placedPeaces.Add(peace.Index, peace);
            return true;
        }

        public int[] NextIndexes(PuzzlePeace peace)
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
