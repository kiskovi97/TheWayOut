
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Zenject;

namespace TheWayOut.Gameplay
{
    public class PeaceGeneration : MonoBehaviour
    {
        [SerializeField] private SelectablePeaces selectablePeaces;
        [SerializeField] private Transform placementsParent;
        [SerializeField] private DragPlacement placementPrefab;
        [SerializeField] private GameObject block;
        [SerializeField] private DragPlacement startBlock;
        [SerializeField] private DragPlacement endBlock;

        private static List<DragPlacement> placements = new List<DragPlacement>();
        private static PeaceGeneration Instance { get; set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public static void StartLevel(int level, int seed)
        {
            if (Instance != null)
            {
                Instance._StartLevel(level, seed);
            }
        }

        public static void ClearLevel()
        {
            foreach(var placement in placements)
            {
                placement.RemoveItem();
            }
            placements.Clear();
            Maze.Clear();
        }

        private void _StartLevel(int level, int seed)
        {
            ClearAll();

            GeneratePlacements(level, seed);

            Instance.selectablePeaces.ClearAll();
        }

        private void ClearAll()
        {
            selectablePeaces.ClearAll();
            foreach (Transform trans in placementsParent)
                Destroy(trans.gameObject);
            placements.Clear();
        }

        private void GeneratePlacements(int level, int seed)
        {
            Random.InitState(seed + level);
            var matrix = new int[Maze.Column * Maze.Column];
            var list = Enumerable.Range(0, Maze.Column * Maze.Column).ToList();
            list = list.OrderBy(item => Random.value).ToList();
            for (int i = 0; i < Maze.Column * Maze.Column; i++)
            {
                var index = list[i];
                if (index == Maze.StartIndex)
                {
                    matrix[index] = 0;
                    continue;
                }

                if (index == Maze.EndIndex)
                {
                    matrix[index] = 0;
                    continue;
                }

                if (Random.value < 0.01f * level)
                {
                    matrix[index] = 1;
                    if (!CheckAvaiability(matrix))
                    {
                        matrix[index] = 0;
                    }
                } else
                {
                    matrix[index] = 0;
                }
            }
            for (int i = 0; i < Maze.Column * Maze.Column; i++)
            {
                if (i == Maze.StartIndex)
                {
                    placements.Add(Instantiate(startBlock, placementsParent));
                    continue;
                }

                if (i == Maze.EndIndex)
                {
                    placements.Add(Instantiate(endBlock, placementsParent));
                    continue;
                }

                if (matrix[i] == 1)
                    Instantiate(block, placementsParent);
                else
                    placements.Add(Instantiate(placementPrefab, placementsParent));
            }
        }

        private bool CheckAvaiability(int[] matrix)
        {
            var temp = new int[Maze.Column * Maze.Column];
            temp[Maze.StartIndex] = 1;
            var list = Enumerable.Range(0, Maze.Column * Maze.Column).ToList();
            list.Remove(Maze.StartIndex);
            for (int i = 0; i < Maze.Column * Maze.Column; i++)
            {
                foreach (var item in list)
                {
                    if (matrix[item] > 0) continue;
                    var column = item % Maze.Column;
                    var row = Mathf.FloorToInt(item / Maze.Column);
                    if ((column > 0 && temp[item - 1] == 1)
                        || (column < Maze.Column - 1 && temp[item + 1] == 1)
                        || (row > 0 && temp[item - Maze.Column] == 1)
                        || (row < Maze.Column - 1 && temp[item + Maze.Column] == 1))
                    {
                        temp[item] = 1;
                        list.Remove(item);
                        break;
                    }
                }
                if (temp[Maze.EndIndex] == 1)
                    return true;
            }

            return false;
        }

        public static void ClearCurrentPeaces()
        {
            if (Instance == null) return; 
            Instance.selectablePeaces.ClearAll();
        }
    }
}
