using TheWayOut.Input;
using UnityEngine;

namespace TheWayOut.Main
{
    class Menu : MonoBehaviour
    {
        public void GoToGamePlay()
        {
            SceneLoader.LoadScene(SceneLoader.GAMEPLAY);
        }

        public void GoToMenu()
        {
            SceneLoader.LoadScene(SceneLoader.MENU);
        }

        public void GoToSettings()
        {
            SceneLoader.LoadScene(SceneLoader.SETTINGS);
        }

        public void ExitGame()
        {
            SceneLoader.ExitGame();
        }
    }
}
