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
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace QLVT
{
    public partial class Kho : UserControl
    {
        string connectionString = @"Data Source=" + Program.serverName + ";Initial Catalog=QLVT;Persist Security Info=True;User ID=" + Program.loginName + ";Password=" + Program.loginPassword;

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;

        Stack undoList = new Stack();

        string undoUpdateQuery = "";


        public Kho()
        {
            InitializeComponent();
        }
        
        private void Kho_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            viewNhanVien.DataSource = null;
            viewNhanVien.Rows.Clear();
            viewNhanVien.Columns.Clear();
            viewNhanVien.Refresh();

            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [MAKHO],[TENKHO],[DIACHI],[MACN] FROM [KHO] ", con);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                viewNhanVien.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            textMaCN.Text = Program.maCnToString();
            textMaCN.Enabled = false;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            viewNhanVien.DataSource = null;
            viewNhanVien.Rows.Clear();
            viewNhanVien.Columns.Clear();
            viewNhanVien.Refresh();

            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [MAKHO],[TENKHO],[DIACHI],[MACN] FROM [KHO] ", con);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                viewNhanVien.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            
        }

        private void viewNhanVien_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) // kiểm tra người dùng đã click vào một dòng hay không
            {
                DataGridViewRow row = this.viewNhanVien.Rows[e.RowIndex];

                string valueMaKho = row.Cells[0].Value.ToString();
                string valueTenKho = row.Cells[1].Value.ToString();
                string valueDiaChi = row.Cells[2].Value.ToString();
                string valueMaCN = row.Cells[3].Value.ToString();
                

                textMaKho.Text = valueMaKho.Trim();
                textTenKho.Text = valueTenKho.Trim();
                textDiaChi.Text = valueDiaChi.Trim();
                textMaCN.Text = valueMaCN.Trim();

                undoUpdateQuery = "update kho set [TENkho] = '" + textTenKho.Text + "' , [diachi] = '"
                    + textDiaChi.Text + "' , [macn] = '" + textMaCN.Text + "' where [makho] = '" + textMaKho.Text.Trim() + "' ;";
                textBox1.Text = undoUpdateQuery;
            }
        }

        private void themBtn_Click(object sender, EventArgs e)
        {
            if(textMaKho.Text == "" || textTenKho.Text == "" || textDiaChi.Text == "")
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin!", "Thêm thất bại");
            }
            else
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    string query = "INSERT INTO Kho ([MAKHO],[TENKHO],[DIACHI],[MACN]) VALUES (@value0, @value1, @value2, @value3)";
                    SqlCommand cmd = new SqlCommand(query, con, transaction);
                    cmd.Parameters.AddWithValue("@value0", textMaKho.Text.Trim());
                    cmd.Parameters.AddWithValue("@value1", textTenKho.Text.Trim());
                    cmd.Parameters.AddWithValue("@value2", textDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@value3", textMaCN.Text.Trim());
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("them thanh cong");

                    //undo query
                    String cauTruyVanHoanTac = "";
                    cauTruyVanHoanTac = "" + "DELETE DBO.Kho " + "WHERE makho = '" + textMaKho.Text.Trim() + "'";
                    undoList.Push(cauTruyVanHoanTac);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message, "Thêm thất bại");
                }
                con.Close();
            }
        }

        private void ghiBtn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                string query = "UPDATE KHO SET [TENKHO] = @value1, [DIACHI] = @value2,[MACN] = @value3 WHERE [MAKHO] = @value0;";
                SqlCommand cmd = new SqlCommand(query, con, transaction);
                cmd.Parameters.AddWithValue("@value1", textTenKho.Text);
                cmd.Parameters.AddWithValue("@value2", textDiaChi.Text);
                cmd.Parameters.AddWithValue("@value3", textMaCN.Text);
                cmd.Parameters.AddWithValue("@value0", textMaKho.Text);
                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("update thanh cong");

                //undo
                String cauTruyVanHoanTac = undoUpdateQuery;
                undoList.Push(cauTruyVanHoanTac);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("update that bai");
                textBox1.Text = ex.ToString();
            }
            con.Close();
        }

        private void xoaBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ban co muon xoa khong?", "Thong bao", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE KHO WHERE makho = @valueDel", con, trans);
                    cmd.Parameters.AddWithValue("@valueDel", textMaKho.Text.Trim());
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    MessageBox.Show("Xoa thành công", "Thông báo", MessageBoxButtons.OK);

                    //undo
                    String cauTruyVanHoanTac = "";
                    cauTruyVanHoanTac = "INSERT INTO kho ([MAkho], [TENkho], [diachi], [macn]) " +
                        "VALUES ('" + textMaKho.Text + "', '" + textTenKho.Text + "', " + textDiaChi.Text + ", '" + textMaCN.Text + "')";
                    undoList.Push(cauTruyVanHoanTac);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xoa that bai", "Thông báo", MessageBoxButtons.OK);
                }
                con.Close();
            }
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
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    String cauTruyVanHoanTac = undoList.Pop().ToString();
                    string query = cauTruyVanHoanTac;
                    SqlCommand cmd = new SqlCommand(query, con, transaction);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("undo thành công", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("undo that bai", "Thông báo", MessageBoxButtons.OK);

                    textBox1.Text = ex.ToString();
                }
                con.Close();
            }
        }

        private void textMaCN_TextChanged(object sender, EventArgs e)
        {

        }

        private void textMaKho_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
