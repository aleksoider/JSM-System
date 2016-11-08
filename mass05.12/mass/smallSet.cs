using System;
using System.Collections.Generic;
using System.Text;

namespace mass
{
    class smallSet
    {
        private universum u;
        private int count = 0;
        private ulong body;
        private void CountInc()
        {
            for (int i = 0; i < u.uni; i++)
            {
                if (this[i]) count++;
            }
        }
        public int Count
        {
            get
            {
                CountInc();
                return count;
            }
        }
        public int universN
        {
            get
            {
                return u.uni;
            }
            set
            {
                u.uni = value;
            }
        }
        public universum univers
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
        public bool this[int i]
        {
            get
            {
                ulong b = 1;
                b <<= i;
                if ((b & body) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                ulong b = 1;
                b <<= i;
                if (value)
                    body |= b;
            }
        }
        public smallSet(params int[] indexes)
        {
            body = 0;
            for (int i = 0; i < indexes.Length; i++)
                this[indexes[i]] = true;
        }
        public smallSet(System.Windows.Forms.DataGridView d)
        {
            if (d.RowCount != 1) return;
            body = new ulong();
            for (int i = 0; i < d.ColumnCount; i++)
                this[i] = Convert.ToInt32(d[i, 0].Value) == 1 ? true : false;
        }
        public smallSet(System.Windows.Forms.DataGridView d, int line)
        {
            //if (d.RowCount != 1) return;
            body = new ulong();
            for (int i = 0; i < d.ColumnCount; i++)
                this[i] = Convert.ToInt32(d[i, line].Value) == 1 ? true : false;
        }
        public void toDGV(System.Windows.Forms.DataGridView d)
        {
            if (1 != d.RowCount || this.universN != d.ColumnCount)
                return;
            for (int j = 0; j < d.ColumnCount; j++)
                d[j, 0].Value = this[j] == true ? 1 : 0; //body[i, j];
            d.Refresh();
        }
        public static smallSet operator +(smallSet a, smallSet b)
        {
            smallSet r = new smallSet();
            r.univers = a.univers;
            for (int i = 0; i < a.universN; i++)
            {
                if (a[i] || b[i]) r[i] = true;
                else r[i] = false;
            }
            return r;
        }
        public static smallSet operator *(smallSet a, smallSet b)
        {
            smallSet r = new smallSet();
            r.univers = a.univers;
            for (int i = 0; i < a.universN; i++)
            {
                if (a[i] && b[i]) r[i] = true;
                else r[i] = false;
            }
            return r;
        }
        public static smallSet operator -(smallSet a, smallSet b)
        {
            smallSet r = new smallSet();
            r.univers = a.univers;
            for (int i = 0; i < a.universN; i++)
            {
                if (a[i] == true && b[i] == false) r[i] = true;
                else r[i] = false;
            }
            return r;
        }
        public static bool operator ==(smallSet a, smallSet b)
        {
            return a.body == b.body ? true : false;
        }
        public static bool operator !=(smallSet a, smallSet b)
        {
            return a.body != b.body ? true : false;
        }
        public static bool operator <=(smallSet a, smallSet b)
        {
            //smallSet c = a * b;
            return a * b == a ? true : false;
        }
        public static bool operator >=(smallSet a, smallSet b)
        {
            return !(a * b == a) ? true : false;
        }
        public smallSet complement()
        {
            smallSet r = new smallSet();
            r.univers = this.univers;
            for (int i = 0; i < this.universN; i++)
            {
                if (this[i]) r[i] = false;
                else r[i] = true;
            }
            return r;
        }
        public string ToString()
        {
            string r = "";
            for (int i = 0; i < u.uni; i++)
                if (this[i]) r += " " + u.getU[i];//r.Insert(r.Length, " " + u.getU[i]);
            return r;
        }
    }
}
