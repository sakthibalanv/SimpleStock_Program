using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Management_Simple
{
    public partial class StockMain : Form
    {
       

        public StockMain()
        {
            InitializeComponent();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products prod = new Products();
            prod.MdiParent = this;
            prod.Show();
        }

        bool close = true;
        private void StockMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to exit ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    close = false;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            
            
        }

        private void productListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportForm.ProductReport rpt = new ReportForm.ProductReport();
            rpt.MdiParent = this;
            rpt.Show();
        }

        private void stockListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportForm.StockReport rpt = new ReportForm.StockReport();
            rpt.MdiParent = this;
            rpt.Show();
        }

       
    }
}
