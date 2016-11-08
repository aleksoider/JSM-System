using System;
using System.Collections.Generic;
using System.Text;

namespace mass
{
    class universum
    {
        protected List<string> u;
        protected int n;
        public List<string> getU
        {
            get
            {
                return u;
            }
            set
            {
                u = value;
            }
        }
        public int uni
        {
            get
            {
                return n;
            }
            set
            {
                n = value;
            }
        }
        public universum()
        {
            n = 0;
        }
        public universum(System.Windows.Forms.DataGridView d)
        {
            u = new List<string>(); 
            n = d.RowCount;
            for (int i = 0; i < n; i++)
                u.Add(Convert.ToString(d[0, i].Value));
        }
        public string this[int i]
        {   get
            {
               return u[i];    
            }
        }
        public void Add(string s)
        {
            u.Add(s);
            n++;
        }
        public void Delete(int ind)
        {
            u.RemoveAt(ind);
            n--;
        }
    }
}
