using TheWayOut.Input;
using UnityEngine;

namespace TheWayOut.Main
{
    public class StartMenu : MonoBehaviour
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

