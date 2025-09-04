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
            var available = IsAvailable(peace.Info, peace.Index);
            if (available)
                placedPeaces.Add(peace.Index, peace);
            return available;
        }

        public static bool IsTherePlace(PeaceInfo info)
        {
            for(int place = 0; place < Column * Column; place++)
            {
                if (!IsAvailable(info, place))
                    continue;

                if (place == StartIndex || place == EndIndex
                    || IsThere(PeaceDirection.Left, place) 
                    || IsThere(PeaceDirection.Right, place) 
                    || IsThere(PeaceDirection.Up, place) 
                    || IsThere(PeaceDirection.Down, place))
                    return true;
            }
            return false;
        }

        private static int NextIndex(PeaceDirection direction, int index)
        {
            var nextIndex = index;
            switch (direction)
            {
                case PeaceDirection.Left:
                    if (index % Column > 0)
                        nextIndex = index - 1;
                    break;
                case PeaceDirection.Right:
                    if (index % Column < Column - 1)
                        nextIndex = index + 1;
                    break;
                case PeaceDirection.Up:
                    if (index - Column > 0)
                        nextIndex = index - Column;
                    break;
                case PeaceDirection.Down:
                    if (index + Column < Column * Column)
                        nextIndex = index + Column;
                    break;
            }
            return nextIndex;
        }

        private static bool IsAvailable(PeaceInfo peace, int Index)
        {
            if (placedPeaces.ContainsKey(Index))
                return false;

            if (Index == StartIndex && !peace.freeLeft)
                return false;

            if (Index == EndIndex && !peace.freeRight)
                return false;

            if (!IsAvailable(PeaceDirection.Left, Index, peace.freeLeft))
                return false;
            if (!IsAvailable(PeaceDirection.Right, Index, peace.freeRight))
                return false;
            if (!IsAvailable(PeaceDirection.Up, Index, peace.freeUp))
                return false;
            if (!IsAvailable(PeaceDirection.Down, Index, peace.freeDown))
                return false;

            return true;
        }

        private static bool IsAvailable(PeaceDirection direction, int Index, bool isFreeWay)
        {
            var leftIndex = NextIndex(direction, Index);
            var oposite = (direction) switch
            {
                PeaceDirection.Left => PeaceDirection.Right,
                PeaceDirection.Right => PeaceDirection.Left,
                PeaceDirection.Up => PeaceDirection.Down,
                PeaceDirection.Down => PeaceDirection.Up,
                _ => PeaceDirection.Right,
            };
            return !placedPeaces.TryGetValue(leftIndex, out var leftPeace) || leftPeace.IsFreeWay(oposite) == isFreeWay;
        }

        private static bool IsThere(PeaceDirection direction, int Index)
        {
            var leftIndex = NextIndex(direction, Index);
            return placedPeaces.ContainsKey(leftIndex);
        }

        private static int[] NextIndexes(PuzzlePeace peace)
        {
            var index = peace.Index;
            if (!placedPeaces.ContainsKey(index))
                return new int[0];
            var listIndexes = new List<int>();
            if (index % Column != 0 && placedPeaces.ContainsKey(index - 1) && peace.IsFreeWay(PeaceDirection.Left) && placedPeaces[index - 1].IsFreeWay(PeaceDirection.Right))
                listIndexes.Add(index - 1);

            if (placedPeaces.ContainsKey(index - Column) && peace.IsFreeWay(PeaceDirection.Up) && placedPeaces[index - Column].IsFreeWay(PeaceDirection.Down))
                listIndexes.Add(index - Column);

            if (index % Column != Column - 1 && placedPeaces.ContainsKey(index + 1) && peace.IsFreeWay(PeaceDirection.Right) && placedPeaces[index + 1].IsFreeWay(PeaceDirection.Left))
                listIndexes.Add(index + 1);

            if (placedPeaces.ContainsKey(index + Column) && peace.IsFreeWay(PeaceDirection.Down) && placedPeaces[index + Column].IsFreeWay(PeaceDirection.Up))
                listIndexes.Add(index + Column);

            return listIndexes.ToArray();
        }
    }
}
