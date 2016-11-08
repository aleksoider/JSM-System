using System.Collections.Generic;
using System.Text;

namespace mass
{
    class concept
    {
        public smallSet Intens;
        public smallSet Extens;
        public concept(smallSet r, int n)
        {
            Intens = new smallSet(n);
            Extens = r;
        }
        public concept(smallSet r, smallSet n)
        {
            Intens = n;
            Extens = r;
        }
        public string ToString()
        {
            return string.Format("[" + Intens.ToString() + "-" + Extens.ToString() + "]");
        }
        public static bool operator <=(concept a, concept b)
        {
            return a.Extens <= b.Extens ? true : false;
        }
        public static bool operator >=(concept a, concept b)
        {
            return a.Extens >= b.Extens ? true : false;
        }
    }
}