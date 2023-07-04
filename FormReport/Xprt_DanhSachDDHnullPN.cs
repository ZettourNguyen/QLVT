using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLVT.pnXprt
{
    public partial class Xprt_DanhSachDDHnullPN : DevExpress.XtraReports.UI.XtraReport
    {
        public Xprt_DanhSachDDHnullPN()
        {
            InitializeComponent();
            if (Program.role.Contains("CONGTY"))
            {
                //String connstrLeft = "Data Source=" + Program.serverNameLeft + ";Initial Catalog=" +
                //       Program.database + ";User ID=" +
                //       Program.loginName + ";password=" + Program.loginPassword;
                //this.sqlDataSource1.Connection.ConnectionString = Program.connstr + ";TrustServerCertificate=True";
                //this.sqlDataSource1.Fill();

                string conn = "Data Source=LAPTOP-5KFMFC55;Initial Catalog=QLVT;Integrated Security=True;";
                this.sqlDataSource1.Connection.ConnectionString = conn + ";TrustServerCertificate=True";
                this.sqlDataSource1.Fill();

                // Lấy dữ liệu từ nguồn dữ liệu thứ hai
                //this.sqlDataSource1.Connection.ConnectionString = Program.connstr + ";TrustServerCertificate=True";
                //this.sqlDataSource1.Fill();
            }
            else
            {
                this.sqlDataSource1.Connection.ConnectionString = Program.connstr + ";TrustServerCertificate=True";
                this.sqlDataSource1.Fill();
            }
        }

    }
}
