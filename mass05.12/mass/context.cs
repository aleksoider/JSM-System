using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace mass
{
    class context
    {
        private universum uR;
        private universum uC;
        private List<smallSet> body;
        public void setUniversForSS(universum u)
        {
            for(int i=0;i<body.Count;i++)
            {
                body[i].univers=u;
            }
        }
        public int Count
        {
            get
            {
                return body.Count;
            }
        }
        public context(System.Windows.Forms.DataGridView d)
        {
            body=new List<smallSet>();
            int r = d.RowCount;
            int c = d.ColumnCount;
            //body = new smallSet[r];
            for (int i = 0; i < d.RowCount; i++)
            {
                smallSet s = new smallSet(d, i);
                body.Add(s);
                body[i].univers = uR;
            }
        }
        public universum uColumn
        {
            get
            {
                return uC;
            }
            set
            {
                uC = value;
            }
        }
        public universum uRow
        {
            get
            {
                return uR;
            }
            set
            {
                uR = value;
            }
        }
        public smallSet this[int i]
        {
            get
            {
                return body[i];
            }
            set
            {
                body[i] = value;
            }
        }
        public bool this[int i, int j]
        {
            get
            {
                return body[i][j];
            }
            set
            {
                body[i][j] = value;
            }
        }
        public smallSet makeSmallSet(int c)
        {
            smallSet a = new smallSet();
            for (int i = 0; i < body.Count; i++)
            {
                a[i] = body[i][c];
            }
            a.univers = uR;
            return a;
        }
        public void Add(smallSet s, string str)
        {
            body.Add(s);
            uR.Add(str);
        }
        public void Delete(smallSet s, int ind)
        {
            body.Remove(s);
            uR.Delete(ind);
        }
        public void toDGV(DataGridView d)
        {

            for (int j = 0; j < this.uC.uni; j++)
            {
                d.Columns.Add(Convert.ToString(j), Convert.ToString(j));
            }
            for (int i = 0; i < body.Count; i++)
            {
                d.Rows.Add();
            }
            for (int i = 0; i < body.Count; i++)
            {
                for (int j = 0; j < body[i].universN; j++)
                {
                    d[j, i].Value = this[i, j] == true ? 1 : 0;
                }
            }
        }
    }
}