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

        private static Maze Instance { get; set; }

        public static int Column { get; private set; }
        public static int StartIndex { get; private set; }
        public static int EndIndex { get; private set; }

        public static event Action OnFinished;

        private static Dictionary<int, PuzzlePeace> placedPeaces = new Dictionary<int, PuzzlePeace>();

        private static HashSet<int> available = new HashSet<int>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Column = column;
                StartIndex = startRow * column;
                EndIndex = (endRow + 1) * column - 1;
            }
        }

        void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        public static void CheckPathFinding()
        {
            available = new HashSet<int>();
            if (placedPeaces.ContainsKey(StartIndex))
                GenerateRoot(StartIndex);
        }

        internal static void Clear()
        {
            foreach (var objectum in placedPeaces.Values)
                Destroy(objectum.gameObject);
            placedPeaces.Clear();
        }

        private static void GenerateRoot(int index)
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

        private static void GameFinished()
        {
            OnFinished?.Invoke();
        }

        public static bool TryAddPeace(PuzzlePeace peace)
        {
            if (placedPeaces.ContainsKey(peace.Index))
                return false;
            placedPeaces.Add(peace.Index, peace);
            return true;
        }

        private static int[] NextIndexes(PuzzlePeace peace)
        {
            var index = peace.Index;
            if (!placedPeaces.ContainsKey(index))
                return new int[0];
            var listIndexes = new List<int>();
            if (index % Column != 0 && placedPeaces.ContainsKey(index - 1) && peace.IsFreeWay(0) && placedPeaces[index - 1].IsFreeWay(2))
                listIndexes.Add(index - 1);


            if (placedPeaces.ContainsKey(index - Column) && peace.IsFreeWay(1) && placedPeaces[index - Column].IsFreeWay(3))
                listIndexes.Add(index - Column);

            if (index % Column != Column - 1 && placedPeaces.ContainsKey(index + 1) && peace.IsFreeWay(2) && placedPeaces[index + 1].IsFreeWay(0))
                listIndexes.Add(index + 1);

            if (placedPeaces.ContainsKey(index + Column) && peace.IsFreeWay(3) && placedPeaces[index + Column].IsFreeWay(1))
                listIndexes.Add(index + Column);

            return listIndexes.ToArray();
        }
    }
}
