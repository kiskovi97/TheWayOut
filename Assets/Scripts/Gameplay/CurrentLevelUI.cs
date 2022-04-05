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
                var button = Instantiate(buttonPrefab, transform);
                button.onClick.AddListener(() => SelectedLevel(i));
                var text = button.GetComponentInChildren<Text>();
                if (text != null)
                    text.text = i.ToString();
            }

            for (int i = currentIndex + 1; i < currentIndex + 50; i++)
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
            SceneLoader.LoadScene(SceneLoader.GAMEPLAY);
        }
    }
}


