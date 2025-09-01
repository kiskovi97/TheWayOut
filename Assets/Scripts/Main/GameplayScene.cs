using TheWayOut.Input;
using UnityEngine;

namespace TheWayOut.Main
{
    class GameplayScene : MonoBehaviour
    {
        public async void ReStart()
        {
            var success = await AdsInitializer.LoadRewardAd();
            SceneLoader.LoadScene(SceneLoader.GAMEPLAY);
        }

        public void GoToMenu()
        {
            SceneLoader.LoadScene(SceneLoader.MENU);
        }
    }
}
