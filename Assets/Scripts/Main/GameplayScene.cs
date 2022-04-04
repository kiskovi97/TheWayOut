using System;
using System.Collections;
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
    }
}
