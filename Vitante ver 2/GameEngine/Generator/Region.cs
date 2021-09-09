using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vitante.GameEngine
{
    class Region
    {
        List<Sprite> objects;
        public int x;
        public int y;

        public Region(List<Sprite> obs, int xc, int yc)
        {
            x = xc;
            y = yc;
            objects = obs;
        }

        public List<Sprite> GetObjects()
        {
            return objects;
        }
    }
}
