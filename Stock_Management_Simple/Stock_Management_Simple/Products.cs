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
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-391RUUKR\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");

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
        }

        private void updateRecord()
        {
            con.Open();
            bool status = comboBox1.SelectedIndex == 0 ? true : false;
            string query = "UPDATE [Products] SET [ProductName] = '" + tb_ProductName.Text + "', [ProductStatus] = '" + status + "' where [ProductCode] = '" + tb_ProductCode.Text + "'" ;

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        private bool ifProductExists(string productCode)
        {
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
            con.Open();
            bool status = comboBox1.SelectedIndex == 0 ? true : false;
            string query = "INSERT INTO Products ([ProductCode],[ProductName],[ProductStatus]) VALUES ('" + tb_ProductCode.Text + "','" + tb_ProductName.Text + "','" + status + "')";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        private void showDataTable()
        {
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
            con.Open();
            bool status = comboBox1.SelectedIndex == 0 ? true : false;
            string query = "Delete From Products where [ProductCode] = '" + tb_ProductCode.Text + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}
