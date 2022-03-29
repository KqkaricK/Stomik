using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace Stomik
{
    public partial class Posetiteli : Form
    {
        StomContext db;
        public Posetiteli()
        {
            InitializeComponent();
            db = new StomContext();
            db.Posets.Load();
            dataGridView1.DataSource = db.Posets.Local.ToBindingList();
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPos addPos = new AddPos();
            DialogResult result = addPos.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            if (addPos.textBox1.Text == "" || addPos.textBox2.Text == "" || addPos.textBox3.Text == "" || addPos.textBox4.Text == "")
            {
                MessageBox.Show("Нужно заполнить все данные");
                return;
            }
            Poset poset = new Poset();  
            poset.FIO = addPos.textBox1.Text;
            poset.Pas = Convert.ToInt64(addPos.textBox2.Text);
            poset.Nomber = Convert.ToInt64(addPos.textBox3.Text);   
            poset.Adress = addPos.textBox4.Text;
            db.Posets.Add(poset);
            db.SaveChanges();
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
                Poset poset = db.Posets.Find(id);
                AddPos addPos = new AddPos();

                addPos.textBox1.Text = poset.FIO;
                addPos.textBox2.Text = poset.Pas.ToString();
                addPos.textBox3.Text = poset.Nomber.ToString();
                addPos.textBox4.Text = poset.Adress.ToString();

                DialogResult result = addPos.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                if (addPos.textBox1.Text == "" || addPos.textBox2.Text == "" || addPos.textBox3.Text == "" || addPos.textBox4.Text == "")
                {
                    MessageBox.Show("Нужно заполнить все данные");
                    return;
                }
                poset.FIO = addPos.textBox1.Text;
                poset.Pas = Convert.ToInt64(addPos.textBox2.Text);
                poset.Nomber = Convert.ToInt64(addPos.textBox3.Text);
                poset.Adress = addPos.textBox4.Text;
                db.SaveChanges();
                dataGridView1.Refresh();
                MessageBox.Show("Объект обновлен");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ZapisiVse zapisiVse = new ZapisiVse();
            zapisiVse.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            DialogResult result = login.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            db.LoginDate.Load();
            foreach (var user in db.LoginDate)
            {
                if (user.Login == login.textBox1.Text && user.Password == login.textBox2.Text)
                {
                    ModulVrach modulVrach = new ModulVrach();
                    modulVrach.Show();
                }
                else
                {
                    MessageBox.Show("Данные не верны");
                }
            }

        }

    }
}
