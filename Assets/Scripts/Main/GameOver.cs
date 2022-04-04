using TheWayOut.Input;
using UnityEngine;

namespace TheWayOut.Main
{
    class GameOver : MonoBehaviour
    {
        public void GoToGamePlay()
        {
            SceneLoader.LoadScene(SceneLoader.GAMEPLAY);
        }

        public void GoToMenu()
        {
            SceneLoader.LoadScene(SceneLoader.MENU);
        }

        public void ExitGame()
        {
            SceneLoader.ExitGame();
        }
    }
}
