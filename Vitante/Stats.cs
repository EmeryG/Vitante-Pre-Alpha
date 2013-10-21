using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vitante
{
    class Stats
    {
        // Strength, Speed, Defence
        int[] stats = new int[3];

        public Stats(int str, int spe, int def)
        {
            stats[0] = str;
            stats[1] = spe;
            stats[2] = def;
        }

        public int GetStr()
        { return stats[0]; }

        public int GetSpe()
        { return stats[1]; }

        public int GetDef()
        { return stats[2]; }

        public void AddStr(int i)
        { stats[0] += i; }

        public void AddSpe(int i)
        { stats[1] += i; }

        public void AddDef(int i)
        { stats[2] += i; }
    }
}
