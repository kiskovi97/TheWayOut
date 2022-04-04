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

        public void ExitGame()
        {
            SceneLoader.ExitGame();
        }
    }
}
