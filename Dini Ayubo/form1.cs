using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dini_Ayubo
{
    public partial class form1 : Form
    {
        public form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string uname = txtuname.Text;
            string pass = txtpass.Text;

            //Loging from admin
            if (uname.Equals("Admin") && pass.Equals("123"))
            {
               Main m = new Main();
               this.Hide();
               m.Show(); //this form hide and show main form
            }

            else
            {
                //check add username and password 
                MessageBox.Show("Invailed User Name or Password");
                txtuname.Clear();
                txtpass.Clear();
                txtpass.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Login form close
            this.Close();
        }
    }
}
