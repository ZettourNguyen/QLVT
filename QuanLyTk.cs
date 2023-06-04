using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT
{
    public partial class QuanLyTk : UserControl
    {
        public QuanLyTk()
        {
            InitializeComponent();
        }

        private bool IsLoginFormShown()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FormDangNhap)
                {
                    return true;
                }
            }
            return false;
        }

        private void DANGNHAP_Click(object sender, EventArgs e)
        {


            if (IsLoginFormShown())
            {
                if (Program.formDangNhap != null && !Program.formDangNhap.IsDisposed)
                {
                    Program.formDangNhap.Dispose();
                    Program.formDangNhap = null;
                }
                Program.formDangNhap = new FormDangNhap();
                Program.formDangNhap.Show();
            }
            else
            {
                // Form Đăng nhập chưa hiển thị
                Program.formDangNhap = new FormDangNhap();
                Program.formDangNhap.Show();
                Program.formChinh.MANHANVIEN.Text = "MÃ NHÂN VIÊN:";
                Program.formChinh.HOTEN.Text = "HỌ TÊN:";
                Program.formChinh.NHOM.Text = "VAI TRÒ:";
                Program.formChinh.enableButtons(0);
            }
        }


        private void TAOTAIKHOAN_Click(object sender, EventArgs e)
        {
            if(Program.userName.Equals("")|| Program.role.Equals("USER"))
            {
                MessageBox.Show("Ban khong co quyen tao tai khoan","Thong bao",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                FormTaoTk form = new FormTaoTk();
                form.Show();
            }
        }

        private void THOAT_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }

        private void QuanLyTk_Load(object sender, EventArgs e)
        {
            
        }
    }
}
