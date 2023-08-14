using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Dini_Ayubo
{
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
            Customer_Load();
        }
        //Adding Sql connection for coding
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-3H6O7OJ0\SQLEXPRESS;Initial Catalog=Ayubo;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dtt;

        string proid;
        string sql;
        bool Mode = true;
        private string id;

        public int totdays, days, weeks, months, Total_rent, DriverCost = 0;

        private void Rental_Load(object sender, EventArgs e)
        {
            //the below function will be used for the load data for datagridviwer
            DisplayDataCustomer();
            DisplayDataCarreg();
        }
        public void Customer_Load()
        {
            cmd = new SqlCommand("SELECT * FROM customer", con);
            con.Open();
            dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                txtCustId.Items.Add(dr["CustId"].ToString());
            }
            //connection close
            con.Close();
        }
        private void DisplayDataCustomer()
        {
            //the below function will be used for the displaydata for above Customer
            //connection open
            con.Open();
            dtt = new DataTable();
            da = new SqlDataAdapter("SELECT * FROM customer", con);
            da.Fill(dtt);
            dataGridView1.DataSource = dtt;
            //connection close
            con.Close();
        }
        private void DisplayDataCarreg()
        {
            //the below function will be used for the displaydata for car model, type, make and other things
            con.Open();
            dtt = new DataTable();
            da = new SqlDataAdapter("SELECT * FROM carreg", con);
            da.Fill(dtt);
            dataGridView2.DataSource = dtt;
            //connection close
            con.Close();
        }
        private void ClearData()
        {
            //the below function will be used for the all data clear code
            txtCarId.Text ="";
            txtCustId.Text = "";
            txtDay.Text = "";
            txtMonth.Text = "";
            txtWeek.Text = "";
            txtVehi.Text = "";
            txtCId.Text = "";
            txtDrv.Text = "";
            txttotal.Text = "";
            StartDateTime.Text = "";
            EndDateTime.Text = "";            
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            //back button, this close and show main form
            Main m = new Main();
            this.Hide();
            m.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // the below function will be used for theall data clear
            ClearData();
        }
        private void btnCalculation_Click(object sender, EventArgs e)
        {
            //the below function will be used for the button calculation click event
            Total_days();
            if (checkBox1.Checked) // checking if the driver slected yes
            {
                if (txtCarId.Text == txtCId.Text)
                {
                    int DayFee = int.Parse(txtDay.Text);
                    int WeekFee = int.Parse(txtWeek.Text);
                    int MonthFee = int.Parse(txtMonth.Text);
                    int DriverCost = int.Parse(txtDrv.Text);
                    Total_rent = 0;
                    Total_rent = (MonthFee * months) + (WeekFee * weeks) + (DayFee * days) + (DriverCost * totdays);
                    txttotal.Text = string.Format("{0:C}", Total_rent);
                }
                else
                {
                    MessageBox.Show("Please Enter a Vehicle ID");
                }
            }
            else
            {
                if (txtCarId.Text == txtCId.Text)
                {
                    //get values
                    int DayFee = int.Parse(txtDay.Text);
                    int WeekFee = int.Parse(txtWeek.Text);
                    int MonthFee = int.Parse(txtMonth.Text);

                    Total_rent = 0;
                    Total_rent = (MonthFee * months) + (WeekFee * weeks) + (DayFee * days);
                    txttotal.Text = string.Format("{0:C}", Total_rent);
                }
                else
                {
                    MessageBox.Show("Please Enter a Vehicle ID");
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //close this form 
            this.Close();
        }

        public void Total_days()
        {
            //This is clculation coding part
            try
            {
                DateTime date1 = StartDateTime.Value.Date;
                DateTime date2 = EndDateTime.Value.Date;
                days = (date2 - date1).Days;
                totdays = days;
                while (true)
                {
                    if (days / 30 >= 1)
                    {
                        months = (days / 30);
                        days = days % 30;
                        continue;
                    }
                    else if (days / 7 >= 1)
                    {
                        weeks = (days / 7);
                        days = days % 7;
                        break;
                    }
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtcarid_TextChanged(object sender, EventArgs e)
        {
            //the below function will be used for the load data for text boxes
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-3H6O7OJ0\SQLEXPRESS;Initial Catalog=Ayubo;Integrated Security=True");
            //Connection open
            con.Open();
            //Get the values from table for the textboxes. [ex;dayfee, weekfee, monthfee, regno, drivefee] 
            if (txtCarId.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT Type, DayFee, WeekFee, MonthFee, RegNo, DriveFee from carreg WHERE RegNo = @RegNo", con);
                cmd.Parameters.AddWithValue("@RegNo", int.Parse(txtCarId.Text));
                SqlDataReader da = cmd.ExecuteReader();

                while (da.Read())
                {
                    txtVehi.Text = da.GetValue(0).ToString();
                    txtDay.Text = da.GetValue(1).ToString();
                    txtWeek.Text = da.GetValue(2).ToString();
                    txtMonth.Text = da.GetValue(3).ToString();
                    txtCId.Text = da.GetValue(4).ToString();
                    txtDrv.Text = da.GetValue(5).ToString();
                }
                //connection close
                con.Close();
            }
        }        
    }
}
