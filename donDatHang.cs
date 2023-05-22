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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLVT
{
    
    public partial class donDatHang : UserControl
    {

        string connectionString = @"Data Source=LAPTOP-5KFMFC55;Initial Catalog=QLVT;Persist Security Info=True;User ID=sa;Password=123";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;

        Stack undoList = new Stack();
        string undoUpdateQueryDDH = "";
        string undoUpdateQueryCTDDH = "";

        string oldMAVT = "";

        public donDatHang()
        {
            InitializeComponent();
        }

        private void donDatHang_Load(object sender, EventArgs e)
        {
            btnGhiCTDDH.Enabled = false;
            btnDelCTDDH.Enabled = false;
            btnAddCTDDH.Enabled = false;
            ghiBtn.Enabled = false;
            xoaBtn.Enabled = false;

            // hien thi table ddh
            con = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            viewNhanVien.DataSource = null;
            viewNhanVien.Rows.Clear();
            viewNhanVien.Columns.Clear();
            viewNhanVien.Refresh();
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [MasoDDH],[NGAY],[NHACC],[MANV],[MAKHO] FROM [DATHANG] ", con);
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
                cmd = new SqlCommand("SELECT [masoddh] FROM [DatHang] ", con);

                SqlDataReader reader = cmd.ExecuteReader();

                cbbMasoDDH.Items.Clear();
                while (reader.Read())
                {
                    string item = reader.GetString(0).Trim();
                    cbbMasoDDH.Items.Add(item);
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
        private void viewNhanVien_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) // kiểm tra người dùng đã click vào một dòng hay không
            {
                DataGridViewRow row = this.viewNhanVien.Rows[e.RowIndex];

                string valueMaDDH = row.Cells[0].Value.ToString().Trim();
                string valueDate = row.Cells[1].Value.ToString().Trim();
                string valueNCC = row.Cells[2].Value.ToString().Trim();
                string valueMaNV = row.Cells[3].Value.ToString().Trim();
                string valueMaKho = row.Cells[4].Value.ToString().Trim();


                cbbMasoDDH.Text = valueMaDDH;
                dateTimePicker.Text = valueDate;
                textNCC.Text = valueNCC;
                textMaNV.Text = valueMaNV;
                cbbMaKho.Text = valueMaKho;
                
                
                // bang chi tiet don dat hang
                con = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                CTDDHview.DataSource = null;
                CTDDHview.Rows.Clear();
                CTDDHview.Columns.Clear();
                CTDDHview.Refresh();
                try
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT [MasoDDH],[mavt],[soluong],[dongia] FROM [CTDDH] where [MasoDDH] = @value ", con);
                    cmd.Parameters.AddWithValue("@value ", valueMaDDH.Trim());
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    CTDDHview.DataSource = dt;
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


                        // string query = "UPDATE  DATHANG [NGAY] = @value1,[NHACC] = @value2,[MANV] = @value3,[makho] = @value4 WHERE [MASODDH] = @value0;";
                        undoUpdateQueryDDH = "update dathang set [ngay] = '" + dateTimePicker.Text + "', " +
                            "[nhacc] = '" + textNCC.Text + "'," +
                            "[manv] = '" + textMaNV.Text + "'," +
                            "[makho] = '" + cbbMaKho.Text + "'" +
                            "where [masoddh] = '" + cbbMasoDDH.Text + "';";
                        // + cbbMaVT.Text + "', "
                        undoUpdateQueryCTDDH = "UPDATE CTDDH SET [MAVT] = '" + cbbMaVT.Text + "', " +
                      "[SOLUONG] = " + textSL.Text + ", " +
                      "[DONGIA] = " + textDG.Text + " " +
                      "WHERE [MASODDH] = '" + cbbMasoDDH.Text + "' " +
                      "AND [MAVT] = '" ;
                        textBox1.Text = undoUpdateQueryCTDDH;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                btnGhiCTDDH.Enabled = true;
                ghiBtn.Enabled = true;
                btnAddCTDDH.Enabled = true;
                btnDelCTDDH.Enabled = true;
                xoaBtn.Enabled = true;
                themBtn.Enabled = false;


            }


        }
        private void CTDDHview_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) // kiểm tra người dùng đã click vào một dòng hay không
            {
                DataGridViewRow row = this.CTDDHview.Rows[e.RowIndex];
                string valueMaVT = row.Cells[1].Value.ToString().Trim();
                string valueSL = row.Cells[2].Value.ToString().Trim();
                string valueDG = row.Cells[3].Value.ToString().Trim();

                cbbMaVT.Text = valueMaVT;
                textSL.Text = valueSL;
                textDG.Text = valueDG;

                undoUpdateQueryDDH = "update dathang set [ngay] = '" + dateTimePicker.Text + "', " +
                            "[nhacc] = '" + textNCC.Text + "'," +
                            "[manv] = '" + textMaNV.Text + "'," +
                            "[makho] = '" + cbbMaKho.Text + "'" +
                            "where [masoddh] = '" + cbbMasoDDH.Text + "';";
                // + cbbMaVT.Text + "', "
                undoUpdateQueryCTDDH = "UPDATE CTDDH SET [MAVT] = '" + cbbMaVT.Text + "', " +
                      "[SOLUONG] = " + textSL.Text + ", " +
                      "[DONGIA] = " + textDG.Text + " " +
                      "WHERE [MASODDH] = '" + cbbMasoDDH.Text + "' " +
                      "AND [MAVT] = '" ;
                textBox1.Text = undoUpdateQueryCTDDH;
                oldMAVT = valueMaVT;
            }
        }

        // ===== undo exit reload click
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
                cmd = new SqlCommand("SELECT [MasoDDH],[NGAY],[NHACC],[MANV],[MAKHO] FROM [DATHANG] ", con);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                viewNhanVien.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            CTDDHview.DataSource = null;
            CTDDHview.Rows.Clear();
            CTDDHview.Columns.Clear();
            CTDDHview.Refresh();
            
            // dat lai cac gia tri trong textbox
            cbbMasoDDH.Text = "";
            dateTimePicker.Text = "";
            textNCC.Text = "";
            textMaNV.Text = "";
            cbbMaKho.Text = "";
            cbbMaVT.Text = "";
            textSL.Text = "";
            textDG.Text = "";

            // dat disable cac textbox cbb

            cbbMasoDDH.Enabled = false;
            cbbMaVT.Enabled = false;
            cbbMaKho.Enabled = false;
            textMaNV.Enabled = false;
            textDG.Enabled = false;
            textNCC.Enabled = false;
            textSL.Enabled = false;
            dateTimePicker.Enabled = false;
            btnGhiCTDDH.Enabled = false;
            btnDelCTDDH.Enabled = false;
            btnAddCTDDH.Enabled = false;
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

        

        // ===== them xoa sua btn click
        // ddh
        private void themBtn_Click(object sender, EventArgs e)
        {
            btnOK.Visible = true;btnHuy.Visible = true;
            // cac text cbb visible = true;
            themBtn.Enabled = false;
            cbbMasoDDH.Enabled = true;
            cbbMaVT.Enabled = true;
            cbbMaKho.Enabled = true;
            textMaNV.Enabled = true;
            textDG.Enabled = true;
            textNCC.Enabled = true;
            textSL.Enabled = true;
            dateTimePicker.Enabled = false;
            dateTimePicker.Text = DateTime.Now.ToString();
            btnAddCTDDH.Enabled = false;
            //
            action = "AddDDH";
        }
        private void xoaBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Chuc nang nay se xoa don dat hang, ban van muon xoa?", "Thong bao", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //delete CTDDH where MasoDDH = 'MDDH04' 
            if (result == DialogResult.Yes)
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE CTDDH WHERE MasoDDH = @valueDDH ;" +
                        "DELETE DATHANG WHERE MasoDDH = @valueDDH ", con, trans);
                    cmd.Parameters.AddWithValue("@valueDDH", cbbMasoDDH.Text.Trim());
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    MessageBox.Show("Xoa thành công", "Thông báo", MessageBoxButtons.OK);

                    //undo
                    String cauTruyVanHoanTac = "";
                    cauTruyVanHoanTac = "INSERT INTO DATHANG ([MASODDH], [NGAY], [NHACC], [MANV], [MAKHO]) " +
                    "VALUES ('" + cbbMasoDDH.Text + "', '" + dateTimePicker.Text + "', '" + textNCC.Text + "', " +
                    "'" + textMaNV.Text + "', '" + cbbMaKho.Text + "');" +
                    "INSERT INTO CTDDH ([MASODDH], [MAVT], [SOLUONG], [DONGIA]) " +
                    "VALUES ('" + cbbMasoDDH.Text + "', '" + cbbMaVT.Text + "', " + textSL.Text + ", '" + textDG.Text + "')";

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
        private void ghiBtn_Click(object sender, EventArgs e)
        {
            action = "EditDDH";
            btnOK.Visible = true;
            btnHuy.Visible = true;

            textNCC.Enabled = true;
            cbbMaKho.Enabled = true;

            btnAddCTDDH.Enabled = false;
            themBtn.Enabled = false;
            xoaBtn.Enabled = false;
            ghiBtn.Enabled = false;
            btnDelCTDDH.Enabled = false;
            btnGhiCTDDH.Enabled = false;
        }


        // ctddh
        private void btnAddCTDDH_Click(object sender, EventArgs e)
            {
                btnOK.Visible = true; btnHuy.Visible = true;
                // cac text cbb visible = true;

                cbbMaVT.Enabled = true;
                textDG.Enabled = true;
                textSL.Enabled = true;

                btnAddCTDDH.Enabled = false;
                themBtn.Enabled = false;
                xoaBtn.Enabled = false;
                ghiBtn.Enabled = false;
                btnDelCTDDH.Enabled = false;
                btnGhiCTDDH.Enabled = false;

            action = "AddCTDDH";
            }
        private void btnDelCTDDH_Click(object sender, EventArgs e)
                {
                    DialogResult result = MessageBox.Show("Ban co muon xoa khong?", "Thong bao", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    //delete CTDDH where MasoDDH = 'MDDH04' and MAVT = 'MX07'
                    if (result == DialogResult.Yes)
                    {
                        con.Open();
                        SqlTransaction trans = con.BeginTransaction();
                        try
                        {
                            SqlCommand cmd = new SqlCommand("DELETE CTDDH WHERE MasoDDH = @valueDDH and MAVT = @valueVT", con, trans);
                            cmd.Parameters.AddWithValue("@valueDDH", cbbMasoDDH.Text.Trim());
                            cmd.Parameters.AddWithValue("@valueVT", cbbMaVT.Text.Trim());
                            cmd.ExecuteNonQuery();
                            trans.Commit();
                            MessageBox.Show("Xoa thành công", "Thông báo", MessageBoxButtons.OK);

                            //undo
                            String cauTruyVanHoanTac = "";
                            cauTruyVanHoanTac = "INSERT INTO CTDDH ([MASODDH], [MAVT], [SOLUONG], [DONGIA]) " +
                    "VALUES ('" + cbbMasoDDH.Text + "', '" + cbbMaVT.Text + "', " + textSL.Text + ", '" + textDG.Text + "')";
                    undoList.Push(cauTruyVanHoanTac);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Xoa that bai", "Thông báo", MessageBoxButtons.OK);
                        }
                        con.Close();
                    }
                }

        // ===== cac function cua ok cancel
        // ===ok cancel
        string action = "";
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (action.Equals("AddDDH"))
            {
                themDDH();
            }
            if (action.Equals("AddCTDDH"))
            {
                themCTDDH();
            }
            if (action.Equals("EditCTDDH"))
            {
                editCTDDH();
            }
            if (action.Equals("EditDDH"))
            {
                editDDH();
            }



        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            cbbMasoDDH.Enabled = false;
            cbbMaVT.Enabled = false;
            cbbMaKho.Enabled = false;
            textMaNV.Enabled = false;
            textDG.Enabled = false;
            textNCC.Enabled = false;
            textSL.Enabled = false;
            dateTimePicker.Enabled = false;
            btnGhiCTDDH.Enabled = false;
            btnDelCTDDH.Enabled = false;
            btnAddCTDDH.Enabled = false;
            ghiBtn.Enabled = false;
            xoaBtn.Enabled = false;
            themBtn.Enabled = true;
            btnOK.Visible = false;
            btnHuy.Visible = false;
            action = "";
            //tra lai date cua row dang dc chon
            LoadSelectedRowDateToDateTimePicker();
        }

        private void btnGhiCTDDH_Click(object sender, EventArgs e)
        {
            cbbMaVT.Enabled = true;
            textSL.Enabled = true; textDG.Enabled = true;
            action = "EditCTDDH";
            btnOK.Visible = true;
            btnHuy.Visible = true;

            btnAddCTDDH.Enabled = false;
            themBtn.Enabled = false;
            xoaBtn.Enabled = false;
            ghiBtn.Enabled = false;
            btnDelCTDDH.Enabled = false;
            btnGhiCTDDH.Enabled = false;
        }

        // ===func them sua xoa
        // ddh
        private void themDDH()
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                // THEM VAO DATHANG
                string query = "INSERT INTO DATHANG ([MASODDH],[NGAY],[NHACC],[MANV],[MAKHO]) VALUES (@valueMASODDH, @valueNGAY, @valueNHACC, @valueMANV, @valueMAKHO)";
                SqlCommand cmd = new SqlCommand(query, con, transaction);
                cmd.Parameters.AddWithValue("@valueMASODDH", cbbMasoDDH.Text.Trim());
                cmd.Parameters.AddWithValue("@valueNGAY", dateTimePicker.Text.Trim());
                cmd.Parameters.AddWithValue("@valueNHACC", textNCC.Text.Trim());
                cmd.Parameters.AddWithValue("@valueMANV", textMaNV.Text.Trim());   //MANV TU DONG DUNG CUA ACC LOGIN
                cmd.Parameters.AddWithValue("@VALUEMAKHO", cbbMaKho.Text.Trim());
                cmd.ExecuteNonQuery();
                // THEM VAO CTDDH
                string queryCTDDH = "INSERT INTO CTDDH ([MASODDH],[MAVT],[SOLUONG],[DONGIA]) VALUES (@valueMASODDH, @valueMAVT, @valueSL, @valueDG)";
                SqlCommand cmdCTDDH = new SqlCommand(queryCTDDH, con, transaction);
                cmdCTDDH.Parameters.AddWithValue("@valueMASODDH", cbbMasoDDH.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueMAVT", cbbMaVT.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueSL", textSL.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueDG", textDG.Text.Trim());
                cmdCTDDH.ExecuteNonQuery();

                transaction.Commit();
                MessageBox.Show("them thanh cong");
                btnAddCTDDH.Enabled = true;

                btnOK.Visible = false;
                btnHuy.Visible = false;
                cbbMasoDDH.Enabled = false;
                cbbMaVT.Enabled = false;
                cbbMaKho.Enabled = false;
                textMaNV.Enabled = false;
                textDG.Enabled = false;
                textNCC.Enabled = false;
                textSL.Enabled = false;
                dateTimePicker.Enabled = false;
                btnGhiCTDDH.Enabled = false;
                btnDelCTDDH.Enabled = false;
                ghiBtn.Enabled = false;
                xoaBtn.Enabled = false;

                themBtn.Enabled = true;
                btnAddCTDDH.Enabled = true;
                ////undo query
                String cauTruyVanHoanTac = "";
                cauTruyVanHoanTac = "" + "DELETE [CTDDH] WHERE [MASODDH] = '" + cbbMasoDDH.Text.Trim() + "' ;" +
                    ""  + "DELETE [DATHANG] WHERE [MASODDH] = '" + cbbMasoDDH.Text.Trim() + "' ;";
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
        
        private void editDDH()
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                //string query = "INSERT INTO DATHANG ([MASODDH],[NGAY],[NHACC],[MANV],[MAKHO]) VALUES (@valueMASODDH, @valueNGAY, @valueNHACC, @valueMANV, @valueMAKHO)";
                string query = "UPDATE DATHANG set [NGAY] = @value1,[NHACC] = @value2,[MANV] = @value3,[makho] = @value4 WHERE [MASODDH] = @value0;";
                SqlCommand cmd = new SqlCommand(query, con, transaction);
                cmd.Parameters.AddWithValue("@value1", dateTimePicker.Text);
                cmd.Parameters.AddWithValue("@value2", textNCC.Text);
                cmd.Parameters.AddWithValue("@value3", textMaNV.Text);
                cmd.Parameters.AddWithValue("@value4", cbbMaKho.Text);
                cmd.Parameters.AddWithValue("@value0", cbbMasoDDH.Text);

                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("update thanh cong");

                //undo
                String cauTruyVanHoanTac = undoUpdateQueryDDH;
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

        // ctddh
        private void themCTDDH()
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                string queryCTDDH = "INSERT INTO CTDDH ([MASODDH],[MAVT],[SOLUONG],[DONGIA]) VALUES (@valueMASODDH, @valueMAVT, @valueSL, @valueDG)";
                SqlCommand cmdCTDDH = new SqlCommand(queryCTDDH, con, transaction);
                cmdCTDDH.Parameters.AddWithValue("@valueMASODDH", cbbMasoDDH.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueMAVT", cbbMaVT.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueSL", textSL.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@valueDG", textDG.Text.Trim());
                cmdCTDDH.ExecuteNonQuery();

                transaction.Commit();
                MessageBox.Show("them thanh cong");

                btnOK.Visible = false;
                btnHuy.Visible = false;
                cbbMasoDDH.Enabled = false;
                cbbMaVT.Enabled = false;
                cbbMaKho.Enabled = false;
                textMaNV.Enabled = false;
                textDG.Enabled = false;
                textNCC.Enabled = false;
                textSL.Enabled = false;
                dateTimePicker.Enabled = false;
                btnGhiCTDDH.Enabled = false;
                btnDelCTDDH.Enabled = false;
                btnAddCTDDH.Enabled = true;
                themBtn.Enabled = true;

                //DELETE FROM CTDDH WHERE [MASODDH] = @valueMASODDH AND [MAVT] = @valueMAVT AND [SOLUONG] = @valueSL AND [DONGIA] = @valueDG;
                ////undo query
                String cauTruyVanHoanTac = "";
                
                cauTruyVanHoanTac = "" + "DELETE CTDDH " + "WHERE [MASODDH] = '" + cbbMasoDDH.Text.Trim() + "' AND MAVT = '" +cbbMaVT.Text.Trim() + "';";
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

        private void editCTDDH()
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                string query = "UPDATE CTDDH SET [SOLUONG] = @value1, [DONGIA] = @value2, [MAVT] = @value3  WHERE [MASODDH] = @value0 and [MAVT] = @value01;";
                SqlCommand cmd = new SqlCommand(query, con, transaction);
                cmd.Parameters.AddWithValue("@value1", textSL.Text.Trim());
                cmd.Parameters.AddWithValue("@value2", textDG.Text.Trim());
                cmd.Parameters.AddWithValue("@value3", cbbMaVT.Text.Trim());

                cmd.Parameters.AddWithValue("@value0", cbbMasoDDH.Text.Trim());
                cmd.Parameters.AddWithValue("@value01", oldMAVT.Trim());

                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("update thanh cong");

                //undo
                String cauTruyVanHoanTac = undoUpdateQueryCTDDH + cbbMaVT.Text + "';";
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

        // other func
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
        private void donDatHang_MouseClick(object sender, MouseEventArgs e)
        {
            viewNhanVien.ClearSelection();
            CTDDHview.DataSource = null;
            CTDDHview.Rows.Clear();
            CTDDHview.Columns.Clear();
            CTDDHview.Refresh();
            //dat lai cac text, cbb
            cbbMasoDDH.Text = "";
            dateTimePicker.Text = "";
            textNCC.Text = "";
            textMaNV.Text = "";
            cbbMaKho.Text = "";
            cbbMaVT.Text = "";
            textSL.Text = "";
            textDG.Text = "";

            cbbMasoDDH.Enabled = false;
            cbbMaVT.Enabled = false;
            cbbMaKho.Enabled = false;
            textMaNV.Enabled = false;
            textDG.Enabled = false;
            textNCC.Enabled = false;
            textSL.Enabled = false;
            dateTimePicker.Enabled = false;
            btnGhiCTDDH.Enabled = false;
            btnDelCTDDH.Enabled = false;
            btnAddCTDDH.Enabled = false;
            ghiBtn.Enabled = false;
            xoaBtn.Enabled = false;

            themBtn.Enabled = true;
            btnOK.Visible = false;
            btnHuy.Visible = false;

            action = "";
            if (undoList.Count == 0)
            {
                undoBtn.Enabled = false;
            }
            else
            {
                undoBtn.Enabled = true;
            }
        }
    }
}
