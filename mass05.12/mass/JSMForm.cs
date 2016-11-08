using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mass
{
    public partial class JSMForm : Form
    {
        JSM JSM;
        context plus;
        context minus;
        context tao;
        universum []row=new universum[3];
        universum col;
        public JSMForm()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, EventArgs e)
        {
            col = new universum(colUniversum);
            plus = new context(plusDG);
            minus = new context(minusDG);
            tao = new context(taoDG);
            context[] conts = { plus, minus, tao };
            row[0]=new universum(plusU);
            row[1]=new universum(minusU);
            row[2]=new universum(taoU);
            for (int i = 0; i < 3; i++)
            {
                conts[i].uColumn = col;
                conts[i].setUniversForSS(col);
                conts[i].uRow = row[i];
            }
            colUniversum.Enabled = false;
            //plusDG.Enabled = false;
            //minusDG.Enabled = false;
            //taoDG.Enabled = false;
            plusU.Enabled = false;
            minusU.Enabled = false;
            taoU.Enabled = false;
            JSM = new JSM(conts);
            c1.Hide();
            p1.Hide();
            m1.Hide();
            t1.Hide();
            createButton.Hide();
            JSM.toDGV(r1, r2, r3);
        }
        private void createMatrix(DataGridView d, int x, int y)
        {
            d.Columns.Clear();
            d.Rows.Clear();
            for (int i = 0; i < x; i++)
                d.Columns.Add(Convert.ToString(i), Convert.ToString(i));
            for (int i = 0; i < y; i++)
                d.Rows.Add();
            d.Refresh();
        }
        private void create(DataGridView d, TextBox t)
        {
            d.Columns.Clear();
            d.Rows.Clear();
            d.Columns.Add("1", "1");
            for (int i = 0; i < Convert.ToInt32(t.Text); i++)
                d.Rows.Add();            
            d.Refresh();
        }
        private void create_Click(object sender, EventArgs e)
        {
            create(colUniversum,c1);
            create(plusU, p1);
            create(minusU, m1);
            create(taoU, t1);
            createMatrix(plusDG, colUniversum.RowCount, plusU.RowCount);
            createMatrix(minusDG, colUniversum.RowCount, minusU.RowCount);
            createMatrix(taoDG, colUniversum.RowCount, taoU.RowCount);
        }
        


    }
}