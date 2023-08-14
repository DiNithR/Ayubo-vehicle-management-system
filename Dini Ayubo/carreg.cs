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
    public partial class Carreg : Form
    {
        public Carreg()
        {
            InitializeComponent();
            Auto_No();
            load();
        }
        //sql connection add
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-3H6O7OJ0\SQLEXPRESS;Initial Catalog=Ayubo;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        private string id;

        public void Auto_No()
        {
            //Auto number generate
            sql = "SELECT RegNo FROM carreg ORDER BY RegNo desc";
            cmd = new SqlCommand(sql, con);
            //connection open
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("0000");
            }
            else if (Convert.IsDBNull(dr))
            {
                proid = ("0000");
            }
            else
            {
                proid = ("0000");
            }

            txtRegNo.Text = proid.ToString();

            //connection close
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //adding values for text boxes
            string RegNo = txtRegNo.Text;
            string Make = txtMake.Text;
            string Model = txtModel.Text;
            string Type = txtType.Text;
            string Aval = txtAvl.SelectedItem.ToString();
            string VehiNo = txtVehiNo.Text;
            string MonthFee = txtMonth.Text;
            string WeekFee = textBox2.Text;
            string DayFee = txtDay.Text;
            string DriveFee = txtDrive.Text;

            if (Mode == true)
            {
                //passing values for the tabale 
                sql = "INSERT INTO carreg (RegNo,Make,Model,Type,Available,VehiNo,MonthFee,WeekFee,DayFee,DriveFee) VALUES (@RegNo,@Make,@Model,@Type,@Available,@VehiNo,@MonthFee,@WeekFee,@DayFee,@DriveFee)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@RegNo", RegNo);
                cmd.Parameters.AddWithValue("@Make", Make);
                cmd.Parameters.AddWithValue("@Model", Model);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Available", Aval);
                cmd.Parameters.AddWithValue("@Vehino", VehiNo);
                cmd.Parameters.AddWithValue("@MonthFee", MonthFee);
                cmd.Parameters.AddWithValue("@WeekFee", WeekFee);
                cmd.Parameters.AddWithValue("@DayFee", DayFee);
                cmd.Parameters.AddWithValue("@DriveFee", DriveFee);
                cmd.ExecuteNonQuery();

                //message for after record adding
                MessageBox.Show("Record Successfully");

                //text boxes clear
                txtMake.Clear();
                txtModel.Clear();
                txtType.Clear();
                txtVehiNo.Clear();
                txtMonth.Clear();
                textBox2.Clear();
                txtDay.Clear();
                txtDrive.Clear();
                txtMake.Focus();
                txtAvl.Refresh();

            }
            else
            {
                //update the records
                sql = "UPDATE carreg SET Make = @Make, Model = @Model, Type = @Type, Available = @Available, VehiNo = @VehiNo, MonthFee =@MonthFee, WeekFee = @WeekFee, DayFee = @DayFee, DriveFee = @DriveFee WHERE RegNo = @RegNo";
                con.Open();
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@RegNo", id);
                cmd.Parameters.AddWithValue("@Make", Make);
                cmd.Parameters.AddWithValue("@Model", Model);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Available", Aval);
                cmd.Parameters.AddWithValue("@VehiNo", VehiNo);
                cmd.Parameters.AddWithValue("@MonthFee", MonthFee);
                cmd.Parameters.AddWithValue("@WeekFee", WeekFee);
                cmd.Parameters.AddWithValue("@DayFee", DayFee);
                cmd.Parameters.AddWithValue("@DriveFee", DriveFee);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successfully");
                txtRegNo.Enabled = true;
                Mode = true;
                //Clear data
                txtMake.Clear();
                txtModel.Clear();
                txtType.Clear();
                txtVehiNo.Clear();
                txtMonth.Clear();
                textBox2.Clear();
                txtDay.Clear();
                txtDrive.Clear();
                txtMake.Focus();

            }
            //connection close
            con.Close();
        }

        public void load()
        {
            sql = "SELECT * FROM carreg";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9]);
            }
            //Connection close
            con.Close();
        }
        public void Getid(string regno)
        {
            sql = @"SELECT * 
                    FROM carreg 
                    WHERE RegNo = '" + id + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtRegNo.Text = dr[0].ToString();
                txtMake.Text = dr[1].ToString();
                txtModel.Text = dr[2].ToString();
                txtType.Text = dr[3].ToString();
                txtAvl.Text = dr[4].ToString();
                txtVehiNo.Text = dr[5].ToString();
                txtMonth.Text = dr[6].ToString();
                textBox2.Text = dr[7].ToString();
                txtDay.Text = dr[8].ToString();
                txtDrive.Text = dr[9].ToString();

            }
            //Connection close
            con.Close();
        }

        private void carreg_Load(object sender, EventArgs e)
        {
            //txtavl.Items.Add("Yes");           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Update data form datagridview
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                txtRegNo.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                Getid(id);

            }
            //Delete record this from datagridview
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "DELETE FROM carreg WHERE RegNo = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Successfully");
                con.Close();

            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Button refresh, all text boxes clear
            load();
            Auto_No();
            txtMake.Clear();
            txtModel.Clear();
            txtType.Clear();
            txtVehiNo.Clear();
            txtMonth.Clear();
            textBox2.Clear();
            txtDay.Clear();
            txtDrive.Clear();
            txtMake.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Close the from
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
