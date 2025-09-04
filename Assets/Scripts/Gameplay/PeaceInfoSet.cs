using System;
using System.Collections.Generic;

using UnityEngine;

namespace TheWayOut.Gameplay
{
    public enum PeaceDirection
    {
        Left = 0,
        Up = 1,
        Right = 2,
        Down = 3
    }

    [Serializable]
    public class PeaceInfo
    {
        public Sprite sprite;
        public bool freeUp;
        public bool freeLeft;
        public bool freeRight;
        public bool freeDown;
        public int minLevel;
    }

    [CreateAssetMenu(fileName ="PeaceInfoList", menuName ="TheWayOut/PeaceInfoList")]
    internal class PeaceInfoSet : ScriptableObject
    {
        public List<PeaceInfo> peaceInfos = new List<PeaceInfo>();
    }
}
