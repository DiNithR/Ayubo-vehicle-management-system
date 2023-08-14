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
    public partial class customer : Form
    {
        public customer()
        {
            InitializeComponent();
            Autono();
            customerload();
        }
        //Connection adding part
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-3H6O7OJ0\SQLEXPRESS;Initial Catalog=Ayubo;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        private string id;

        public void Autono()
        {
            //Auto number generat part for customer ID
            sql = "SELECT custid from customer ORDER by CustId desc";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("0000");
            }
            else if (Convert.IsDBNull(dr))
            {
                proid = ("0001");
            }
            else
            {
                proid = ("0001");
            }

            txtId.Text = proid.ToString();
            //Connection close
            con.Close();
        }

        public void customerload()
        {
            //Get the Customer information for datagridview
            sql = "SELECT * FROM customer";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
            }
            //connection close
            con.Close();
        }


        public void Getid(string regno)
        {
            sql = "SELECT * FROM customer WHERE CustId = '" + id + "'";
            //Creat the sql command
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtId.Text = dr[0].ToString();
                txtName.Text = dr[1].ToString();
                txtAddress.Text = dr[2].ToString();
                txtMobile.Text = dr[3].ToString();
                
            }
            con.Close();
        }


        private void customer_Load(object sender, EventArgs e)
        {
            //**//
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Edit click from datagridview
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                txtId.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                Getid(id);

            }
            //Delete click from datagridview
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "DELETE from customer WHERE CustId = @id";
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
            //Refresh the all add data and all dat
            customerload();
            Autono();
            txtName.Clear();
            txtAddress.Clear();
            txtMobile.Clear();
            txtName.Focus();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            //Go to the main form agin click event
            Main m = new Main();
            this.Hide();
            m.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //This application closing
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Add information for tables
            string CustId = txtId.Text;
            string CustName = txtName.Text;
            string Address = txtAddress.Text;
            string Mobile = txtMobile.Text;


            if (Mode == true)
            {
                sql = "INSERT INTO customer(CustId,CustName,Address,Mobile) VALUES(@CustId,@CustName,@Address,@Mobile)";
                con.Open();
                //Create the sql command
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Custid", CustId);
                cmd.Parameters.AddWithValue("@Custname", CustName);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Mobile", Mobile);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Record Successfully");

                txtName.Clear();
                txtAddress.Clear();
                txtMobile.Clear();
                txtName.Focus();
            }
            else
            {
                //Update values, before add data
                sql = "UPDATE customer set CustName = @CustName, Address = @Address, Mobile = @Mobile WHERE CustId = @CustId";
                con.Open();
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@CustId", id);
                cmd.Parameters.AddWithValue("@CustName", CustName);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Mobile", Mobile);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successfully");
                txtId.Enabled = true;
                Mode = true;

                //Text clear
                txtName.Clear();
                txtAddress.Clear();
                txtMobile.Clear();
                txtName.Focus();


            }
            //connection close
            con.Close();
        }
    }
}
