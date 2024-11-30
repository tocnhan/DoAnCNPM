using MySql.Data.MySqlClient;
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

namespace test1.QlHoaDon
{
    public partial class TaoMoiHD : Form
    {
        soluong input_soluong;
        private int id;
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";
        public TaoMoiHD(int id)
        {
            InitializeComponent();
            MySqlConnection mySqlConnection = new MySqlConnection(MysqlCon);
            try
            {
                mySqlConnection.Open();
                MessageBox.Show("connection success");

                // Câu lệnh SQL để lấy dữ liệu thể loại
                string query_theloai = "SELECT id, name FROM nhanvien;";

                MySqlCommand cmd = new MySqlCommand(query_theloai, mySqlConnection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Gán dữ liệu vào ComboBox theloai
                cb_nv.DataSource = dt;
                cb_nv.DisplayMember = "name"; // Tên cột hiển thị
                cb_nv.ValueMember = "id";    // Giá trị cột ẩn

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
            this.id = id;
            dt_sp.CellClick += dataGridView2_CellClick;
            dt_addsp.CellClick += dataGridView1_CellClick;

            // Thêm cột nút vào DataGridView
            AddButtonColumnToDataGridView();
            AddButtonColumnToDataGridView1();
            LoadDataGrid();
        }
        private void LoadDataGrid()
        {
            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, tenhang, dongia FROM hanghoa"; // Truy vấn lấy dữ liệu từ bảng
                    string query_selected = $"SELECT hd_sp.id AS id,  hanghoa.tenhang AS tenhang, hd_sp.soluong AS soluong FROM hd_sp JOIN hanghoa ON hd_sp.id_sp = hanghoa.id WHERE hd_sp.id_hd = {id};";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gắn dữ liệu vào DataGridView
                    dt_sp.DataSource = dt;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dt_sp.Columns["id"].HeaderText = "ID";
                    dt_sp.Columns["tenhang"].HeaderText = "Tên hàng";
                    dt_sp.Columns["dongia"].HeaderText = "đơn giá";


                    MySqlDataAdapter da_sl = new MySqlDataAdapter(query_selected, conn);
                    DataTable dt_sl = new DataTable();
                    da_sl.Fill(dt_sl);

                    // Gắn dữ liệu vào DataGridView
                    dt_addsp.DataSource = dt_sl;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dt_addsp.Columns["id"].HeaderText = "ID";
                    dt_addsp.Columns["tenhang"].HeaderText = "Tên hàng";
                    dt_addsp.Columns["soluong"].HeaderText = "số lượng";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void AddButtonColumnToDataGridView()
        {
            // Tạo cột kiểu nút
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "Action";
            buttonColumn.HeaderText = "Thao tác";
            buttonColumn.Text = "thêm"; // Văn bản hiển thị trên nút
            buttonColumn.UseColumnTextForButtonValue = true; // Dùng văn bản mặc định cho nút

            // Thêm cột vào DataGridView
            dt_sp.Columns.Add(buttonColumn);
        }
        private void AddButtonColumnToDataGridView1()
        {
            // Tạo cột kiểu nút
            DataGridViewButtonColumn buttonColumn1 = new DataGridViewButtonColumn();
            buttonColumn1.Name = "Action";
            buttonColumn1.HeaderText = "Thao tác";
            buttonColumn1.Text = "bỏ"; // Văn bản hiển thị trên nút
            buttonColumn1.UseColumnTextForButtonValue = true; // Dùng văn bản mặc định cho nút

            // Thêm cột vào DataGridView
            dt_addsp.Columns.Add(buttonColumn1);
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dt_sp.Columns["Action"].Index)
            {
                // Lấy giá trị ID từ cột "ID" của dòng tương ứng
                int id_sp = Convert.ToInt32(dt_sp.Rows[e.RowIndex].Cells["ID"].Value);
                string name = Convert.ToString(dt_sp.Rows[e.RowIndex].Cells["tenhang"].Value);
                // Hiển thị hoặc xử lý giá trị ID
                using (input_soluong = new soluong())
                {
                    if (input_soluong.ShowDialog() == DialogResult.OK)
                    {
                        // Lấy giá trị số được nhập
                        int enteredNumber = input_soluong.EnteredNumber;
                        MessageBox.Show($"Bạn đã nhập số: {enteredNumber}");
                        using (MySqlConnection conn = new MySqlConnection(MysqlCon))
                        {
                            try
                            {
                                conn.Open();

                                // Câu lệnh INSERT
                                string query = "INSERT INTO hd_sp (id_sp, id_hd, soluong) VALUES (@id, @id_hd, @soluong);";
                                MySqlCommand cmd = new MySqlCommand(query, conn);

                                // Gắn giá trị vào các tham số
                                cmd.Parameters.AddWithValue("@id", id_sp);
                                cmd.Parameters.AddWithValue("@id_hd", id);
                                cmd.Parameters.AddWithValue("@soluong", enteredNumber);

                                // Thực thi lệnh
                                int rowsInserted = cmd.ExecuteNonQuery();

                                if (rowsInserted > 0)
                                {
                                    MessageBox.Show("Thêm mới thành công!");
                                    // Tải lại dữ liệu lên DataGridView (nếu có)

                                }
                                else
                                {
                                    MessageBox.Show("Không có thay đổi nào được thực hiện.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi: " + ex.Message);
                            }
                        }
                        // Hiển thị số đã nhập

                    }
                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dt_addsp.Columns["Action"].Index)
            {
                // Lấy giá trị ID từ cột "ID" của dòng tương ứng
                int id_selec = Convert.ToInt32(dt_addsp.Rows[e.RowIndex].Cells["ID"].Value);
                delete_selected(id_selec);


            }
        }
        private void delete_selected(int id_sl)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn bỏ mặt hàng này?", "Xác nhận bỏ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection(MysqlCon))
                {
                    try
                    {
                        conn.Open();

                        // Lệnh DELETE
                        string query = "DELETE FROM hd_sp WHERE id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id_sl);

                        // Thực thi lệnh
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("bỏ mặt hàng thành công!");
                            LoadDataGrid(); // Tải lại dữ liệu lên DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy mặt hàng để bỏ!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }
        private void TaoMoiHD_Load(object sender, EventArgs e)
        {

        }
        private void thanhtien()
        {
            string query = @"
            SELECT SUM(hd_sp.soluong * hanghoa.dongia) AS tong_gia_tien
            FROM hd_sp
            JOIN hanghoa ON hd_sp.id_sp = hanghoa.id
            WHERE hd_sp.id_hd = @id_hd";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(MysqlCon))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Truyền tham số vào query
                        cmd.Parameters.AddWithValue("@id_hd", id);

                        // Thực thi truy vấn và lấy kết quả
                        object result = cmd.ExecuteScalar();

                        // Hiển thị kết quả trong TextBox (nếu kết quả null thì gán 0)
                        txt_tt.Text = result != DBNull.Value ? result.ToString() : "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadDataGrid_unslsp();
            thanhtien();
        }
        private void LoadDataGrid_unslsp()
        {
            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();
                     // Truy vấn lấy dữ liệu từ bảng
                    string query_selected = $"SELECT hd_sp.id AS id,  hanghoa.tenhang AS tenhang, hd_sp.soluong AS soluong FROM hd_sp JOIN hanghoa ON hd_sp.id_sp = hanghoa.id WHERE hd_sp.id_hd = {id};";
                    


                    MySqlDataAdapter da_sl = new MySqlDataAdapter(query_selected, conn);
                    DataTable dt_sl = new DataTable();
                    da_sl.Fill(dt_sl);

                    // Gắn dữ liệu vào DataGridView
                    dt_addsp.DataSource = dt_sl;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dt_addsp.Columns["id"].HeaderText = "ID";
                    dt_addsp.Columns["tenhang"].HeaderText = "Tên hàng";
                    dt_addsp.Columns["soluong"].HeaderText = "số lượng";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void cb_nv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            // Lấy giá trị từ TextBox và ComboBox
            
            int id_khach_hang;
            int id_nhan_vien;
            string sdt = txt_sdt.Text;

            
            if (!int.TryParse(txt_sdt.Text, out id_khach_hang))
            {
                MessageBox.Show("Vui lòng nhập ID khách hàng hợp lệ!");
                return;
            }
            if (cb_nv.SelectedValue == null || !int.TryParse(cb_nv.SelectedValue.ToString(), out id_nhan_vien))
            {
                MessageBox.Show("Vui lòng chọn ID nhân viên hợp lệ!");
                return;
            }

            // Câu lệnh SQL UPDATE
            string findCustomerQuery = "SELECT id FROM khachhang WHERE sdt = @sdt";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(MysqlCon))
                {
                    conn.Open();

                    using (MySqlCommand findCustomerCmd = new MySqlCommand(findCustomerQuery, conn))
                    {
                        findCustomerCmd.Parameters.AddWithValue("@sdt", sdt);

                        object result = findCustomerCmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out id_khach_hang))
                        {
                            // Bước 2: Cập nhật hóa đơn
                            string updateQuery = @"
                            UPDATE hoadon
                            SET khach_hang = @id_khach_hang, 
                                nhan_vien = @id_nhan_vien
                            WHERE id = @id";

                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@id", id);
                                updateCmd.Parameters.AddWithValue("@id_khach_hang", id_khach_hang);
                                updateCmd.Parameters.AddWithValue("@id_nhan_vien", id_nhan_vien);

                                int rowsAffected = updateCmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Cập nhật hóa đơn thành công!");
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy hóa đơn với ID này!");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khách hàng với số điện thoại này!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }
    
    }
}
