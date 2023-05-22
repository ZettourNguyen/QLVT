using System;
using System.Collections;
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

namespace QLVT
{
    public partial class VatTu : UserControl
    {
        Stack undoList = new Stack();
        
        string undoUpdateQuery = "";

        string connectionString = @"Data Source=LAPTOP-5KFMFC55;Initial Catalog=QLVT;Persist Security Info=True;User ID=sa;Password=123";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        public VatTu()
        {
            InitializeComponent();
        }

        private void VatTu_Load(object sender, EventArgs e)
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
                cmd = new SqlCommand("SELECT [MAVT],[TENVT],[SOLUONGTON],[DVT] FROM [VATTU] ", con);
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

        private void themBtn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                string query = "INSERT INTO vattu ([MAVT], [TENVT],[SOLUONGTON],[DVT]) values (@value0, @value1, @value2, @value3)";
                SqlCommand cmd = new SqlCommand(query, con, transaction);
                cmd.Parameters.AddWithValue("@value0", textMaVT.Text);
                cmd.Parameters.AddWithValue("@value1", textTenVT.Text);
                cmd.Parameters.AddWithValue("@value2", textSLT.Text);
                cmd.Parameters.AddWithValue("@value3", textDVT.Text);
                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("Them thành công", "Thông báo", MessageBoxButtons.OK);


                String cauTruyVanHoanTac = "";
                cauTruyVanHoanTac = "" + "DELETE DBO.vattu " + "WHERE mavt = '" + textMaVT.Text.Trim() + "'";
                undoList.Push(cauTruyVanHoanTac);

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("them that bai");
                textBox1.Text = ex.ToString();
            }
            con.Close();
            undoBtn.Enabled = true;
        }

        private void viewNhanVien_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) // kiểm tra người dùng đã click vào một dòng hay không
            {
                DataGridViewRow row = this.viewNhanVien.Rows[e.RowIndex];

                string valueMAVT = row.Cells[0].Value.ToString();
                string valueTENVT = row.Cells[1].Value.ToString();
                string valueSL = row.Cells[2].Value.ToString();
                string valueDVT = row.Cells[3].Value.ToString();

                textMaVT.Text = valueMAVT;
                textTenVT.Text = valueTENVT;
                textSLT.Text = valueSL;
                textDVT.Text = valueDVT;

                undoUpdateQuery = "update vattu set [TENVT] = '" + textTenVT.Text + "' , [SOLUONGTON] = '"
                    + textSLT.Text + "' , [DVT] = '" + textSLT.Text + "' where [mavt] = '" + textMaVT.Text + "' ;";
                textBox1.Text += undoUpdateQuery;
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

        private void xoaBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ban co muon xoa khong?","Thong bao", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE vattu WHERE mavt = @valueDel", con, trans);
                    cmd.Parameters.AddWithValue("@valueDel", textMaVT.Text.Trim());
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    MessageBox.Show("Xoa thành công", "Thông báo", MessageBoxButtons.OK);
                    String cauTruyVanHoanTac = "";
                    cauTruyVanHoanTac = "INSERT INTO vattu ([MAVT], [TENVT], [SOLUONGTON], [DVT]) " +
                        "VALUES ('" + textMaVT.Text + "', '" + textTenVT.Text + "', " + textSLT.Text + ", '" + textDVT.Text + "')";
                    undoList.Push(cauTruyVanHoanTac);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xoa that bai", "Thông báo", MessageBoxButtons.OK);
                    textBox1.Text = ex.Message;
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
                string query = "update vattu set [TENVT] = @value1,[SOLUONGTON] = @value2,[DVT] = @value3 where [mavt] = @value0";
                SqlCommand cmd = new SqlCommand(query, con, transaction);
                cmd.Parameters.AddWithValue("@value1", textTenVT.Text);
                cmd.Parameters.AddWithValue("@value2", textSLT.Text);
                cmd.Parameters.AddWithValue("@value3", textDVT.Text);
                cmd.Parameters.AddWithValue("@value0", textMaVT.Text);
                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("Ghi thanh cong");

                String cauTruyVanHoanTac = undoUpdateQuery ;
                
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
                cmd = new SqlCommand("SELECT [MAVT],[TENVT],[SOLUONGTON],[DVT] FROM [VATTU] ", con);
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

        private void exitBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }

        

        
    }
    }

