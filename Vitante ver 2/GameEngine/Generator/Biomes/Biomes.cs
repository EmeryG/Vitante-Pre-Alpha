using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vitante.Generator
{
    class Biome
    {
        public virtual int GetBlock(Random r)
        {
            return 0;
        }
    }

    class Snow : Biome
    {
        public override int GetBlock(Random r)
        {

            if (r.Next(20) == 0)
            {
                return 3;
            }
            else
            {
                return 2;
            }
        }
    }

    class Forest : Biome
    {
        public override int GetBlock(Random r)
        {
            if (r.Next(7) == 0)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
    }

    class Plains : Biome
    {
        public override int GetBlock(Random r)
        {
            if (r.Next(100) == 0)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
    }

    class Desert : Biome
    {
        public override int GetBlock(Random r)
        {
            return 2;
        }
    }
}
