using System.Collections.Generic;

using TheWayOut.Input;
using UnityEngine;
using UnityEngine.UI;

namespace TheWayOut.Gameplay
{
    public class CurrentLevelUI : MonoBehaviour
    {
        public LevelList levelList;

        // Start is called before the first frame update
        void Start()
        {
            var currentIndex = LevelManager.MaxLevel;
            var levels = new List<Level>();
            for (int i = 0; i <= currentIndex; i++)
            {
                var index = i;
                levels.Add(new Level()
                {
                    number = i,
                    enabled = true
                });
            }
            var max = currentIndex + 50;
            max -= max % 10;
            for (int i = currentIndex + 1; i < max; i++)
            {
                levels.Add(new Level()
                {
                    number = i,
                    enabled = false
                });
            }

            levelList.SetData(levels);
        }
    }
}


