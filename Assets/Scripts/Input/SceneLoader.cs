using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheWayOut.Input
{
    public class SceneLoader
    {
        public static int START_MENU = 0;
        public static int MENU = 1;
        public static int GAMEPLAY = 2;
        public static int GAMEOVER = 3;

        private static int LOADING = 4;
        public static int ADDVERTISMENT = 5;
        public static int SETTINGS = 6;

        private static int lastSceneRequested = 0;

        public static void LoadScene(int index)
        {
            lastSceneRequested = index;
            SceneManager.LoadSceneAsync(LOADING).completed += OnSceneLoaded;
        }

        private static void OnSceneLoaded(AsyncOperation obj)
        {
            SceneManager.LoadScene(lastSceneRequested);
        }

        public static void ExitGame()
        {
            Application.Quit();
        }
    }
}


