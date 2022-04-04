using System;
using System.Collections;
using TheWayOut.Input;
using UnityEngine;

namespace TheWayOut.Main
{
    class Advertisment : MonoBehaviour
    {
        void Start()
        {
            AdsShower.LoadAd(AdEnded);
        }

        private void AdEnded(bool success)
        {
            SceneLoader.LoadScene(SceneLoader.GAMEPLAY);
        }
    }
}
