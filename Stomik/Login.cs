using System;
using System.Windows.Forms;

namespace Stomik
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == true)
            {
                textBox2.UseSystemPasswordChar = false;
                label3.Text = "Спрятать пароль";
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                label3.Text = "Показать пароль";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
