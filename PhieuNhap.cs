using DevExpress.CodeParser;
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

namespace QLVT
{
    public partial class PhieuNhap : UserControl
    {
        string connectionString = @"Data Source=" + Program.serverName + ";" +
            "Initial Catalog=QLVT;Persist Security Info=True;" +
            "User ID=" + Program.loginName + ";" +
            "Password=" + Program.loginPassword;

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;

        Stack undoList = new Stack();
        string undoUpdateQueryPN = "";
        string undoUpdateQueryCTPN = "";

        string action = "";
        string oldMAVT = "";


        int oldQuantity = 0;
        public PhieuNhap()
        {
            InitializeComponent();
        }

        private void PhieuNhap_Load(object sender, EventArgs e)
        {
            btnGhiCTPN.Enabled = false;
            btnDelCTPN.Enabled = false;
            btnAddCTPN.Enabled = false;
            ghiBtn.Enabled = false;
            xoaBtn.Enabled = false;

            // hien thi table pn
            con = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            viewNhanVien.DataSource = null;
            viewNhanVien.Rows.Clear();
            viewNhanVien.Columns.Clear();
            viewNhanVien.Refresh();
            if (Program.role == "CONGTY")
            {
                controlPanel1.Hide();
                controlPanel2.Hide();
                controlPanel3.Hide();
            }
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [mapn],[NGAY],[masoddh],[manv],[makho] FROM [phieunhap] ", con);
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

            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT [MaPN] FROM [PhieuNhap] ", con);

                SqlDataReader reader = cmd.ExecuteReader();

                cbbMaPN.Items.Clear();
                while (reader.Read())
                {
                    string item = reader.GetString(0).Trim();
                    cbbMaPN.Items.Add(item);
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


        private void cbbMasoDDH_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                cmd = new SqlCommand("SELECT [mapn],[NGAY],[masoddh],[manv],[makho] FROM [phieunhap] ", con);
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
            cbbMasoDDH.Text = "";
            dateTimePicker.Text = "";
            cbbMaPN.Text = "";
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
            cbbMaPN.Enabled = false;
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

                string valueMaPN = row.Cells[0].Value.ToString().Trim();
                string valueNgay = row.Cells[1].Value.ToString().Trim();
                string valueMaDDH = row.Cells[2].Value.ToString().Trim();
                string valueMaNV = row.Cells[3].Value.ToString().Trim();
                string valueMaKho = row.Cells[4].Value.ToString().Trim();

                cbbMasoDDH.Text = valueMaDDH;
                dateTimePicker.Text = valueNgay;
                cbbMaPN.Text = valueMaPN;
                textMaNV.Text = valueMaNV;
                cbbMaKho.Text = valueMaKho;

                try
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT [mavt] FROM [ctddh] where masoDDH = '"+cbbMasoDDH.Text+"'", con);

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


                // xuat chi tiet don dat hang theo maPN
                DataTable dt = new DataTable();
                CTPNview.DataSource = null;
                CTPNview.Rows.Clear();
                CTPNview.Columns.Clear();
                CTPNview.Refresh();
                try
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT [mapn],[mavt],[soluong],[dongia] FROM [ctpn] where mapn = @valueMaPN ;", con);
                    cmd.Parameters.AddWithValue("@valueMaPN", valueMaPN);
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

                        undoUpdateQueryPN = "update PHIEUNHAP set [ngay] = '" + dateTimePicker.Text + "', " +
                                                        "[MASODDH] = '" + cbbMasoDDH.Text + "'," +
                                                        "[manv] = '" + textMaNV.Text + "'," +
                                                        "[makho] = '" + cbbMaKho.Text + "'" +
                                                        "where [maPN] = '" + cbbMaPN.Text + "';";
                        // + cbbMaVT.Text + "', "
                        undoUpdateQueryCTPN = "UPDATE CTPN SET [MAVT] = '" + cbbMaVT.Text + "', " +
                              "[SOLUONG] = " + textSL.Text + ", " +
                              "[DONGIA] = " + textDG.Text + " " +
                              "WHERE [MAPN] = '" + cbbMaPN.Text + "' " +
                              "AND [MAVT] = '" ;
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

                    undoUpdateQueryPN = "update PHIEUNHAP set [ngay] = '" + dateTimePicker.Text + "', " +
                                "[MASODDH] = '" + cbbMasoDDH.Text + "'," +
                                "[manv] = '" + textMaNV.Text + "'," +
                                "[makho] = '" + cbbMaKho.Text + "'" +
                                "where [maPN] = '" + cbbMaPN.Text + "';";
                    // + cbbMaVT.Text + "', "
                    undoUpdateQueryCTPN = "UPDATE CTPN SET [MAVT] = '" + cbbMaVT.Text + "', " +
                          "[SOLUONG] = " + textSL.Text + ", " +
                          "[DONGIA] = " + textDG.Text + " " +
                          "WHERE [MAPN] = '" + cbbMaPN.Text + "' " +
                          "AND [MAVT] = '" ;
                    
                oldMAVT = valueMaVT;

            }
        }

