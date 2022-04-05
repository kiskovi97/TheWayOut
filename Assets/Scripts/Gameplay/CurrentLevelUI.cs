using TheWayOut.Input;
using UnityEngine;
using UnityEngine.UI;

namespace TheWayOut.Gameplay
{
    public class CurrentLevelUI : MonoBehaviour
    {
        [SerializeField] private Button buttonPrefab;

        // Start is called before the first frame update
        void Start()
        {
            var currentIndex = LevelManager.MaxLevel;
            for (int i = 0; i <= currentIndex; i++)
            {
                var index = i;
                var button = Instantiate(buttonPrefab, transform);
                button.onClick.AddListener(() => SelectedLevel(index));
                var text = button.GetComponentInChildren<Text>();
                if (text != null)
                    text.text = i.ToString();
            }
            var max = currentIndex + 50;
            max -= max % 10;
            for (int i = currentIndex + 1; i < max; i++)
            {
                var button = Instantiate(buttonPrefab, transform);
                var text = button.GetComponentInChildren<Text>();
                if (text != null)
                    text.text = i.ToString();
                button.interactable = false;
            }
        }

        private void SelectedLevel(int i)
        {
            LevelManager.CurrentLevel = i;
            SceneLoader.LoadScene(SceneLoader.GAMEPLAY);
        }
    }
}


