using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace TheWayOut.Gameplay
{
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
