using System;
using TheWayOut.Input;
using UnityEngine;

namespace TheWayOut.Gameplay
{
    class LevelManager : MonoBehaviour
    {
        private static LevelManager Instance;
        public static int CurrentLevel { get; set; } = -1;
        public static int MaxLevel => PlayerPrefs.GetInt(nameof(MaxLevel), 0);

        void Awake()
        {
            if (Instance == null)
                SetInstance(this);
            else
                Destroy(gameObject);
        }

        private static void SetInstance(LevelManager levelManager)
        {
            //CurrentLevel = MaxLevel;
            if (CurrentLevel < 0)
                CurrentLevel = MaxLevel;
            Instance = levelManager;
            Maze.Clear();
            Maze.OnFinished += Maze_OnFinished;

            PeaceGeneration.StartLevel(CurrentLevel, 0);
        }

        public static void Restart()
        {
            PeaceGeneration.StartLevel(CurrentLevel, 0);
        }

        private static void Maze_OnFinished()
        {
            CurrentLevel++;
            if (CurrentLevel > MaxLevel)
            {
                PlayerPrefs.SetInt(nameof(MaxLevel), CurrentLevel);
                PlayerPrefs.Save();
            }    
            SceneLoader.LoadScene(SceneLoader.GAMEOVER);
            ClearAllButton.StaticSpecialOne += 1;
            ClearMazeButton.StaticSpecialOne += 1;
            //PeaceGeneration.StartLevel(startLevel, 0);
        }

        void OnDestroy()
        {
            if (Instance == this)
            {
                SetInstance(null);
                Maze.OnFinished -= Maze_OnFinished;
            }
        }
    }
}
