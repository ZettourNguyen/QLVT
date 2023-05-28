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

        private void DANGNHAP_Click(object sender, EventArgs e)
        {
            FormDangNhap form = new FormDangNhap();

            form.Show();
            Program.formChinh.MANHANVIEN.Text = "MÃ NHÂN VIÊN:";
            Program.formChinh.HOTEN.Text = "HỌ TÊN:";
            Program.formChinh.NHOM.Text = "VAI TRÒ:";
            Program.formChinh.enableButtons(0);

        }


        private void TAOTAIKHOAN_Click(object sender, EventArgs e)
        {
            if(Program.userName.Equals(""))
            {
                MessageBox.Show("Ban phai dang nhap de tao tai khoan","Thong bao",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
