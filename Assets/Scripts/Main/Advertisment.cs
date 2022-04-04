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
            StartCoroutine(PlayAd());
        }

        private IEnumerator PlayAd()
        {
            yield return new WaitForSeconds(5);

            SceneLoader.LoadScene(SceneLoader.GAMEPLAY);
        }
    }
}
