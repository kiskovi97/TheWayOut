using System.Collections.Generic;
using UnityEngine;

namespace TheWayOut.Gameplay
{
    class Maze : MonoBehaviour
    {
        [SerializeField] private int column;

        private Dictionary<int, PuzzlePeace> placedPeaces = new Dictionary<int, PuzzlePeace>();

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
            if (index % column != 0 && placedPeaces.ContainsKey(index - 1))
                listIndexes.Add(index - 1);

            if (placedPeaces.ContainsKey(index - column))
                listIndexes.Add(index - column);

            if (index % column != column - 1 && placedPeaces.ContainsKey(index + 1))
                listIndexes.Add(index + 1);

            if (placedPeaces.ContainsKey(index + column))
                listIndexes.Add(index + column);

            return listIndexes.ToArray();
        }
    }
}
