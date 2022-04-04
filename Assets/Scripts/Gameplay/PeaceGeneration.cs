
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
        [SerializeField] private Maze maze;

        private void Start()
        {
            GeneratePlacements();

            GenerateNew();
            GenerateNew();
            GenerateNew();

            maze.OnFinished += Maze_OnFinished;
        }

        private void GeneratePlacements()
        {
            foreach (Transform trans in placementsParent.transform)
                Destroy(trans.gameObject);

            for (int i = 0; i < maze.Column * maze.Column; i++)
            {
                if (i == maze.StartIndex)
                {
                    Instantiate(startBlock, placementsParent);
                    continue;
                }

                if (i == maze.EndIndex)
                {
                    Instantiate(endBlock, placementsParent);
                    continue;
                }

                if (Random.value > 0.1f)
                    Instantiate(placementPrefab, placementsParent);
                else
                    Instantiate(block, placementsParent);
            }
        }

        private void Maze_OnFinished()
        {
            GeneratePlacements();
        }

        private void OnPeacePlaced(PuzzlePeace peace, DragPlacement placement)
        {
            peace.isDragable = false;
            peace.transform.SetParent(goalParent);
            maze.TryAddPeace(peace);
            peace.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            maze.GenerateRoot();
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
