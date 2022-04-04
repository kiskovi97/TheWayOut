
using UnityEngine;
using UnityEngine.EventSystems;

namespace TheWayOut.Gameplay
{
    public class PeaceGeneration : MonoBehaviour
    {
        [SerializeField] private PuzzlePeace[] peacesPrefabs;
        [SerializeField] private Transform placementParent;
        [SerializeField] private Transform goalParent;
        [SerializeField] private Maze maze;

        private void Start()
        {
            GenerateNew();
            GenerateNew();
            GenerateNew();
        }

        private void OnPeacePlaced(PuzzlePeace peace, DragPlacement placement)
        {
            peace.isDragable = false;
            peace.transform.SetParent(goalParent);
            //if (peace.IsFreeWay(0))
            //    Debug.LogWarning("left");
            //if (peace.IsFreeWay(1))
            //    Debug.LogWarning("up");
            //if (peace.IsFreeWay(2))
            //    Debug.LogWarning("right");
            //if (peace.IsFreeWay(3))
            //    Debug.LogWarning("down");
            maze.TryAddPeace(peace);

            peace.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            GenerateNew();
        }

        private void GenerateNew()
        {
            var randomIndex = Mathf.FloorToInt(Random.value * peacesPrefabs.Length);
            var selected = peacesPrefabs[randomIndex];
            var instanitated = Instantiate(selected, placementParent);
            instanitated.transform.rotation = Quaternion.AngleAxis(90 * Mathf.RoundToInt(Random.value * 4), Vector3.forward);
            instanitated.OnPeacePlaced += OnPeacePlaced;
        }
    }
}
