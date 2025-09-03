using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kiskovi.Core;

namespace TheWayOut.Gameplay
{
    public class LevelList : DataList<Level>
    {
        public void SetData(IEnumerable<Level> data)
        {
            Clear();

            foreach (var level in data)
            {
                AddItem(level);
            }
        }
    }
}
