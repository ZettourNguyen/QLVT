using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.MVVM.Internal.ILReader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLVT
{
    public partial class NhanVien : UserControl
    {
        Stack undoList = new Stack();
        string undoUpdateQuery;
        string patternCMND = @"^(?!0)\d{9}(\d{3})?$";
        string connectionString = @"Data Source="+ Program.serverName + ";Initial Catalog=QLVT;Persist Security Info=True;User ID="+Program.loginName + ";Password="+ Program.loginPassword;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        public NhanVien()
        {
            InitializeComponent();
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            viewNhanVien.DataSource = null;
            viewNhanVien.Rows.Clear();
            viewNhanVien.Columns.Clear();
            viewNhanVien.Refresh();
            if (Program.role == "CONGTY")
            {
                controlPanel.Hide();
            }
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [MANV],[HO],[TEN],[SOCMND],[DIACHI],[NGAYSINH],[LUONG],[MACN],[trangthaixoa] FROM [NhanVien] ", con);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                viewNhanVien.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (undoList.Count == 0)
            {
                undoBtn.Enabled = false;
            }
            else
            {
                undoBtn.Enabled = true;
            }
        }

        private void viewNhanVien_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) // kiểm tra người dùng đã click vào một dòng hay không
            {
                DataGridViewRow row = this.viewNhanVien.Rows[e.RowIndex];

                string valueMANV = row.Cells[0].Value.ToString();
                string valueHO = row.Cells[1].Value.ToString();
                string valueTEN = row.Cells[2].Value.ToString();
                string valueSOCMND = row.Cells[3].Value.ToString();
                string valueDIACHI = row.Cells[4].Value.ToString();
                string valueNGAYSINH = row.Cells[5].Value.ToString();
                string valueLUONG = row.Cells[6].Value.ToString();
                string valueMACN = row.Cells[7].Value.ToString();

                

                textMaNV.Text = valueMANV;
                textHo.Text = valueHO;
                textTen.Text = valueTEN;
                textCMND.Text = valueSOCMND;
                textDiachi.Text = valueDIACHI;
                dateTimePicker.Text =valueNGAYSINH;
                textLuong.Text = valueLUONG;
                _ = valueMACN.Trim() != "" ? textCN.Text = valueMACN.Trim() : textCN.Text = Program.chinhanhduocchon.Trim();

                undoUpdateQuery = "update NhanVien set [HO] = '" + textHo.Text + "' , [TEN] = '"
                    + textTen.Text + "' , [SOCMND] = '" + textCMND.Text + "' , [DIACHI] = '" + textDiachi.Text + "' , [NGAYSINH] = '" + dateTimePicker.Text +
                    "' , [LUONG] = '" + textLuong.Text + "' , [MACN] = '" + textCN.Text +
                    "' where [MANV] = '" + textMaNV.Text + "' ;";
            }
        }

        private void themBtn_Click(object sender, EventArgs e)
        {
            con.Open();
            int max = 0;
            string qrMaNV = "SELECT MAX(manv) AS max_manv FROM nhanvien";
            SqlCommand cmMaNV = new SqlCommand(qrMaNV, con);
            SqlDataReader reader = cmMaNV.ExecuteReader();

            if (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    max = reader.GetInt32(0);
                    max = max +2;
                }

            }
            reader.Close();

            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                // manv = manv cuoi + 1
                

                if(Regex.IsMatch(textCMND.Text, patternCMND))
                {
                    string query = "INSERT INTO NhanVien ([MANV],[HO],[TEN],[SOCMND],[DIACHI],[NGAYSINH],[LUONG],[MACN],[TrangThaiXoa]) VALUES (@value0, @value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8)";
                    SqlCommand cmd = new SqlCommand(query, con, transaction);
                    cmd.Parameters.AddWithValue("@value0", max);
                    cmd.Parameters.AddWithValue("@value1", textHo.Text);
                    cmd.Parameters.AddWithValue("@value2", textTen.Text);
                    cmd.Parameters.AddWithValue("@value3", textCMND.Text);
                    cmd.Parameters.AddWithValue("@value4", textDiachi.Text);
                    cmd.Parameters.AddWithValue("@value5", dateTimePicker.Text);
                    if (int.Parse(textLuong.Text) >= 4000000)
                    {
                        cmd.Parameters.AddWithValue("@value6", textLuong.Text);
                    }
                    else
                    {
                        MessageBox.Show("Thêm dữ liệu không thành công \n Luong>=4000000");
                    }
                    cmd.Parameters.AddWithValue("@value7", Program.chinhanhduocchon);
                    cmd.Parameters.AddWithValue("@value8", '0');
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("Thêm nhân viên thành công!");

                    // undo push query
                    String cauTruyVanHoanTac = "";
                    cauTruyVanHoanTac = "" + "DELETE nhanvien " + "WHERE manv = '" + max.ToString() + "'";
                    undoList.Push(cauTruyVanHoanTac);
                    undoBtn.Enabled = true;
                } else
                {
                    MessageBox.Show("Số chứng minh nhân nhân không hợp lệ!");
                }

                
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.Message, "Thông báo thêm thất bại!");
            }
            con.Close();

        }

        private void ghiBtn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                if (Regex.IsMatch(textCMND.Text.Trim(), patternCMND))
                {
                    string query = "UPDATE NhanVien SET [HO] = @value1,[TEN] = @value2,[SOCMND] = @value3,[DIACHI] = @value4,[NGAYSINH] = @value5,[LUONG] = @value6,[MACN] = @value7 WHERE[manv] = @value0;";
                    SqlCommand cmd = new SqlCommand(query, con, transaction);
                    cmd.Parameters.AddWithValue("@value0", textMaNV.Text.Trim());
                    cmd.Parameters.AddWithValue("@value1", textHo.Text);
                    cmd.Parameters.AddWithValue("@value2", textTen.Text);
                    cmd.Parameters.AddWithValue("@value3", textCMND.Text.Trim());
                    cmd.Parameters.AddWithValue("@value4", textDiachi.Text);
                    cmd.Parameters.AddWithValue("@value5", dateTimePicker.Text);

                    if (int.Parse(textLuong.Text) >= 4000000)
                    {
                        cmd.Parameters.AddWithValue("@value6", textLuong.Text);
                    }
                    else
                    {
                        MessageBox.Show("Thêm dữ liệu không thành công \n Luong>=4000000");
                    }
                    cmd.Parameters.AddWithValue("@value7", textCN.Text);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("Cập nhật thành công");

                    // undo push query
                    String cauTruyVanHoanTac = undoUpdateQuery;

                    undoList.Push(cauTruyVanHoanTac);
                } else
                {
                    MessageBox.Show("Số chứng minh nhân nhân không hợp lệ!");
                }
                

                

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.Message,"Cập nhật thất bại!");
            }
            con.Close();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            viewNhanVien.DataSource = null;
            viewNhanVien.Rows.Clear();
            viewNhanVien.Columns.Clear();
            viewNhanVien.Refresh();

            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [MANV],[HO],[TEN],[SOCMND],[DIACHI],[NGAYSINH],[LUONG],[MACN] FROM [NhanVien] WHERE [TRANGTHAIXOA] = 0", con);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                viewNhanVien.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (undoList.Count == 0)
            {
                undoBtn.Enabled = false;
            }
            else
            {
                undoBtn.Enabled = true;
            }
        }

        private void xoaBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Chuc nang xoa nay se tat trang thai hoat dong cua nhan vien, neu muon xoa nhan vien vua them, hay su dung undo", "thong bao", MessageBoxButtons.OKCancel);
            if(dialog == DialogResult.OK)
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {

                    string query = "UPDATE NhanVien SET [TrangThaiXoa] = @value8 WHERE[manv] = @value;";
                    SqlCommand cmd = new SqlCommand(query, con, transaction);
                    cmd.Parameters.AddWithValue("@value", textMaNV.Text);
                    cmd.Parameters.AddWithValue("@value8", '1');
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("Xóa thành công");

                    String cauTruyVanHoanTac = "";
                    cauTruyVanHoanTac = "update nhanvien set [trangthaixoa] = 0 where [manv] = '" + textMaNV.Text + "' ;"; ;
                    undoList.Push(cauTruyVanHoanTac);
                    undoBtn.Enabled = true;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Xóa thất bại!");
                }
                con.Close();
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            
            System.Windows.Forms.Application.Exit();
        }

        private void undoBtn_Click(object sender, EventArgs e)
        {
            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để khôi phục", "Thông báo", MessageBoxButtons.OK);
                undoBtn.Enabled = false;
            }

            else
            {
                undoBtn.Enabled = true;

                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    String cauTruyVanHoanTac = undoList.Pop().ToString();
                    string query = cauTruyVanHoanTac;
                    SqlCommand cmd = new SqlCommand(query, con, transaction);
                    cmd.Parameters.AddWithValue("@textMaNV", textMaNV.Text.Trim());
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("undo thành công", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);

                }
                con.Close();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void viewNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Program.brand.ToString());
        }
    }
}
