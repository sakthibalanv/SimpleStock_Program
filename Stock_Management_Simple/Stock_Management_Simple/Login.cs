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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void bt_Clear_Click(object sender, EventArgs e)
        {
            tb_Password.Clear();
            tb_UserName.Text = "";
            tb_UserName.Focus();
        }

        private void bt_Login_Click(object sender, EventArgs e)
        {
            SqlConnection con = Connection.GetConnection();
            string query = "SELECT * FROM Login where UserName = '" + tb_UserName.Text + "' and Password = '" + tb_Password.Text + "'";
           // SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [dbo].[Login] where UserName='admin' and Password='admin@123'", con);
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid UserName and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bt_Clear_Click(sender, e);
            }
            // if we are using SqlDataAdapter connection open and close is automatic

        }

        /*
         * To create a database use sql server management studio and create a database and tables in it.
         * Set primary keys and create a database diagram in it
         * 
         * in the forms application use mdi form to create a main page
         * to add database connection, open server explorer and click connect
         * select sql server client type db and in the server name add the server name where you
         * create the database. and select the database from the list
         * 
         * use sqlconnection to connect to the database we can get the connection string from the properties window when 
         * clicked on the database on the server explorer
         */
    }
}
