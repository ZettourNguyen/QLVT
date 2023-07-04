using DevExpress.XtraReports.UI;
using QLVT.FormReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT.pnXprt
{
    public partial class PnXprtSelection : UserControl
    {

        public PnXprtSelection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PnDanhSachNhanVien dsnv = new PnDanhSachNhanVien();
            dsnv.Dock = DockStyle.Fill;
            pnXprtInput.Controls.Clear();
            pnXprtInput.Controls.Add(dsnv);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PnDanhSachVatTu dsvt = new PnDanhSachVatTu();
            dsvt.Dock = DockStyle.Fill;
            pnXprtInput.Controls.Clear();
            pnXprtInput.Controls.Add(dsvt);
        }

        private void btnPrintXprt_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PnDanhSachDDHnullPN pn = new PnDanhSachDDHnullPN();
            pn.Dock = DockStyle.Fill;
            pnXprtInput.Controls.Clear();
            pnXprtInput.Controls.Add(pn);
        }
    }
}
