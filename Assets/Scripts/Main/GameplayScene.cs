using TheWayOut.Input;
using UnityEngine;

namespace TheWayOut.Main
{
    class GameplayScene : MonoBehaviour
    {
        public void ReStart()
        {
            AdsInitializer.LoadAd((_) => SceneLoader.LoadScene(SceneLoader.GAMEPLAY));
        }

        public void GoToMenu()
        {
            SceneLoader.LoadScene(SceneLoader.MENU);
        }
    }
}
