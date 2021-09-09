using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitante.Lib
{
    class Lib
    {
        public static double Distance(int[] a, int[] b)
        {
            double k = (((double)a[0] - (double)b[0]) * ((double)a[0] - (double)b[0])) + (((double)a[1] - (double)b[1]) * ((double)a[1] - (double)b[1]));
            return Math.Sqrt(k);
        }
    }
}
