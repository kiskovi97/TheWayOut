using TheWayOut.Input;
using UnityEngine;

namespace TheWayOut.Main
{
    class GameplayScene : MonoBehaviour
    {
        public void ReStart()
        {
            SceneLoader.LoadScene(SceneLoader.GAMEPLAY, withAd: true);
        }

        public void GoToMenu()
        {
            SceneLoader.LoadScene(SceneLoader.MENU);
        }
    }
}
