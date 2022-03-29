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
    public partial class AddVr : Form
    {
        StomContext db;
        public AddVr()
        {
            InitializeComponent();
            db = new StomContext();
            db.Vraches.Load();
            dataGridView1.DataSource = db.Vraches.Local.ToBindingList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.SaveChanges();
            dataGridView1.Refresh();
        }
    }
}
