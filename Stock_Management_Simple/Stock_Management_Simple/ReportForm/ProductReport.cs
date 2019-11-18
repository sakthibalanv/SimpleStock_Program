//using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Management_Simple.ReportForm
{
    public partial class ProductReport : Form
    {
        //ReportDocument crrpt = new ReportDocument();

        public ProductReport()
        {
            InitializeComponent();
        }

        private void ProductReport_Load(object sender, EventArgs e)
        {
            crrpt.Load(@"B:\Git_VS_Projects\SimpleStock_Program\Stock_Management_Simple\Stock_Management_Simple\Reports\Product.rpt");
            SqlConnection con = Connection.GetConnection();
            con.Open();
            DataSet dst = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter("select * from [Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            crrpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = crrpt;
        }
    }
}
