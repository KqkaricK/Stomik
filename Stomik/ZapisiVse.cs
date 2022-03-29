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
    public partial class ZapisiVse : Form
    {
        StomContext db;
        DataSet ds;
        DataTable dt;
        public ZapisiVse()
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Zapis zap = new Zapis();
            db.Posets.Load();
            zap.comboBox1.DataSource = db.Posets.Local.ToBindingList();
            zap.comboBox1.ValueMember = "Id";
            zap.comboBox1.DisplayMember = "FIO";
            db.Vraches.Load();
            zap.comboBox2.DataSource = db.Vraches.Local.ToBindingList();
            zap.comboBox2.ValueMember = "Id";
            zap.comboBox2.DisplayMember = "FIO";
            db.Yslygis.Load();
            zap.comboBox3.DataSource = db.Yslygis.Local.ToBindingList();
            zap.comboBox3.ValueMember = "Id";
            zap.comboBox3.DisplayMember = "Name";
            DialogResult result = zap.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            Priem priem = new Priem();
            priem.kod_pos = Convert.ToInt32(zap.comboBox1.SelectedValue.ToString());
            priem.kod_vrach = Convert.ToInt32(zap.comboBox2.SelectedValue.ToString());
            priem.kod_yslugi = Convert.ToInt32(zap.comboBox3.SelectedValue.ToString());
            priem.date = zap.dateTimePicker1.Value.ToString();
            priem.diagnos = zap.textBox1.Text;
            db.Priems.Add(priem);
            db.SaveChanges();
            loding();
            dataGridView1.Refresh();
            MessageBox.Show("Новый объект добавлен");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Priem priem = db.Priems.Find(id);
                Zapis zap = new Zapis();
                db.Posets.Load();
                zap.comboBox1.DataSource = db.Posets.Local.ToBindingList();
                zap.comboBox1.ValueMember = "Id";
                zap.comboBox1.DisplayMember = "FIO";
                db.Vraches.Load();
                zap.comboBox2.DataSource = db.Vraches.Local.ToBindingList();
                zap.comboBox2.ValueMember = "Id";
                zap.comboBox2.DisplayMember = "FIO";
                db.Yslygis.Load();
                zap.comboBox3.DataSource = db.Yslygis.Local.ToBindingList();
                zap.comboBox3.ValueMember = "Id";
                zap.comboBox3.DisplayMember = "Name";
                
                zap.comboBox1.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                zap.comboBox2.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
                zap.comboBox3.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
                zap.textBox1.Text = dataGridView1.Rows[index].Cells[7].Value.ToString();

                DialogResult result = zap.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                priem.kod_pos = Convert.ToInt32(zap.comboBox1.SelectedValue.ToString());
                priem.kod_vrach = Convert.ToInt32(zap.comboBox2.SelectedValue.ToString());
                priem.kod_yslugi = Convert.ToInt32(zap.comboBox3.SelectedValue.ToString());
                priem.date = zap.dateTimePicker1.Value.ToString();
                priem.diagnos = zap.textBox1.Text;
                db.Priems.Add(priem);
                Priem priem1 = db.Priems.Find(id);
                db.Priems.Remove(priem1);
                db.SaveChanges();
                loding();
                dataGridView1.Refresh();
                MessageBox.Show("Объект обнавлён");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
            if (converted == false)
                return;
            Priem priem = db.Priems.Find(id);
            db.Priems.Remove(priem);
            db.SaveChanges();
            loding();
            dataGridView1.Refresh();
            MessageBox.Show("Объект удалён");

        }
    }
}
