﻿using System;
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
    public partial class PhieuXuat : UserControl
    {
        string connectionString = @"Data Source=" + Program.serverName + ";Initial Catalog=QLVT;Persist Security Info=True;User ID=" + Program.loginName + ";Password=" + Program.loginPassword;

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;

        Stack undoList = new Stack();
        string undoUpdateQueryPX = "";
        string undoUpdateQueryCTPX = "";

        string action = "";
        string oldMAVT = "";

        public PhieuXuat()
        {
            InitializeComponent();
        }

        private void PhieuXuat_Load(object sender, EventArgs e)
        {
            btnGhiCTPN.Enabled = false;
            btnDelCTPN.Enabled = false;
            btnAddCTPN.Enabled = false;
            ghiBtn.Enabled = false;
            xoaBtn.Enabled = false;

            // hien thi table PX
            con = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            viewNhanVien.DataSource = null;
            viewNhanVien.Rows.Clear();
            viewNhanVien.Columns.Clear();
            viewNhanVien.Refresh();
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [MAPX],[NGAY],[hotenkh],[manv],[makho] FROM [phieuxuat] ", con);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                viewNhanVien.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


            // hien thi ds combobox vat tu
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [mavt] FROM [vattu] ", con);

                SqlDataReader reader = cmd.ExecuteReader();

                cbbMaVT.Items.Clear();
                while (reader.Read())
                {
                    string item = reader.GetString(0).Trim();
                    cbbMaVT.Items.Add(item);
                }
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [makho] FROM [kho] ", con);

                SqlDataReader reader = cmd.ExecuteReader();

                cbbMaKho.Items.Clear();
                while (reader.Read())
                {
                    string item = reader.GetString(0).Trim();
                    cbbMaKho.Items.Add(item);
                }
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }




            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [MAPX] FROM [Phieuxuat] ", con);

                SqlDataReader reader = cmd.ExecuteReader();

                cbbMaPX.Items.Clear();
                while (reader.Read())
                {
                    string item = reader.GetString(0).Trim();
                    cbbMaPX.Items.Add(item);
                }
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (undoList.Count == 0)
            {
                undoBtn.Enabled = false;
            }
            else { undoBtn.Enabled = true; }
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
                cmd = new SqlCommand("SELECT [MAPX],[NGAY],[hotenkh],[manv],[makho] FROM [phieuxuat] ", con);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                viewNhanVien.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            CTPNview.DataSource = null;
            CTPNview.Rows.Clear();
            CTPNview.Columns.Clear();
            CTPNview.Refresh();

            // dat lai cac gia tri trong textbox
            textName.Text = "";
            dateTimePicker.Text = "";
            cbbMaPX.Text = "";
            textMaNV.Text = "";
            cbbMaKho.Text = "";
            cbbMaVT.Text = "";
            textSL.Text = "";
            textDG.Text = "";

            // dat disable cac textbox cbb

            textName.Enabled = false;
            cbbMaVT.Enabled = false;
            cbbMaKho.Enabled = false;
            textMaNV.Enabled = false;
            textDG.Enabled = false;
            cbbMaPX.Enabled = false;
            textSL.Enabled = false;
            dateTimePicker.Enabled = false;
            btnGhiCTPN.Enabled = false;
            btnDelCTPN.Enabled = false;
            btnAddCTPN.Enabled = false;
            ghiBtn.Enabled = false;
            xoaBtn.Enabled = false;

            themBtn.Enabled = true;
            btnOK.Visible = false;
            btnHuy.Visible = false;

            action = "";
            viewNhanVien.ClearSelection();

            if (undoList.Count == 0)
            {
                undoBtn.Enabled = false;
            }
            else { undoBtn.Enabled = true; }
        }

        private void viewNhanVien_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) // kiểm tra người dùng đã click vào một dòng hay không
            {
                DataGridViewRow row = this.viewNhanVien.Rows[e.RowIndex];

                string valueMAPX = row.Cells[0].Value.ToString().Trim();
                string valueNgay = row.Cells[1].Value.ToString().Trim();
                string valuehotenkh = row.Cells[2].Value.ToString().Trim();
                string valueMaNV = row.Cells[3].Value.ToString().Trim();
                string valueMaKho = row.Cells[4].Value.ToString().Trim();

                textName.Text = valuehotenkh;
                dateTimePicker.Text = valueNgay;
                cbbMaPX.Text = valueMAPX;
                textMaNV.Text = valueMaNV;
                cbbMaKho.Text = valueMaKho;

                // xuat chi tiet don dat hang theo MAPX
                DataTable dt = new DataTable();
                CTPNview.DataSource = null;
                CTPNview.Rows.Clear();
                CTPNview.Columns.Clear();
                CTPNview.Refresh();
                try
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT [MAPX],[mavt],[soluong],[dongia] FROM [ctpx] where MAPX = @valueMAPX ;", con);
                    cmd.Parameters.AddWithValue("@valueMAPX", valueMAPX);
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    CTPNview.DataSource = dt;
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        string valueMaVT = dt.Rows[0]["mavt"].ToString().Trim();
                        string valueSL = dt.Rows[0]["soluong"].ToString().Trim();
                        string valueDG = dt.Rows[0]["dongia"].ToString().Trim();

                        cbbMaVT.Text = valueMaVT;
                        textSL.Text = valueSL;
                        textDG.Text = valueDG;

                        oldMAVT = valueMaVT;

                        undoUpdateQueryPX = "update phieuxuat set [ngay] = '" + dateTimePicker.Text + "', " +
                                                        "[HOTENKH] = '" + textName.Text + "'," +
                                                        "[manv] = '" + textMaNV.Text + "'," +
                                                        "[makho] = '" + cbbMaKho.Text + "'" +
                                                        "where [MAPX] = '" + cbbMaPX.Text + "';";
                        // + cbbMaVT.Text + "', "
                        undoUpdateQueryCTPX = "UPDATE CTPX SET [MAVT] = '" + cbbMaVT.Text + "', " +
                              "[SOLUONG] = " + textSL.Text + ", " +
                              "[DONGIA] = " + textDG.Text + " " +
                              "WHERE [MAPX] = '" + cbbMaPX.Text + "' " +
                              "AND [MAVT] = '";
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                btnGhiCTPN.Enabled = true;
                ghiBtn.Enabled = true;
                btnAddCTPN.Enabled = true;
                btnDelCTPN.Enabled = true;
                xoaBtn.Enabled = true;
                themBtn.Enabled = false;
                if (Program.userName != textMaNV.Text)
                {
                    btnAddCTPN.Enabled = false;
                    btnDelCTPN.Enabled = false;
                    btnGhiCTPN.Enabled = false;
                    xoaBtn.Enabled = false;
                    ghiBtn.Enabled = false;
                }
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }

        private void CTPNview_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) // kiểm tra người dùng đã click vào một dòng hay không
            {
                DataGridViewRow row = this.CTPNview.Rows[e.RowIndex];
                string valueMaVT = row.Cells[1].Value.ToString().Trim();
                string valueSL = row.Cells[2].Value.ToString().Trim();
                string valueDG = row.Cells[3].Value.ToString().Trim();

                cbbMaVT.Text = valueMaVT;
                textSL.Text = valueSL;
                textDG.Text = valueDG;

                undoUpdateQueryPX = "update PHIEUXUAT set [ngay] = '" + dateTimePicker.Text + "', " +
                            "[hotenkh] = '" + textName.Text + "'," +
                            "[manv] = '" + textMaNV.Text + "'," +
                            "[makho] = '" + cbbMaKho.Text + "'" +
                            "where [MAPX] = '" + cbbMaPX.Text + "';";
                // + cbbMaVT.Text + "', "
                undoUpdateQueryCTPX = "UPDATE CTPX SET [MAVT] = '" + cbbMaVT.Text + "', " +
                      "[SOLUONG] = " + textSL.Text + ", " +
                      "[DONGIA] = " + textDG.Text + " " +
                      "WHERE [MAPX] = '" + cbbMaPX.Text + "' " +
                      "AND [MAVT] = '";

                oldMAVT = valueMaVT;
            }
        }

       

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (action.Equals("AddPX"))
            {
                themPX();
            }
            if (action.Equals("AddCTPX"))
            {
                themCTPX();
            }
            if (action.Equals("EditCTPX"))
            {
                editCTPX();
            }
            if (action.Equals("EditPX"))
            {
                editPX();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            textName.Enabled = false;
            cbbMaVT.Enabled = false;
            cbbMaKho.Enabled = false;
            textMaNV.Enabled = false;
            textDG.Enabled = false;
            cbbMaPX.Enabled = false;
            textSL.Enabled = false;
            dateTimePicker.Enabled = false;
            btnGhiCTPN.Enabled = false;
            btnDelCTPN.Enabled = false;
            btnAddCTPN.Enabled = false;
            ghiBtn.Enabled = false;
            xoaBtn.Enabled = false;
            themBtn.Enabled = true;
            btnOK.Visible = false;
            btnHuy.Visible = false;
            action = "";
            //tra lai date cua row dang dc chon
            LoadSelectedRowDateToDateTimePicker();
        }
        private void LoadSelectedRowDateToDateTimePicker()
        {
            if (viewNhanVien.SelectedRows.Count > 0)
            {
                // Lấy giá trị ngày từ cột tương ứng của hàng đang được chọn
                DateTime selectedDate = (DateTime)viewNhanVien.SelectedRows[0].Cells["NGAY"].Value;

                // Gán giá trị ngày vào DateTimePicker
                dateTimePicker.Value = selectedDate;

            }
        }

        private void themPX()
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                // THEM VAO DATHANG
                string query = "INSERT INTO phieuxuat ([MAPX],[NGAY],[HOTENKH],[MANV],[MAKHO]) VALUES (@valueMAPX, @valueNGAY, @valueHOTENKH, @valueMANV, @valueMAKHO)";
                SqlCommand cmd = new SqlCommand(query, con, transaction);
                cmd.Parameters.AddWithValue("@valueMAPX", cbbMaPX.Text.Trim());
                cmd.Parameters.AddWithValue("@valueNGAY", dateTimePicker.Text.Trim());
                cmd.Parameters.AddWithValue("@valueHOTENKH", textName.Text.Trim());
                cmd.Parameters.AddWithValue("@valueMANV", Program.userName);   //MANV TU DONG DUNG CUA ACC LOGIN
                cmd.Parameters.AddWithValue("@VALUEMAKHO", cbbMaKho.Text.Trim());
                cmd.ExecuteNonQuery();
                // THEM VAO CTDDH
                string queryCTDDH = "INSERT INTO CTPX ([MAPX],[MAVT],[SOLUONG],[DONGIA]) VALUES (@valueMAPX, @valueMAVT, @valueSL, @valueDG)";
                SqlCommand cmdCTDDH = new SqlCommand(queryCTDDH, con, transaction);
                cmdCTDDH.Parameters.AddWithValue("@valueMAPX", cbbMaPX.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueMAVT", cbbMaVT.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueSL", textSL.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueDG", textDG.Text.Trim());
                cmdCTDDH.ExecuteNonQuery();

                transaction.Commit();
                MessageBox.Show("them thanh cong");
                btnAddCTPN.Enabled = true;

                btnOK.Visible = false;
                btnHuy.Visible = false;
                textName.Enabled = false;
                cbbMaVT.Enabled = false;
                cbbMaKho.Enabled = false;
                textMaNV.Enabled = false;
                textDG.Enabled = false;
                cbbMaPX.Enabled = false;
                textSL.Enabled = false;
                dateTimePicker.Enabled = false;
                btnGhiCTPN.Enabled = false;
                btnDelCTPN.Enabled = false;
                ghiBtn.Enabled = false;
                xoaBtn.Enabled = false;

                themBtn.Enabled = true;
                btnAddCTPN.Enabled = true;
                ////undo query
                String cauTruyVanHoanTac = "";
                cauTruyVanHoanTac = "" + "DELETE [CTPX] WHERE [MAPX] = '" + cbbMaPX.Text.Trim() + "' ;" +
                    "" + "DELETE [phieuxuat] WHERE [MAPX] = '" + cbbMaPX.Text.Trim() + "' ;";
                undoList.Push(cauTruyVanHoanTac);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("them that bai", ex.Message);
                textBox1.Text = ex.Message;
            }
            con.Close();

        }

        private void themCTPX()
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                string queryCTDDH = "INSERT INTO CTPX ([MAPX],[MAVT],[SOLUONG],[DONGIA]) VALUES (@valueMAPX, @valueMAVT, @valueSL, @valueDG)";
                SqlCommand cmdCTDDH = new SqlCommand(queryCTDDH, con, transaction);
                cmdCTDDH.Parameters.AddWithValue("@valueMAPX", cbbMaPX.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueMAVT", cbbMaVT.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueSL", textSL.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueDG", textDG.Text.Trim());
                cmdCTDDH.ExecuteNonQuery();

                transaction.Commit();
                MessageBox.Show("them thanh cong");

                btnOK.Visible = false;
                btnHuy.Visible = false;
                textName.Enabled = false;
                cbbMaVT.Enabled = false;
                cbbMaKho.Enabled = false;
                textMaNV.Enabled = false;
                textDG.Enabled = false;
                cbbMaPX.Enabled = false;
                textSL.Enabled = false;
                dateTimePicker.Enabled = false;
                btnGhiCTPN.Enabled = false;
                btnDelCTPN.Enabled = false;
                btnAddCTPN.Enabled = true;
                themBtn.Enabled = true;

                //DELETE FROM CTDDH WHERE [HOTENKH] = @valueHOTENKH AND [MAVT] = @valueMAVT AND [SOLUONG] = @valueSL AND [DONGIA] = @valueDG;
                ////undo query
                String cauTruyVanHoanTac = "";

                cauTruyVanHoanTac = "" + "DELETE CTPX " + "WHERE [MAPX] = '" + cbbMaPX.Text.Trim() + "' AND MAVT = '" + cbbMaVT.Text.Trim() + "';";
                undoList.Push(cauTruyVanHoanTac);
                textBox1.Text = cauTruyVanHoanTac;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("them that bai", ex.Message);
                textBox1.Text = ex.Message;
            }
            con.Close();
        }
        private void editCTPX()
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                string query = "UPDATE CTPX SET [SOLUONG] = @value1, [DONGIA] = @value2, [MAVT] = @value3  WHERE [MAPX] = @value0 and [MAVT] = @value01;";
                SqlCommand cmd = new SqlCommand(query, con, transaction);
                cmd.Parameters.AddWithValue("@value1", textSL.Text.Trim());
                cmd.Parameters.AddWithValue("@value2", textDG.Text.Trim());
                cmd.Parameters.AddWithValue("@value3", cbbMaVT.Text.Trim());
                cmd.Parameters.AddWithValue("@value01", oldMAVT.Trim());
                cmd.Parameters.AddWithValue("@value0", cbbMaPX.Text.Trim());
                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("update thanh cong");

                //undo
                String cauTruyVanHoanTac = undoUpdateQueryCTPX + cbbMaVT.Text + "';";
                textBox1.Text = cauTruyVanHoanTac;
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
        private void editPX()
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                //string query = "INSERT INTO DATHANG ([HOTENKH],[NGAY],[NHACC],[MANV],[MAKHO]) VALUES (@valueHOTENKH, @valueNGAY, @valueNHACC, @valueMANV, @valueMAKHO)";
                string query = "UPDATE phieuxuat set [NGAY] = @value1,[HOTENKH] = @value2,[MANV] = @value3,[makho] = @value4 WHERE [MAPX] = @value0;";
                SqlCommand cmd = new SqlCommand(query, con, transaction);
                cmd.Parameters.AddWithValue("@value1", dateTimePicker.Text);
                cmd.Parameters.AddWithValue("@value2", textName.Text);
                cmd.Parameters.AddWithValue("@value3", textMaNV.Text);
                cmd.Parameters.AddWithValue("@value4", cbbMaKho.Text);
                cmd.Parameters.AddWithValue("@value0", cbbMaPX.Text);

                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("update thanh cong");

                //undo
                String cauTruyVanHoanTac = undoUpdateQueryPX;
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

        private void themBtn_Click(object sender, EventArgs e)
        {
            btnOK.Visible = true; btnHuy.Visible = true;
            // cac text cbb visible = true;
            themBtn.Enabled = false;
            textName.Enabled = true;
            cbbMaVT.Enabled = true;
            cbbMaKho.Enabled = true;
            textMaNV.Enabled = false;
            textDG.Enabled = true;
            textName.Enabled = true;
            textSL.Enabled = true;
            cbbMaPX.Enabled = true;
            dateTimePicker.Enabled = false;
            dateTimePicker.Text = DateTime.Now.ToString();
            btnAddCTPN.Enabled = false;
            //
            textMaNV.Text = Program.userName;
            action = "AddPX";
        }

        private void btnAddCTPX_Click(object sender, EventArgs e)
        {
            btnOK.Visible = true; btnHuy.Visible = true;
            // cac text cbb visible = true;

            cbbMaVT.Enabled = true;
            textDG.Enabled = true;
            textSL.Enabled = true;

            btnAddCTPN.Enabled = false;
            themBtn.Enabled = false;

            xoaBtn.Enabled = false;
            ghiBtn.Enabled = false;
            btnDelCTPN.Enabled = false;
            btnGhiCTPN.Enabled = false;

            action = "AddCTPX";
        }

        private void xoaBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Chuc nang nay se xoa don dat hang, ban van muon xoa?", "Thong bao", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //delete CTDDH where HOTENKH = 'MDDH04' 
            if (result == DialogResult.Yes)
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE CTPX WHERE MAPX = @valuePX ;" +
                        "DELETE phieuxuat WHERE MAPX = @valuePX ", con, trans);
                    cmd.Parameters.AddWithValue("@valuePX", cbbMaPX.Text.Trim());
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    MessageBox.Show("Xoa thành công", "Thông báo", MessageBoxButtons.OK);

                    //undo
                    String cauTruyVanHoanTac = "";
                    cauTruyVanHoanTac = "INSERT INTO phieuxuat ([MAPX], [NGAY], [HOTENKH], [MANV], [MAKHO]) " +
                    "VALUES ('" + cbbMaPX.Text + "', '" + dateTimePicker.Text + "', '" + textName.Text + "', " +
                    "'" + textMaNV.Text + "', '" + cbbMaKho.Text + "');" +
                    "INSERT INTO CTPX ([MAPX], [MAVT], [SOLUONG], [DONGIA]) " +
                    "VALUES ('" + cbbMaPX.Text + "', '" + cbbMaVT.Text + "', " + textSL.Text + ", '" + textDG.Text + "')";

                    undoList.Push(cauTruyVanHoanTac);
                    textBox1.Text = cauTruyVanHoanTac;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xoa that bai", "Thông báo", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }

        private void btnDelCTPX_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show("Ban co muon xoa khong?", "Thong bao", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //delete CTDDH where HOTENKH = 'MDDH04' and MAVT = 'MX07'
                if (result == DialogResult.Yes)
                {
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("DELETE CTPX WHERE MAPX = @valuePX and MAVT = @valueVT", con, trans);
                        cmd.Parameters.AddWithValue("@valuePX", cbbMaPX.Text.Trim());
                        cmd.Parameters.AddWithValue("@valueVT", cbbMaVT.Text.Trim());
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        MessageBox.Show("Xoa thành công", "Thông báo", MessageBoxButtons.OK);

                        //undo
                        String cauTruyVanHoanTac = "";
                        cauTruyVanHoanTac = "INSERT INTO CTPX ([MAPX], [MAVT], [SOLUONG], [DONGIA]) " +
                "VALUES ('" + cbbMaPX.Text + "', '" + cbbMaVT.Text + "', " + textSL.Text + ", '" + textDG.Text + "')";
                        undoList.Push(cauTruyVanHoanTac);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xoa that bai", "Thông báo", MessageBoxButtons.OK);
                    }
                    con.Close();
                }
            }
        }

        private void ghiBtn_Click(object sender, EventArgs e)
        {
            action = "EditPX";
            btnOK.Visible = true;
            btnHuy.Visible = true;

            textName.Enabled = true;
            cbbMaKho.Enabled = true;

            btnAddCTPN.Enabled = false;
            themBtn.Enabled = false;
            xoaBtn.Enabled = false;
            ghiBtn.Enabled = false;
            btnDelCTPN.Enabled = false;
            btnGhiCTPN.Enabled = false;
        }

        private void btnGhiCTPX_Click(object sender, EventArgs e)
        {
            cbbMaVT.Enabled = true;
            textSL.Enabled = true; textDG.Enabled = true;
            action = "EditCTPX";
            btnOK.Visible = true;
            btnHuy.Visible = true;

            btnAddCTPN.Enabled = false;
            themBtn.Enabled = false;
            xoaBtn.Enabled = false;
            ghiBtn.Enabled = false;
            btnDelCTPN.Enabled = false;
            btnGhiCTPN.Enabled = false;
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

        private void PhieuXuat_MouseClick(object sender, MouseEventArgs e)
        {
            

            CTPNview.DataSource = null;
            CTPNview.Rows.Clear();
            CTPNview.Columns.Clear();
            CTPNview.Refresh();

            // dat lai cac gia tri trong textbox
            textName.Text = "";
            dateTimePicker.Text = "";
            cbbMaPX.Text = "";
            textMaNV.Text = "";
            cbbMaKho.Text = "";
            cbbMaVT.Text = "";
            textSL.Text = "";
            textDG.Text = "";

            // dat disable cac textbox cbb

            textName.Enabled = false;
            cbbMaVT.Enabled = false;
            cbbMaKho.Enabled = false;
            textMaNV.Enabled = false;
            textDG.Enabled = false;
            cbbMaPX.Enabled = false;
            textSL.Enabled = false;
            dateTimePicker.Enabled = false;
            btnGhiCTPN.Enabled = false;
            btnDelCTPN.Enabled = false;
            btnAddCTPN.Enabled = false;
            ghiBtn.Enabled = false;
            xoaBtn.Enabled = false;

            themBtn.Enabled = true;
            btnOK.Visible = false;
            btnHuy.Visible = false;

            action = "";
            viewNhanVien.ClearSelection();

            if (undoList.Count == 0)
            {
                undoBtn.Enabled = false;
            }
            else { undoBtn.Enabled = true; }
        }
    }
}
