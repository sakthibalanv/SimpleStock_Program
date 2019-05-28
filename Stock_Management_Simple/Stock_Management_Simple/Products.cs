using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace Stock_Management_Simple
{
    public partial class Products : Form
    {
        

        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

            showDataTable();
        }

        private void bt_Add_Click(object sender, EventArgs e)
        {
            if (ifProductExists(tb_ProductCode.Text))
            {
                updateRecord();
            }
            else
            {
                addRecord();  
            }

                    

            showDataTable();            
            resetControls();
        }

        private void resetControls()
        {
            tb_ProductCode.Clear();
            tb_ProductName.Clear();
            comboBox1.SelectedIndex = -1;
            bt_Add.Text = "Add";
            tb_ProductCode.Focus();
        }

        private void updateRecord()
        {
            if (validation())
            {
                SqlConnection con = Connection.GetConnection();
                con.Open();
                bool status = comboBox1.SelectedIndex == 0 ? true : false;
                string query = "UPDATE [Products] SET [ProductName] = '" + tb_ProductName.Text + "', [ProductStatus] = '" + status + "' where [ProductCode] = '" + tb_ProductCode.Text + "'";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            
        }

        private bool ifProductExists(string productCode)
        {
            SqlConnection con = Connection.GetConnection();
            SqlDataAdapter sda = new SqlDataAdapter("select * from [Stock].[dbo].[Products] where [ProductCode] = '" + productCode + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        private void addRecord()
        {
            if (validation())
            {
                SqlConnection con = Connection.GetConnection();
                con.Open();
                bool status = comboBox1.SelectedIndex == 0 ? true : false;
                string query = "INSERT INTO Products ([ProductCode],[ProductName],[ProductStatus]) VALUES ('" + tb_ProductCode.Text + "','" + tb_ProductName.Text + "','" + status + "')";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            
        }

        private void showDataTable()
        {
            SqlConnection con = Connection.GetConnection();
            SqlDataAdapter sda = new SqlDataAdapter("select * from [Stock].[dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();

                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Inactive";
                }
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bt_Add.Text = "Update";

            tb_ProductCode.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            tb_ProductName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }

        private void bt_Delete_Click(object sender, EventArgs e)
        {
            if (ifProductExists(tb_ProductCode.Text))
            {
                deleteRecord();
                showDataTable();
            }
            else
            {
                MessageBox.Show("Record Doesnot exists");
            }
        }

        private void deleteRecord()
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (validation())
                {
                    SqlConnection con = Connection.GetConnection();
                    con.Open();
                    bool status = comboBox1.SelectedIndex == 0 ? true : false;
                    string query = "Delete From Products where [ProductCode] = '" + tb_ProductCode.Text + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                else
                {
                    MessageBox.Show("Record not found...");
                }
            }
           
        }

        private void bt_Reset_Click(object sender, EventArgs e)
        {
            resetControls();
        }

        private bool validation()
        {
            bool result = false;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(tb_ProductCode.Text))
            {
                errorProvider1.SetError(tb_ProductCode, "Product Code Required");
            }
            else if (string.IsNullOrEmpty(tb_ProductName.Text))
            {
                errorProvider1.SetError(tb_ProductName, "Product Name Required");
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBox1, "Select Status");
            }
            else
            {
                result = true;
            }            
            return result;
        }
    }
}
