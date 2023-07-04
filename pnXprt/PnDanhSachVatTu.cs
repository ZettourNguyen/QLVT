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
    public partial class PnDanhSachVatTu : UserControl
    {
        public PnDanhSachVatTu()
        {
            InitializeComponent();
        }

        private void PnDanhSachVatTu_Load(object sender, EventArgs e)
        {
            this.vattu1TableAdapter1.Fill(this.dataSet1.Vattu1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Validate();
            //this.vattuBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.dataSet);
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Xrpt_DanhSachVatTuCongTy report = new Xrpt_DanhSachVatTuCongTy();
            try
            {
                if (File.Exists(@"D:\ReportDanhSachVatTu.pdf"))
                {
                    DialogResult dr = MessageBox.Show("File ReportDanhSachVatTu.pdf tại ổ D đã có!\nBạn có muốn tạo lại?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        report.ExportToPdf(@"D:\ReportDanhSachVatTu.pdf");
                        MessageBox.Show("File ReportDSNhanVien.pdf đã được ghi thành công tại ổ D",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    report.ExportToPdf(@"D:\ReportDanhSachVatTu.pdf");
                    MessageBox.Show("File ReportDSNhanVien.pdf đã được ghi thành công tại ổ D",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Vui lòng đóng file ReportDanhSachVatTu.pdf",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            Xrpt_DanhSachVatTuCongTy inVatTu = new Xrpt_DanhSachVatTuCongTy();
            ReportPrintTool print = new ReportPrintTool(inVatTu);
            print.ShowPreviewDialog();
        }
    }
}
