using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Data.Entity;

namespace Stomik
{
    public partial class ModulVrach : Form
    {
        StomContext db;
        DataSet ds;
        DataTable dt;
        public ModulVrach()
        {
            InitializeComponent();
            loding();
        }
        void loding()
        {
            db = new StomContext();
            ds = new DataSet();
            dt = new DataTable();
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Database=StomikBD;User Id=postgres; Password=7532;");
            con.Open();
            string cmd = ("select priem.\"kod_priem\", poset.\"FIO\", yslygi.\"Name\", yslygi.\"Money\", vrach.\"FIO\", vrach.\"cabinet\", priem.\"date\", priem.\"diagnos\" from yslygi INNER JOIN(poset INNER JOIN(vrach INNER JOIN priem ON vrach.\"Id\" = priem.\"kod_vrach\") ON poset.\"Id\" = priem.\"kod_pos\") ON yslygi.\"Id\" = priem.\"kod_yslugi\";");

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            con.Close();

            db.Priems.Load();
            dataGridView2.DataSource = db.Priems.Local.ToBindingList();
            int i = 0;
            while (i <= 9)
            {
                dataGridView2.Columns[i].Visible = false;
                i++;
            }
            dataGridView2.Columns[i - 2].Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.SaveChanges();
            dataGridView2.Refresh();
            MessageBox.Show("Объект обновлен");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddYslygi addYslygi = new AddYslygi();
            addYslygi.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddVr addVr = new AddVr();
            addVr.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Export export = new Export();
            export.Show();
        }

        private void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView2.FirstDisplayedScrollingRowIndex;
        }
    }
}
