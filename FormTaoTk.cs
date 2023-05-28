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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLVT
{
    public partial class FormTaoTk : Form

    {
        string connectionString = @"Data Source=" + Program.serverName + ";Initial Catalog=QLVT;Persist Security Info=True;User ID=" + Program.loginName + ";Password=" + Program.loginPassword;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;

        String targetMaNV = "";
        String targetRole = "CHINHANH";

        public FormTaoTk()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            targetRole = "CHINHANH";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cbbNv_SelectedIndexChanged(object sender, EventArgs e)
        {
            String manv = cbbNv.SelectedItem.ToString().Split('-')[0];
            targetMaNV = manv;
        }

        private void FormTaoTk_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [MANV], [HO], [TEN] FROM [nhanvien] ", con);

                SqlDataReader reader = cmd.ExecuteReader();

                cbbNv.Items.Clear();
                while (reader.Read())
                {
                    string item = reader.GetInt32(0).ToString()+ "-" + reader.GetString(1).ToString() + " " + reader.GetString(2).ToString();
                    cbbNv.Items.Add(item);
                }
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (textPass.Text.Trim() == texRePass.Text.Trim())
            {
                con.Open();
                try
                {
                    SqlCommand cmdTaoTk = new SqlCommand("TaoTaiKhoan", con);
                    cmdTaoTk.CommandType = CommandType.StoredProcedure;
                    cmdTaoTk.Parameters.AddWithValue("@LGNAME", usTextBox.Text.Trim());
                    cmdTaoTk.Parameters.AddWithValue("@PASS", textPass.Text.Trim());
                    cmdTaoTk.Parameters.AddWithValue("@USERNAME", targetMaNV);
                    cmdTaoTk.Parameters.AddWithValue("@ROLE", targetRole);
                    cmdTaoTk.ExecuteNonQuery();

                    MessageBox.Show("Tạo tài khoản thành công!", "Thông báo");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo!");
                }
                con.Close();
            } else
            {
                MessageBox.Show("Xác nhận mật khẩu không đúng", "Thông báo!");
            }
        }

        private void roleBtn2_CheckedChanged(object sender, EventArgs e)
        {
            targetRole = "USER";
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
