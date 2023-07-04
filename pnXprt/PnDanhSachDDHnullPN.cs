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
    public partial class PnDanhSachDDHnullPN : UserControl
    {
        public PnDanhSachDDHnullPN()
        {
            InitializeComponent();
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            Xprt_DanhSachDDHnullPN inDS = new Xprt_DanhSachDDHnullPN();
            if(Program.role == "CONGTY")
            {
                inDS.lblCN.Text = "CỦA CÔNG TY";

            }
            else
            {
                if (Program.serverName.Contains("1"))
                {
                    inDS.lblCN.Text = "CỦA CHI NHÁNH 1";
                }
                else
                {
                    inDS.lblCN.Text = "CỦA CHI NHÁNH 2";
                }

            }
            ReportPrintTool print = new ReportPrintTool(inDS);
            print.ShowPreviewDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Xprt_DanhSachDDHnullPN report = new Xprt_DanhSachDDHnullPN();
            try
            {
                if (File.Exists(@"D:\ReportDanhSachDonDatHangChuacoPhieuNhap.pdf"))
                {
                    DialogResult dr = MessageBox.Show("File ReportDonDatHangChuacoPhieuNhap.pdf tại ổ D đã có!\nBạn có muốn tạo lại?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        report.ExportToPdf(@"D:\ReportDanhSachDonDatHangChuacoPhieuNhap.pdf");
                        MessageBox.Show("File ReportDonDatHangChuacoPhieuNhap.pdf đã được ghi thành công tại ổ D",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    report.ExportToPdf(@"D:\ReportDanhSachDonDatHangChuacoPhieuNhap.pdf");
                    MessageBox.Show("File ReportDonDatHangChuacoPhieuNhap.pdf đã được ghi thành công tại ổ D",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Vui lòng đóng file ReportDanhSachDonDatHangChuacoPhieuNhap.pdf",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
        }

        private void PnDanhSachDDHnullPN_Load(object sender, EventArgs e)
        {

        }
    }
}