        private void PhieuNhap_MouseClick(object sender, MouseEventArgs e)
        {
            CTPNview.DataSource = null;
            CTPNview.Rows.Clear();
            CTPNview.Columns.Clear();
            CTPNview.Refresh();

            // dat lai cac gia tri trong textbox
            cbbMasoDDH.Text = "";
            dateTimePicker.Text = "";
            cbbMaPN.Text = "";
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
            cbbMaPN.Enabled = false;
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (action.Equals("AddPN"))
            {
                themPN();
            }
            if (action.Equals("AddCTPN"))
            {
                themCTPN();
            }
            if (action.Equals("EditCTPN"))
            {
                editCTPN();
            }
            if (action.Equals("EditPN"))
            {
                editPN();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            cbbMasoDDH.Enabled = false;
            cbbMaVT.Enabled = false;
            cbbMaKho.Enabled = false;
            textMaNV.Enabled = false;
            textDG.Enabled = false;
            cbbMaPN.Enabled = false;
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

        private void themPN()
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                // THEM VAO DATHANG
                string query = "INSERT INTO phieunhap ([mapn],[NGAY],[MasoDDH],[MANV],[MAKHO]) VALUES (@valuemapn, @valueNGAY, @valueMasoDDH, @valueMANV, @valueMAKHO)";
                SqlCommand cmd = new SqlCommand(query, con, transaction);
                cmd.Parameters.AddWithValue("@valuemapn", cbbMaPN.Text.Trim());
                cmd.Parameters.AddWithValue("@valueNGAY", dateTimePicker.Text.Trim());
                cmd.Parameters.AddWithValue("@valueMasoDDH", cbbMasoDDH.Text.Trim());
                cmd.Parameters.AddWithValue("@valueMANV", textMaNV.Text.Trim());   //MANV TU DONG DUNG CUA ACC LOGIN
                cmd.Parameters.AddWithValue("@VALUEMAKHO", cbbMaKho.Text.Trim());
                cmd.ExecuteNonQuery(); 
                // THEM VAO CTDDH
                //string queryCTDDH = "INSERT INTO CTPN ([mapn],[MAVT],[SOLUONG],[DONGIA]) VALUES (@valuemapn, @valueMAVT, @valueSL, @valueDG)";
                //SqlCommand cmdCTDDH = new SqlCommand(queryCTDDH, con, transaction);
                //cmdCTDDH.Parameters.AddWithValue("@valuemapn", cbbMaPN.Text.Trim());
                //cmdCTDDH.Parameters.AddWithValue("@valueMAVT", cbbMaVT.Text.Trim());
                //cmdCTDDH.Parameters.AddWithValue("@valueSL", textSL.Text.Trim());
                //cmdCTDDH.Parameters.AddWithValue("@valueDG", textDG.Text.Trim());
                //cmdCTDDH.ExecuteNonQuery();

                transaction.Commit();
                MessageBox.Show("them thanh cong");
                btnAddCTPN.Enabled = true;

                btnOK.Visible = false;
                btnHuy.Visible = false;
                cbbMasoDDH.Enabled = false;
                cbbMaVT.Enabled = false;
                cbbMaKho.Enabled = false;
                textMaNV.Enabled = false;
                textDG.Enabled = false;
                cbbMaPN.Enabled = false;
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
                cauTruyVanHoanTac = "" + "DELETE [CTPN] WHERE [MAPN] = '" + cbbMaPN.Text.Trim() + "' ;" +
                    "" + "DELETE [PHIEUNHAP] WHERE [MAPN] = '" + cbbMaPN.Text.Trim() + "' ;";
                undoList.Push(cauTruyVanHoanTac);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("them that bai", ex.Message);
            }
            con.Close();

        }

