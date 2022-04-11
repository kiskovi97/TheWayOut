
using System.Linq;
using UnityEngine;

namespace TheWayOut.Gameplay
{
    public class PeaceGeneration : MonoBehaviour
    {
        [SerializeField] private PuzzlePeace[] peacesPrefabs;
        [SerializeField] private Transform peacesSelectorParent;
        [SerializeField] private Transform goalParent;
        [SerializeField] private Transform placementsParent;
        [SerializeField] private DragPlacement placementPrefab;
        [SerializeField] private GameObject block;
        [SerializeField] private GameObject startBlock;
        [SerializeField] private GameObject endBlock;

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

        private void _StartLevel(int level, int seed)
        {
            ClearAll();

            GeneratePlacements(level, seed);

            GenerateNew();
            GenerateNew();
            GenerateNew();
        }

        private void ClearAll()
        {
            foreach (Transform trans in peacesSelectorParent)
                Destroy(trans.gameObject);
            foreach (Transform trans in placementsParent)
                Destroy(trans.gameObject);
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
                    Instantiate(startBlock, placementsParent);
                    continue;
                }

                if (i == Maze.EndIndex)
                {
                    Instantiate(endBlock, placementsParent);
                    continue;
                }

                if (matrix[i] == 1)
                    Instantiate(block, placementsParent);
                else
                    Instantiate(placementPrefab, placementsParent);
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

        private void OnPeacePlaced(PuzzlePeace peace, DragPlacement placement)
        {
            if (Maze.TryAddPeace(peace))
            {
                peace.transform.localScale = new Vector3(1f, 1f, 1f);
                peace.transform.SetParent(goalParent);
                peace.isDragable = false;
                Maze.CheckPathFinding();
                GenerateNew();
            }
            else
            {
                peace.ReturnToPosition();
            }
        }

        private void GenerateNew()
        {
            var randomIndex = Mathf.FloorToInt(Random.value * peacesPrefabs.Length);
            var selected = peacesPrefabs[randomIndex];
            var instanitated = Instantiate(selected, peacesSelectorParent);
            instanitated.transform.rotation = Quaternion.AngleAxis(90 * Mathf.RoundToInt(Random.value * 4), Vector3.forward);
            instanitated.OnPeacePlaced += OnPeacePlaced;
        }
    }
}
