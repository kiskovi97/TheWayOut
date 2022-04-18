using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace TheWayOut.Gameplay
{
    class Maze : MonoBehaviour
    {
        [SerializeField] private int column;
        [SerializeField] private int startRow;
        [SerializeField] private int endRow;
        [SerializeField] private Character character;

        private static Maze Instance { get; set; }

        public static int Column { get; private set; }
        public static int StartIndex { get; private set; }
        public static int EndIndex { get; private set; }

        public static event Action OnFinished;

        private static Dictionary<int, PuzzlePeace> placedPeaces = new Dictionary<int, PuzzlePeace>();

        private static HashSet<int> available = new HashSet<int>();

        private static Stack<Vector3> tempPosition = new Stack<Vector3>();
        private static List<Vector3> FinalPositions = new List<Vector3>();

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
            available.Clear();
            tempPosition.Clear();
            FinalPositions.Clear();
            if (placedPeaces.ContainsKey(StartIndex))
            {
                GenerateRoot(StartIndex);
                if (FinalPositions.Count > 0)
                {
                    GameFinished();
                }
            }
        }

        internal static void Clear()
        {
            foreach (var objectum in placedPeaces.Values)
                if (objectum != null)
                    Destroy(objectum.gameObject);
            placedPeaces.Clear();
        }

        private static void GenerateRoot(int index)
        {
            if (index == EndIndex)
            {
                var peaceEnd = placedPeaces[index];
                peaceEnd.transform.localScale = new Vector3(1f, 1f, 1f);

                tempPosition.Push(peaceEnd.transform.position);
                if (FinalPositions.Count == 0 || FinalPositions.Count > tempPosition.Count)
                {
                    FinalPositions = tempPosition.ToList();
                }

                return;
            }

            if (!placedPeaces.ContainsKey(index))
            {
                return;
            }

            available.Add(index);
            var peace = placedPeaces[index];
            tempPosition.Push(peace.transform.position);

            peace.transform.localScale = new Vector3(1f, 1f, 1f);
            var next = NextIndexes(peace);
            foreach (var nextIndex in next)
            {
                if (!available.Contains(nextIndex))
                {
                    GenerateRoot(nextIndex);
                    tempPosition.Pop();
                    available.Remove(nextIndex);
                }
            }
        }

        private static void GameFinished()
        {
            if (Instance == null || Instance.character == null)
            {
                OnFinished?.Invoke();
                return;
            }
            Instance.character.gameObject.SetActive(true);
            Instance.character.StartGoing(FinalPositions.ToArray().Reverse().ToArray(), OnFinished);
        }

        public static bool TryAddPeace(PuzzlePeace peace)
        {
            var available = IsAvailable(peace);
            if (available)
                placedPeaces.Add(peace.Index, peace);
            return available;
        }

        private static bool IsAvailable(PuzzlePeace peace)
        {
            if (placedPeaces.ContainsKey(peace.Index))
                return false;

            if (peace.Index == StartIndex && !peace.IsFreeWay(0))
                return false;

            if (peace.Index == EndIndex && !peace.IsFreeWay(2))
                return false;

            var column = peace.Index % Column;
            var row = Mathf.FloorToInt((float)peace.Index / Column);

            var index = peace.Index - 1;
            if (column > 0 && placedPeaces.ContainsKey(index))
            {
                var peace2 = placedPeaces[index];
                if ((peace2.IsFreeWay(2) && !peace.IsFreeWay(0)) || (!peace2.IsFreeWay(2) && peace.IsFreeWay(0)))
                    return false;
            }

            index = peace.Index - Column;
            if (row > 0 && placedPeaces.ContainsKey(index))
            {
                var peace2 = placedPeaces[index];
                if ((peace2.IsFreeWay(3) && !peace.IsFreeWay(1)) || (!peace2.IsFreeWay(3) && peace.IsFreeWay(1)))
                    return false;
            }

            index = peace.Index + 1;
            if (column < Column - 1 && placedPeaces.ContainsKey(index))
            {
                var peace2 = placedPeaces[index];
                if ((peace2.IsFreeWay(0) && !peace.IsFreeWay(2)) || (!peace2.IsFreeWay(0) && peace.IsFreeWay(2)))
                    return false;
            }

            index = peace.Index + Column;
            if (row < Column - 1 && placedPeaces.ContainsKey(index))
            {
                var peace2 = placedPeaces[index];
                if ((peace2.IsFreeWay(1) && !peace.IsFreeWay(3)) || (!peace2.IsFreeWay(1) && peace.IsFreeWay(3)))
                    return false;
            }

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
