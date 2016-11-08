using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace mass
{
    class JSM
    {
        context plus;
        context minus;
        context TAO;
        List<concept> plusExample;
        List<concept> minusExample;
        public JSM(context[] cont)
        {
            plus = cont[0];
            minus = cont[1];
            TAO = cont[2];
            plusExample = Norris(plus);
            minusExample = Norris(minus);
            Filter(plusExample);
            Filter(minusExample);
            Kernel();
        }
        private void Filter(List<concept> L)
        {
            for (int i = 0; i < L.Count; i++)
            {
                if (L[i].Intens.Count < 2)
                    L.Remove(L[i]);
            }
        }
        private void Kernel()
        {
            int l = TAO.Count;
            while (true)
            {
                for (int i = 0; i < TAO.Count; i++)
                {
                    int b1 = checkExstens(TAO[i], plusExample);
                    int b2 = checkExstens(TAO[i], minusExample);
                    if (b1 == 1 && b2 == 0)
                    {
                        plus.Add(TAO[i], TAO.uRow[i]);
                        TAO.Delete(TAO[i], i);
                        i--;
                    }
                    if (b1 == 0 && b2 == 1)
                    {
                        minus.Add(TAO[i], TAO.uRow[i]);
                        TAO.Delete(TAO[i], i);
                        i--;
                    }
                }
                plusExample = Norris(plus);
                minusExample = Norris(minus);
                Filter(plusExample);
                Filter(minusExample);
                if (l == TAO.Count) break;
                else l = TAO.Count;
            }
        }
        private int checkExstens(smallSet s, List<concept> L)//сравнивать признаки с признаками, сейчас объекты с признаками
        {
            for (int i = 0; i < L.Count; i++)
            {
                if (L[i].Extens.Count == 0)
                    i++;
                if (L[i].Extens <= s)
                    return 1;
            }
            return 0;
        }
        private bool relCanon(context cont, int ind, smallSet Y, smallSet z)
        {
            for (int i = 0; i < ind; i++)
                if (!Y[i] && (z * cont[i]) == z)
                    return false;
            return true;
        }
        private bool canon(context cont, int ind)
        {
            for (int i = 0; i < ind; i++)
                if ((cont[ind] * cont[i]) == cont[ind])
                    return false;
            return true;
        }
        private List<concept> Norris(context cont)
        {
            List<concept> L;
            L = new List<concept>();
            int n=cont.uRow.uni;
            for (int i = 0; i < cont.uRow.uni; i++)
            {
                concept x = new concept(cont[i], i);
                x.Intens.univers = cont.uRow;
                x.Extens.univers = cont.uColumn;
                if (L.Count != 0)
                    for (int j = 0; j < L.Count; j++)
                        if (L[j] <= x)
                        {
                            concept c = new concept(L[j].Extens, L[j].Intens + x.Intens);
                            L[j] = c;
                        }
                        else
                        {
                            smallSet z = L[j].Extens * x.Extens;
                            if (relCanon(cont, i, L[j].Intens, z))
                            {
                                concept c = new concept(z, L[j].Intens + x.Intens);
                                L.Add(c);
                            }
                        }
                if (canon(cont, i))
                    L.Add(x);
            }
            return L;
        }
        public void toListBox(ListBox LB, List<concept> L)
        {
            LB.Items.Clear();
            for (int i = 0; i < L.Count; i++)
            {
                LB.Items.Add(L[i].ToString());
            }
        }
        private void Clear(DataGridView dataGridView)
        {
            while (dataGridView.Rows.Count > 1)
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
            while (dataGridView.Columns.Count > 1)
                for (int i = 0; i < dataGridView.Columns.Count - 1; i++)
                    dataGridView.Columns.Remove(dataGridView.Columns[i]);
        }
        public void toDGV(params DataGridView[] d)
        {
            for (int i = 0; i < 3; i++)
            {
                Clear(d[i]);
            }
            plus.toDGV(d[0]);
            minus.toDGV(d[1]);
            TAO.toDGV(d[2]);
        }
    }
}

