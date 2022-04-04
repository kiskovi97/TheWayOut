
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
            Random.InitState(seed);
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

                if (Random.value < 0.01f * level)
                    Instantiate(block, placementsParent);
                else
                    Instantiate(placementPrefab, placementsParent);
            }
        }

        private void OnPeacePlaced(PuzzlePeace peace, DragPlacement placement)
        {
            peace.isDragable = false;
            peace.transform.SetParent(goalParent);
            Maze.TryAddPeace(peace);
            peace.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            Maze.CheckPathFinding();
            GenerateNew();
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
