using System;
using UnityEngine;

namespace TheWayOut.Gameplay
{
    class LevelManager : MonoBehaviour
    {
        private static LevelManager Instance;
        private static int startLevel = 0;

        void Awake()
        {
            if (Instance == null)
                SetInstance(this);
            else
                Destroy(gameObject);
        }

        private static void SetInstance(LevelManager levelManager)
        {
            Instance = levelManager;

            Maze.OnFinished += Maze_OnFinished;

            PeaceGeneration.StartLevel(startLevel, 0);
        }

        private static void Maze_OnFinished()
        {
            startLevel++;
            PeaceGeneration.StartLevel(startLevel, 0);
        }

        void OnDestroy()
        {
            if (Instance == this)
                SetInstance(null);
        }
    }
}
