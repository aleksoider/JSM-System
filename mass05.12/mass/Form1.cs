using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace mass
{
    public partial class Form1 : Form
    {
        universum u;
        universum u1;
        context cont;
        smallSet s1;
        smallSet s2;
        public Form1()
        {
            InitializeComponent();
        }
        private void create(DataGridView d)
        {
            d.Columns.Clear();
            d.Rows.Clear();
            for (int i = 0; i < Convert.ToInt32(this.textBox1.Text); i++)
                d.Columns.Add(Convert.ToString(i), Convert.ToString(i));
            d.Rows.Add();
            d.Refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            create(dataGridView1);
            create(dataGridView2);
            dataGridView4.Columns.Clear();
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Add(Convert.ToString(1), Convert.ToString(1));
            for (int i = 0; i < Convert.ToInt32(this.textBox1.Text); i++)
                dataGridView4.Rows.Add();
            dataGridView4.Refresh();
        }

        private void result_Click(object sender, EventArgs e)
        {
            smallSet result;
            if (this.con.Checked == true) result = s1 + s2;
            else
                if (this.diz.Checked == true) result = s1 * s2;
                else
                    if (this.minus.Checked == true) result = s1 - s2;
                    else
                        if (this.complem.Checked == true) result = s1.complement();
                        else
                            //if (this.equal.Checked == true) if (s1 <= s2) { trueT.Text = "true"; result = new smallSet(); }
                            //    else { trueT.Text = "false"; result = new smallSet(); }
                            //else
                            {
                                System.Windows.Forms.MessageBox.Show("Choose operation");
                                return;
                            }
            dataGridView3.ColumnCount = result.universN;
            dataGridView3.RowCount = 1;
            result.toDGV(dataGridView3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            u = new universum(dataGridView4);
            s1 = new smallSet(dataGridView1);
            s1.univers = u;
            s2 = new smallSet(dataGridView2);
            s2.univers = u;
            dataGridView4.Enabled = false;
            dataGridView1.Enabled = false;
            dataGridView2.Enabled = false;
            string s = s1.ToString();
            textBox2.Text = s1.ToString();
            textBox3.Text = s2.ToString();
            textBox1.Hide();
            button1.Hide();
        }

        private void createMatrix(DataGridView d)
        {
            d.Columns.Clear();
            d.Rows.Clear();
            for (int i = 0; i < Convert.ToInt32(this.textBox1.Text); i++)
                d.Columns.Add(Convert.ToString(i), Convert.ToString(i));
            for (int i = 0; i < Convert.ToInt32(this.textBox4.Text); i++)
                d.Rows.Add();
            d.Refresh();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            createMatrix(dataGridView6);
            dataGridView5.Columns.Clear();
            dataGridView5.Rows.Clear();
            dataGridView5.Columns.Add(Convert.ToString(1), Convert.ToString(1));
            for (int i = 0; i < Convert.ToInt32(this.textBox4.Text); i++)
                dataGridView5.Rows.Add();
            dataGridView5.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            u = new universum(dataGridView4);
            u1 = new universum(dataGridView5);
            cont = new context(dataGridView6);

            cont.uColumn = u;
            cont.uRow = u1;
            //cont.uColumn = u1; для Норриса
            //cont.uRow = u; для Норриса
            dataGridView4.Enabled = false;
            dataGridView5.Enabled = false;
            textBox4.Hide();
            button3.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            smallSet a = cont.makeSmallSet(Convert.ToInt32(this.textBox5.Text));
            dataGridView7.Columns.Clear();
            dataGridView7.Rows.Clear();
            for (int i = 0; i < a.univers.uni; i++)
                dataGridView7.Columns.Add(Convert.ToString(i), Convert.ToString(i));
            dataGridView7.Rows.Add();
            a.toDGV(dataGridView7);
            dataGridView7.Refresh();

            //Норрис
            textBox6.Text = a.ToString();
            Norris();
            toListBox(L);
            norrisL.Refresh();
        }
        List<concept> L;
        private bool relCanon(int ind, smallSet Y, smallSet z)
        {
            for (int i = 0; i < ind; i++)
                if (!Y[i] && (z * cont[i]) == z)
                    return false;
            return true;
        }
        private bool canon(int ind)
        {
            for (int i = 0; i < ind; i++)
                if ((cont[ind] * cont[i]) == cont[ind])
                    return false;
            return true;
        }
        private void Norris()
        {
            L = new List<concept>();
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
                            if (relCanon(i, L[j].Intens, z))
                            {
                                concept c = new concept(z, L[j].Intens + x.Intens);
                                L.Add(c);
                            }
                        }
                if (canon(i))
                    L.Add(x);
            }
        }

        private void toListBox(List<concept> L)
        {
            this.norrisL.Items.Clear();
            for(int i=0;i<L.Count;i++)
            {
                this.norrisL.Items.Add(L[i].ToString());
            }
        }
    }
}