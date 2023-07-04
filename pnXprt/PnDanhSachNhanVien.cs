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
using DevExpress.XtraReports.UI;
using QLVT.FormReport;
using System.IO;

namespace QLVT.pnXprt
{
    public partial class PnDanhSachNhanVien : UserControl
    {
        public PnDanhSachNhanVien()
        {
            InitializeComponent();
        }

        ///  cbb chinhanh
        private void PnDanhSachNhanVien_Load(object sender, EventArgs e)
        {
            
            if (Program.role.Contains("CONGTY")){
                cbbCN.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else
            {
                cbbCN.Enabled = false;
            }
            if (KetNoiDatabaseGoc() == 0)
                return;
            layDanhSachPhanManh("SELECT TOP 2 * FROM sp_GetSubscriptions");
            cbbCN.SelectedIndex = 1;
            cbbCN.SelectedIndex = 0;
            
        }
        private SqlConnection connPublisher = new SqlConnection();
        private void layDanhSachPhanManh(String cmd)
        {
            if (connPublisher.State == ConnectionState.Closed)
            {
                connPublisher.Open();
            }
            DataTable dt = new DataTable();
            // adapter dùng để đưa dữ liệu từ view sang database
            SqlDataAdapter da = new SqlDataAdapter(cmd, connPublisher);
            // dùng adapter thì mới đổ vào data table được
            da.Fill(dt);
            connPublisher.Close();
            Program.bindingSource.DataSource = dt;
            cbbCN.DataSource = Program.bindingSource;
            cbbCN.DisplayMember = "TENCN";
            cbbCN.ValueMember = "TENSERVER";
        }
        private int KetNoiDatabaseGoc()
        {
            if (connPublisher != null && connPublisher.State == ConnectionState.Open)
                connPublisher.Close();
            try
            {
                connPublisher.ConnectionString = Program.connstrPublisher;
                connPublisher.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }

        private void cbbCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCN.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            Program.serverName = cbbCN.SelectedValue.ToString();

            /*Neu chon sang chi nhanh khac voi chi nhanh hien tai*/
            if (cbbCN.SelectedIndex != Program.brand)
            {
                Program.loginName = Program.remoteLogin;
                Program.loginPassword = Program.remotePassword;
            }
            /*Neu chon trung voi chi nhanh dang dang nhap o formDangNhap*/
            else
            {
                Program.loginName = Program.currentLogin;
                Program.loginPassword = Program.currentPassword;
            }

            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Xảy ra lỗi kết nối với chi nhánh hiện tại", "Thông báo", MessageBoxButtons.OK);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Xrpt_DanhSachNhanVien report = new Xrpt_DanhSachNhanVien();
            try
            {
                if (File.Exists(@"D:\ReportDanhSachNhanVien.pdf"))
                {
                    DialogResult dr = MessageBox.Show("File ReportDanhSachNhanVien.pdf tại ổ D đã có!\nBạn có muốn tạo lại?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        report.ExportToPdf(@"D:\ReportDanhSachNhanVien.pdf");
                        MessageBox.Show("File ReportDanhSachNhanVien.pdf đã được ghi thành công tại ổ D",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    report.ExportToPdf(@"D:\ReportDanhSachNhanVien.pdf");
                    MessageBox.Show("File ReportDanhSachNhanVien.pdf đã được ghi thành công tại ổ D",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Vui lòng đóng file ReportDanhSachNhanVien.pdf",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            Xrpt_DanhSachNhanVien inNV = new Xrpt_DanhSachNhanVien();
            inNV.lblTitle.Text = "DANH SÁCH NHÂN VIÊN "+cbbCN.Text.ToUpper();
            ReportPrintTool print = new ReportPrintTool(inNV);
            print.ShowPreviewDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        ////



    }
}
