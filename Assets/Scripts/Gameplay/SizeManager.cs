using UnityEngine;
using UnityEngine.UI;

namespace TheWayOut.Gameplay
{
    public class SizeManager : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        // Start is called before the first frame update
        void Start()
        {
            var widthPixel = Screen.width;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

