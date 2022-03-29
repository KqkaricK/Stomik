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
    public partial class AddYslygi : Form
    {
        StomContext db;
        public AddYslygi()
        {
            InitializeComponent();
            db = new StomContext();
            db.Yslygis.Load();
            dataGridView1.DataSource = db.Yslygis.Local.ToBindingList();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            db.SaveChanges();
            dataGridView1.Refresh();
        }
    }
}
