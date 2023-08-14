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
    public partial class Hire_Packages : Form
    {
        public Hire_Packages()
        {
            InitializeComponent();
            Auto_No();
        }
        //Sql connection adding
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-3H6O7OJ0\SQLEXPRESS;Initial Catalog=Ayubo;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        private string id;
        SqlDataAdapter da;
        DataTable dtt;

        private void Hire_Packages_Load(object sender, EventArgs e)
        {
            //the below function will be used for the display data in gridview
            DisplayDataPackages();
        }
        public void Auto_No()
        {
            //Auto number generate
            sql = "SELECT PackageID FROM Hire_Packages ORDER BY PackageID desc";
            cmd = new SqlCommand(sql, con);
            //connection open
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("0");
            }
            else if (Convert.IsDBNull(dr))
            {
                proid = ("0");
            }
            else
            {
                proid = ("0");
            }

            txtPackId.Text = proid.ToString();

            //connection close
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //try catch part
            try
            { //starting brace
                //the below function will be used for passing values for the tabale 
                cmd = new SqlCommand("INSERT INTO Hire_Packages (PackageId, Package_Name, Total_Km, Total_Hour, Ext_Km_Charge, Ext_Hour_Charge, Basic_Hire_Charge, Overnight_Stay_Charge, Night_Parking_Charge, DriverFee) values (@PackageId, @Package_Name, @Total_Km, @Total_Hour, @Ext_Km_Charge, @Ext_Hour_Charge, @Basic_Hire_Charge, @Overnight_Stay_Charge, @Night_Parking_Charge, @DriverFee)", con);
                con.Open();
                cmd.Parameters.AddWithValue("PackageId", txtPackId.Text);
                cmd.Parameters.AddWithValue("Package_Name", txtPackName.Text);
                cmd.Parameters.AddWithValue("Total_Km", txtTotKm.Text);
                cmd.Parameters.AddWithValue("Total_Hour", txtTotHrs.Text);
                cmd.Parameters.AddWithValue("Ext_Km_Charge", txtExKmCharge.Text);
                cmd.Parameters.AddWithValue("Ext_Hour_Charge", txtExHrsCharge.Text);
                cmd.Parameters.AddWithValue("Basic_Hire_Charge", txtHireCharge.Text);
                cmd.Parameters.AddWithValue("Overnight_Stay_Charge", txtOverNightCharge.Text);
                cmd.Parameters.AddWithValue("Night_Parking_Charge", txtNightParkCharge.Text);
                cmd.Parameters.AddWithValue("DriverFee", txtDriveCost.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                //message for after record adding
                MessageBox.Show("Record Successfully");
                ClearData();
                DisplayDataPackages();
            } //ending brace 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //the below function will be used for the all data update from Edit click
            if ( txtPackId.Text != "")
            { //starting brace
                cmd = new SqlCommand("UPDATE Hire_Packages SET Package_Name =@Package_Name, Total_Km = @Total_Km, Total_Hour =@Total_Hour, Ext_Km_Charge = @Ext_Km_Charge, Ext_Hour_Charge = @Ext_Hour_Charge, Basic_Hire_Charge = @Basic_Hire_Charge, Overnight_Stay_Charge = @Overnight_Stay_Charge, Night_Parking_Charge = @Night_Parking_Charge, DriverFee = @DriverFee WHERE PackageId = @PackageId", con);
                con.Open();
                cmd.Parameters.AddWithValue("PackageId", txtPackId.Text);
                cmd.Parameters.AddWithValue("Package_Name", txtPackName.Text);
                cmd.Parameters.AddWithValue("Total_Km", txtTotKm.Text);
                cmd.Parameters.AddWithValue("Total_Hour", txtTotHrs.Text);
                cmd.Parameters.AddWithValue("Ext_Km_Charge", txtExKmCharge.Text);
                cmd.Parameters.AddWithValue("Ext_Hour_Charge", txtExHrsCharge.Text);
                cmd.Parameters.AddWithValue("Basic_Hire_Charge", txtHireCharge.Text);
                cmd.Parameters.AddWithValue("Overnight_Stay_Charge", txtOverNightCharge.Text);
                cmd.Parameters.AddWithValue("Night_Parking_Charge", txtNightParkCharge.Text);
                cmd.Parameters.AddWithValue("DriverFee", txtDriveCost.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                //Record added successfully messages
                MessageBox.Show("Record Updated Successfully");
                ClearData(); //After added data clear data all text boxes
                DisplayDataPackages(); //After added data show the gridview
            } //ending brace 
            else
            {
                MessageBox.Show("Please Select a Record to Update");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Delete click
            if(txtPackId.Text != "")
            {
                cmd = new SqlCommand("DELETE Customer WHERE PackageId=@PackageId", con);
                con.Open();
                cmd.Parameters.AddWithValue("@PackageId", txtPackId.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                ClearData();
                DisplayDataPackages();
            }
            else
            {
                MessageBox.Show("Please Select a Record to Delete");
            }
        }
        private void ClearData()
        {
            //Clear all date from here
            txtPackName.Clear();
            txtTotKm.Clear();
            txtTotHrs.Clear();
            txtExKmCharge.Clear();
            txtExHrsCharge.Clear();
            txtHireCharge.Clear();
            txtOverNightCharge.Clear();
            txtNightParkCharge.Clear();
            txtDriveCost.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Data clear click button
            ClearData();
        }
        private void DisplayDataPackages()
        {
            //Displaydata for above Customer
            //connection open
            con.Open();
            dtt = new DataTable();
            da = new SqlDataAdapter("SELECT * FROM Hire_Packages", con);
            da.Fill(dtt);
            dataGridView1.DataSource = dtt;
            //connection close
            con.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //This from close
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //back click.
            Main m = new Main();
            this.Hide();
            m.Show();
        }
    }

}
