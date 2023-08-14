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
    public partial class Hire : Form
    {
        //Add the SQL Connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-3H6O7OJ0\SQLEXPRESS;Initial Catalog=Ayubo;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dtt;

        public int Tot_time, Extra_hours, Extra_kms, total_km, Tot_kms, Tot_days, Tot_nights, lt_ext_km, Total_hire_charge = 0;
        public String km_per_day_package;
        public Hire()
        {
            //Customer and Vehicle information load (Customer id and Vehicle id)
            InitializeComponent();
            customer_load();
            Vehicle_Load();
        }

        private void txtPackageID_TextChanged(object sender, EventArgs e)
        {
            //Connection Open
            con.Open();
            if (txtPackageID.Text != "")
            {
                //the below function will be used for the package details load for the all text boxes from PackageId
                SqlCommand cmd = new SqlCommand("SELECT Package_Name, Total_Km, Total_Hour, Ext_Km_Charge, Ext_Hour_Charge, Basic_Hire_Charge, Overnight_Stay_Charge, Night_Parking_Charge, PackageId, DriverFee FROM Hire_Packages WHERE PackageId = @PackageId", con);
                cmd.Parameters.AddWithValue("@PackageId", int.Parse(txtPackageID.Text));
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtPackName.Text = dr.GetValue(0).ToString();
                    txtTotKm.Text = dr.GetValue(1).ToString();
                    txtTotHrs.Text = dr.GetValue(2).ToString();
                    txtExtraKmChrg.Text = dr.GetValue(3).ToString();
                    txtExtraHrsChrg.Text = dr.GetValue(4).ToString();
                    txtHireCharge.Text = dr.GetValue(5).ToString();
                    txtOverNightCharge.Text = dr.GetValue(6).ToString();
                    txtNightParking.Text = dr.GetValue(7).ToString();
                    txtDetailPackageId.Text = dr.GetValue(8).ToString();
                    txtDrvCost.Text = dr.GetValue(9).ToString();
                }
            }
            //connection close
            con.Close();
        }

        private void btnLongCal_Click(object sender, EventArgs e)
        {
            //the below function will be used for the long Calculation button click
            Total_days();
            Total_km_lt();

            int total_km = int.Parse(txtTotKm.Text);
            int extra_km = int.Parse(txtExtraKmChrg.Text);
            int base_hire = int.Parse(txtHireCharge.Text);
            int ot_charge = int.Parse(txtOverNightCharge.Text);
            int nt_charge = int.Parse(txtNightParking.Text);
            int driver_fee = int.Parse(txtDrvCost.Text);

            if (txtPackageID.Text == txtDetailPackageId.Text)
            {
                txtLongHireCharge.Text = (base_hire * Tot_days).ToString();
                txtLongOverNightCharge.Text = (ot_charge * Tot_nights).ToString();
                txtLongExKmCharge.Text = (lt_ext_km * extra_km).ToString();
                txtLongDriveCost.Text = (driver_fee * Tot_days).ToString();
                Total_hire_charge = (base_hire * Tot_days) + (Extra_kms * extra_km) + (driver_fee * Tot_days) + (ot_charge * Tot_nights) + (nt_charge * Tot_nights);
                txtLongCost.Text = Total_hire_charge.ToString();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //the below function will be used for the back button click 
            Main m = new Main();
            this.Hide();
            m.Show();
        }

        private void Hire_Load(object sender, EventArgs e)
        {
            //the below function will be used for the Customer details and vehicle details display for the datagridview
            DisplayDataVehicles();
            DisplayDataCustomer();
        }

        private void btnDayCal_Click(object sender, EventArgs e)
        {
            //Day Calculation button click
            Total_time_hun();
            Total_km();
           
            int extra_km = int.Parse(txtExtraKmChrg.Text);
            int extra_hour = int.Parse(txtExtraHrsChrg.Text);
            int base_hire = int.Parse(txtHireCharge.Text);
            int ot_charge = int.Parse(txtOverNightCharge.Text);
            int nt_charge = int.Parse(txtNightParking.Text);
            int driver_fee = int.Parse(txtDrvCost.Text);
            string package = txtPackName.Text.ToString();

            if (txtPackageID.Text == txtDetailPackageId.Text)
            {
                txtDayHireCharge.Text = base_hire.ToString();
                txtDayWaitCharge.Text = (Extra_hours * extra_hour).ToString();
                txtDayExKmCharge.Text = (Extra_kms * extra_km).ToString();
                txtDayDriveCost.Text = driver_fee.ToString();
                Total_hire_charge = base_hire + (Extra_hours * extra_hour) + (Extra_kms * extra_km) + driver_fee;
                txtDayCost.Text = Total_hire_charge.ToString();
            }
            else
            {
                MessageBox.Show("Please Enter a Valid Package ID");
                ClearData();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Hire form close
            this.Close();
        }

        private void btnDayClear_Click(object sender, EventArgs e)
        {
            //Clear data button, clear all datails
            ClearData();
        }

        private void ClearData()
        {
            //Clear data codes
            txtVehiId.Text = "";
            txtPackageID.Text = "";
            dayStartTime.Text = "";
            dayEndTime.Text = "";
            txtDayStartKmRead.Text = "";
            txtDayEndKmRead.Text = "";
            txtDayEndKmRead.Text = "";
            txtLgStartKmRead.Text = "";
            txtLgEndKmRead.Text = "";
            txtLgStartKmRead.Text = "";
            txtLgEndKmRead.Text = "";
            txtDetailPackageId.Text = "";
            txtPackName.Text = "";
            txtTotKm.Text = "";
            txtTotHrs.Text = "";
            txtExtraKmChrg.Text = "";
            txtExtraHrsChrg.Text = "";
            txtHireCharge.Text = "";
            txtOverNightCharge.Text = "";
            txtNightParking.Text = "";
            txtDrvCost.Text = "";
            txtDayHireCharge.Text = "";
            txtDayWaitCharge.Text = "";
            txtDayExKmCharge.Text = "";
            txtDayDriveCost.Text = "";
            txtDayCost.Text = "";
            txtLongHireCharge.Text = "";
            txtLongOverNightCharge.Text = "";
            txtLongExKmCharge.Text = "";
            txtLongDriveCost.Text = "";
            txtLongCost.Text = "";
            txtCusId.Text = "";
        }

        public void Total_time_hun()
        {
            //Find the total time from here
            int total_hour = int.Parse(txtTotHrs.Text);

            TimeSpan Tt = this.dayEndTime.Value - this.dayStartTime.Value;
            Tot_time = Tt.Hours;

            if (Tot_time > total_hour)
            {
                Extra_hours = (this.Tot_time - total_hour);
            }
            else
            {
                Extra_hours = 0;
            }
        }

        public void Total_days()
        {
            //Find the total days from here
            DateTime date1 = longStartDate.Value.Date;
            DateTime date2 = longEndDate.Value.Date;
            Tot_days = (date2 - date1).Days;

            if (Tot_days > 1)
            {
                Tot_nights = Tot_days - 1;
            }
            else
            {
                Tot_nights = 0;
            }
        }
        public void Total_km()
        {
            //Find Total KM from here
            Total_days();
            int max_km = int.Parse(txtTotKm.Text) * (Tot_days);
            Tot_kms = (int.Parse(txtDayEndKmRead.Text) - int.Parse(txtDayStartKmRead.Text) );

            if (Tot_kms > max_km)
            {
                Extra_kms = Tot_kms - max_km;
            }
            else
            {
                Extra_kms = 0;
            }
        }
        public void Total_km_lt()
        {
            //Total km for long tour
            int lt_km = int.Parse(txtTotKm.Text);
            int lt_km_tot = int.Parse(txtLgEndKmRead.Text) - int.Parse(txtLgStartKmRead.Text);

            if (lt_km_tot > lt_km)
            {
                lt_ext_km = lt_km_tot - lt_km;
            }
            else
            {
                lt_ext_km = 0;
            }
        }
        private void DisplayDataVehicles()
        {
            //Displaydata for above Customer
            //connection open
            con.Open();
            dtt = new DataTable();
            da = new SqlDataAdapter("SELECT * FROM carreg", con);
            da.Fill(dtt);
            dataGridView2.DataSource = dtt;
            //connection close
            con.Close();
        }
        private void DisplayDataCustomer()
        {
            //Displaydata for above Customer
            //connection open
            con.Open();
            dtt = new DataTable();
            da = new SqlDataAdapter("SELECT * FROM customer", con);
            da.Fill(dtt);
            dataGridView1.DataSource = dtt;
            //connection close
            con.Close();
        }
        public void customer_load()
        {
            //Customers load 
            cmd = new SqlCommand("SELECT * FROM customer", con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtCusId.Items.Add(dr["CustId"].ToString());
            }
            //connection close
            con.Close();
        }
        public void Vehicle_Load()
        {
            //Vehicles load
            cmd = new SqlCommand("SELECT * FROM carreg", con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtVehiId.Items.Add(dr["RegNo"].ToString());
            }
            //connection close
            con.Close();
        }
    }
}