using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dini_Ayubo
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Carreg m = new Carreg();
            this.Hide();
            m.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            customer m = new customer();
            this.Hide();
            m.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rental m = new Rental();
            this.Hide();
            m.Show();
        }

        private void btnHire_Click(object sender, EventArgs e)
        {
            Hire H = new Hire();
            this.Hide();
            H.Show();
        }

        private void btnHirePackages_Click(object sender, EventArgs e)
        {
            Hire_Packages H = new Hire_Packages();
            this.Hide();
            H.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