        private void themCTPN()
        {
            con.Open();

            try
            {
                SqlCommand cmdCTDDH = new SqlCommand("AddChiTietPhieuNhap", con);
                cmdCTDDH.CommandType = CommandType.StoredProcedure;
                cmdCTDDH.Parameters.AddWithValue("@mapn", cbbMaPN.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@MAVT", cbbMaVT.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@SOLUONG", textSL.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@DONGIA", textDG.Text.Trim());
                cmdCTDDH.Parameters.AddWithValue("@MADDH", cbbMasoDDH.Text.Trim());
                cmdCTDDH.ExecuteNonQuery();

                MessageBox.Show("them thanh cong");

                btnOK.Visible = false;
                btnHuy.Visible = false;
                cbbMasoDDH.Enabled = false;
                cbbMaVT.Enabled = false;
                cbbMaKho.Enabled = false;
                textMaNV.Enabled = false;
                textDG.Enabled = false;
                cbbMaPN.Enabled = false;
                textSL.Enabled = false;
                dateTimePicker.Enabled = false;
                btnGhiCTPN.Enabled = false;
                btnDelCTPN.Enabled = false;
                btnAddCTPN.Enabled = true;
                themBtn.Enabled = true;

                //DELETE FROM CTDDH WHERE [MASODDH] = @valueMASODDH AND [MAVT] = @valueMAVT AND [SOLUONG] = @valueSL AND [DONGIA] = @valueDG;
                ////undo query
                String cauTruyVanHoanTac = "";

                cauTruyVanHoanTac = "" + "DELETE CTPN " + "WHERE [MAPN] = '" + cbbMaPN.Text.Trim() + "' AND MAVT = '" + cbbMaVT.Text.Trim() + "';";
                undoList.Push(cauTruyVanHoanTac);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Thông báo!");
            }
            con.Close();
        }
        private void editCTPN()
        {
            con.Open();
            try
            {
                string getSoLuong = "SELECT SOLUONG FROM CTDDH  WHERE MasoDDH = @value1 AND MAVT = @value2";
                SqlCommand cmd2 = new SqlCommand(getSoLuong, con);
                cmd2.Parameters.AddWithValue("@value1", cbbMasoDDH.Text.Trim());
                cmd2.Parameters.AddWithValue("@value2", cbbMaVT.Text.Trim());
                object result = cmd2.ExecuteScalar();

                if (result != null)
                {
                    int soluong = Convert.ToInt32(result);
                    if(int.Parse(textSL.Text.Trim()) <= soluong)
                    {
                        string query = "UPDATE CTPN SET [SOLUONG] = @value1, [DONGIA] = @value2, [MAVT] = @value3  WHERE [MAPN] = @value0 and [MAVT] = @value01;";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@value1", textSL.Text.Trim());
                        cmd.Parameters.AddWithValue("@value2", textDG.Text.Trim());
                        cmd.Parameters.AddWithValue("@value3", cbbMaVT.Text.Trim());
                        cmd.Parameters.AddWithValue("@value01", oldMAVT.Trim());
                        cmd.Parameters.AddWithValue("@value0", cbbMaPN.Text.Trim());
                        cmd.ExecuteNonQuery();

                        int chenhlech = oldQuantity - int.Parse(textSL.Text.Trim());
                        
                        if( chenhlech > 0)
                        {
                            SqlCommand cmd4 = new SqlCommand("UPDATE Vattu set SOLUONGTON = SOLUONGTON - @soLuong WHERE MAVT = @valueMaVT", con);
                            cmd4.Parameters.AddWithValue("@valueMaVT", cbbMaVT.Text.Trim());
                            cmd4.Parameters.AddWithValue("@soLuong", chenhlech.ToString());
                            cmd4.ExecuteNonQuery();
                        } else
                        {
                            SqlCommand cmd4 = new SqlCommand("UPDATE Vattu set SOLUONGTON = SOLUONGTON + @soLuong WHERE MAVT = @valueMaVT", con);
                            cmd4.Parameters.AddWithValue("@valueMaVT", cbbMaVT.Text.Trim());
                            cmd4.Parameters.AddWithValue("@soLuong", Math.Abs(chenhlech).ToString());
                            cmd4.ExecuteNonQuery();
                        }

                        MessageBox.Show("update thanh cong");
                        String cauTruyVanHoanTac = undoUpdateQueryCTPN + cbbMaVT.Text + "';";
                        undoList.Push(cauTruyVanHoanTac);
                    } else
                    {
                        MessageBox.Show("Số lượng lớn hơn số lượng trong đơn đặt hàng!", "Thông báo");
                    }

                    //undo
                    
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra", "Thông báo");
                }




                
            }
            catch (Exception ex)
            {
                MessageBox.Show("update that bai");
            }
            con.Close();
        }
        private void editPN()
        {
            con.Open();
            try
            {
                //string query = "INSERT INTO DATHANG ([MASODDH],[NGAY],[NHACC],[MANV],[MAKHO]) VALUES (@valueMASODDH, @valueNGAY, @valueNHACC, @valueMANV, @valueMAKHO)";
                string query = "UPDATE PHIEUNHAP set [NGAY] = @value1,[MASODDH] = @value2,[MANV] = @value3,[makho] = @value4 WHERE [mapn] = @value0;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@value1", dateTimePicker.Text);
                cmd.Parameters.AddWithValue("@value2", cbbMasoDDH.Text);
                cmd.Parameters.AddWithValue("@value3", textMaNV.Text);
                cmd.Parameters.AddWithValue("@value4", cbbMaKho.Text);
                cmd.Parameters.AddWithValue("@value0", cbbMaPN.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("update thanh cong");

                //undo
                String cauTruyVanHoanTac = undoUpdateQueryPN;
                undoList.Push(cauTruyVanHoanTac);
            }
            catch (Exception ex)
            {
                MessageBox.Show("update that bai");
            }
            con.Close();
        }

        private void themBtn_Click(object sender, EventArgs e)
        {
            btnOK.Visible = true; btnHuy.Visible = true;
            // cac text cbb visible = true;
            themBtn.Enabled = false;
            cbbMasoDDH.Enabled = true;
            cbbMaVT.Enabled = true;
            cbbMaKho.Enabled = true;
            textMaNV.Text = Program.userName;
            textDG.Enabled = true;
            cbbMasoDDH.Enabled = true;
            textSL.Enabled = true;
            cbbMaPN.Enabled = true;
            dateTimePicker.Enabled = false;
            dateTimePicker.Text = DateTime.Now.ToString();
            btnAddCTPN.Enabled = false;
            //
            action = "AddPN";
        }

        private void btnAddCTPN_Click(object sender, EventArgs e)
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

            action = "AddCTPN";
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
                    SqlCommand cmd = new SqlCommand("DELETE CTPN WHERE MAPN = @valuePN ;" +
                        "DELETE PHIEUNHAP WHERE MAPN = @valuePN ", con, trans);
                    cmd.Parameters.AddWithValue("@valuePN", cbbMaPN.Text.Trim());
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    MessageBox.Show("Xoa thành công", "Thông báo", MessageBoxButtons.OK);



                    //undo
                    String cauTruyVanHoanTac = "";
                    cauTruyVanHoanTac = "INSERT INTO PHIEUNHAP ([MAPN], [NGAY], [MASODDH], [MANV], [MAKHO]) " +
                    "VALUES ('" + cbbMaPN.Text + "', '" + dateTimePicker.Text + "', '" + cbbMasoDDH.Text + "', " +
                    "'" + textMaNV.Text + "', '" + cbbMaKho.Text + "');" +
                    "INSERT INTO CTPN ([MAPN], [MAVT], [SOLUONG], [DONGIA]) " +
                    "VALUES ('" + cbbMaPN.Text + "', '" + cbbMaVT.Text + "', " + textSL.Text + ", '" + textDG.Text + "')";

                    undoList.Push(cauTruyVanHoanTac);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xoa that bai", "Thông báo", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }

        private void btnDelCTPN_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show("Ban co muon xoa khong?", "Thong bao", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //delete CTDDH where MasoDDH = 'MDDH04' and MAVT = 'MX07'
                if (result == DialogResult.Yes)
                {
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("DELETE CTPN WHERE MAPN = @valuePN and MAVT = @valueVT", con, trans);
                        cmd.Parameters.AddWithValue("@valuePN", cbbMaPN.Text.Trim());
                        cmd.Parameters.AddWithValue("@valueVT", cbbMaVT.Text.Trim());
                        cmd.ExecuteNonQuery();

                        

                        // xóa chi tiết phiếu nhập thì trừ luôn số lượng tồn đó luôn
                        SqlCommand cmd2 = new SqlCommand("UPDATE Vattu set SOLUONGTON = SOLUONGTON - @soLuong WHERE MAVT = @valueMaVT", con, trans);
                        cmd2.Parameters.AddWithValue("@valueMaVT", cbbMaVT.Text.Trim());
                        cmd2.Parameters.AddWithValue("@soLuong", textSL.Text);
                        cmd2.ExecuteNonQuery();
                        trans.Commit();
                        MessageBox.Show("Xoa thành công", "Thông báo", MessageBoxButtons.OK);
                        //undo
                        String cauTruyVanHoanTac = "";
                        cauTruyVanHoanTac = "INSERT INTO CTPN ([MAPN], [MAVT], [SOLUONG], [DONGIA]) " +
                        "VALUES ('" + cbbMaPN.Text + "', '" + cbbMaVT.Text + "', " + textSL.Text + ", '" + textDG.Text + "')";

                        String cauTruyVanHoanTac2 = "UPDATE Vattu set SOLUONGTON = SOLUONGTON + " + textSL.Text + " WHERE MAVT = " + cbbMaVT.Text.Trim();

                        undoList.Push(cauTruyVanHoanTac);
                        undoList.Push(cauTruyVanHoanTac2);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
                    }
                    con.Close();
                }
            }
        }

        private void ghiBtn_Click(object sender, EventArgs e)
        {
            action = "EditPN";
            btnOK.Visible = true;
            btnHuy.Visible = true;

            cbbMasoDDH.Enabled = true;
            cbbMaKho.Enabled = true;

            btnAddCTPN.Enabled = false;
            themBtn.Enabled = false;
            xoaBtn.Enabled = false;
            ghiBtn.Enabled = false;
            btnDelCTPN.Enabled = false;
            btnGhiCTPN.Enabled = false;
        }

        private void btnGhiCTPN_Click(object sender, EventArgs e)
        {
            oldQuantity = int.Parse(textSL.Text.Trim());
            cbbMaVT.Enabled = true;
            textSL.Enabled = true; textDG.Enabled = true;
            action = "EditCTPN";
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
                try
                {
                    String cauTruyVanHoanTac = undoList.Pop().ToString();
                    string query = cauTruyVanHoanTac;
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("undo thành công", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("undo that bai", "Thông báo", MessageBoxButtons.OK);

                }
                con.Close();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbbMaVT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbMaPN_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbMasoDDH_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void textSL_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
