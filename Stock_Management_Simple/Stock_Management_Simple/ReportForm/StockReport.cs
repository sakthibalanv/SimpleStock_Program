using CrystalDecisions.CrystalReports.Engine;
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
    public partial class StockReport : Form
    {
        ReportDocument crrpt = new ReportDocument();

        public StockReport()
        {
            InitializeComponent();
        }

        private void bt_ShowReport_Click(object sender, EventArgs e)
        {
            crrpt.Load(@"B:\Git_VS_Projects\SimpleStock_Program\Stock_Management_Simple\Stock_Management_Simple\Reports\Stock.rpt");
            SqlConnection con = Connection.GetConnection();
            con.Open();
            DataSet dst = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter("select * from [Stock] where Cast(TransDate as Date) between '" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' and '" +
                dateTimePicker2.Value.ToString("MM/dd/yyyy") + "'", con);
            sda.Fill(dst, "Stock");
            crrpt.SetParameterValue("@FromDate", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            crrpt.SetParameterValue("@ToDate", dateTimePicker2.Value.ToString("dd/MM/yyyy"));
            crrpt.SetDataSource(dst);
            crystalReportViewer1.ReportSource = crrpt;
        }
    }
}
