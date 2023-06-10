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

namespace QLVT
{
    public partial class FormChinh : Form
    {
        private SqlConnection connPublisher = new SqlConnection();

        public FormChinh()
        {
            InitializeComponent();
        }
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


            cmbCHINHANH.DataSource = Program.bindingSource;
            cmbCHINHANH.DisplayMember = "TENCN";
            cmbCHINHANH.ValueMember = "TENSERVER";
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
        private void Form1_Load(object sender, EventArgs e)
        {
            QuanLyTk form = new QuanLyTk();
            form.Dock = DockStyle.Fill;
            pnHETHONG.Controls.Clear();
            pnHETHONG.Controls.Add(form);

        }
        private void btnNhanVien_Click_1(object sender, EventArgs e)
        {
            NhanVien form = new NhanVien();
            form.Dock = DockStyle.Fill;
            controlPn.Controls.Clear();
            controlPn.Controls.Add(form);
        }

      

        private void bntVatTu_Click(object sender, EventArgs e)
        {
            VatTu form = new VatTu();
            form.Dock = DockStyle.Fill;
            controlPn.Controls.Clear();
            controlPn.Controls.Add(form);
        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            Kho form = new Kho();
            form.Dock = DockStyle.Fill;
            controlPn.Controls.Clear();
            controlPn.Controls.Add(form);
        }

        private void btnDDH_Click(object sender, EventArgs e)
        {
            donDatHang form = new donDatHang();
            form.Dock = DockStyle.Fill;
            controlPn.Controls.Clear();
            controlPn.Controls.Add(form);

        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            PhieuNhap form = new PhieuNhap();
            form.Dock = DockStyle.Fill;
            controlPn.Controls.Clear();
            controlPn.Controls.Add(form);
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            PhieuXuat form = new PhieuXuat();
            form.Dock = DockStyle.Fill;
            controlPn.Controls.Clear();
            controlPn.Controls.Add(form);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void hethong_Click(object sender, EventArgs e)
        {
            QuanLyTk form = new QuanLyTk();
            form.Dock = DockStyle.Fill;
            pnHETHONG.Controls.Clear();
            pnHETHONG.Controls.Add(form);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.role != "CONGTY")
            {
                cmbCHINHANH.Hide();
            }
            else
            {
                try
                {
                    if (KetNoiDatabaseGoc() == 0)
                        return;
                    layDanhSachPhanManh("SELECT TOP 2 * FROM sp_GetSubscriptions");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            pnHETHONG.Controls.Clear();
            NhanVien form = new NhanVien();
            form.Dock = DockStyle.Fill;
            pnHETHONG.Controls.Add(pnControl);
            pnHETHONG.Controls.Add(controlPn);

            controlPn.Controls.Clear();
            controlPn.Controls.Add(form);
        }
        public void enableButtons(int t)
        {
            if(t == 0) 
            {
                btnQuanLy.Enabled = false;
                btnBaoCao.Enabled = false;
            }
            else
            {
                btnQuanLy.Enabled = true;
                btnBaoCao.Enabled = true;
            }
            
        }

        private void FormChinh_Load(object sender, EventArgs e)
        {
            QuanLyTk form = new QuanLyTk();
            form.Dock = DockStyle.Fill;
            pnHETHONG.Controls.Clear();
            pnHETHONG.Controls.Add(form);

            

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NHOM_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbCHINHANH_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.serverName = cmbCHINHANH.SelectedValue.ToString();
            Program.loginName = Program.remoteLogin;
            Program.loginPassword = Program.remotePassword;

            Program.KetNoi();
        }
    }
}
