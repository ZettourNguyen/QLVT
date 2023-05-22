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
    public partial class TrangChu : Form
    {

        public TrangChu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.Dock = DockStyle.Fill;
            controlPn.Controls.Add(nv);

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
    }
}
