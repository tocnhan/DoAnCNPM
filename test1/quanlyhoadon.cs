using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test1.QlHoaDon;
using test1.qlnhanvien;

namespace test1
{
    public partial class quanlyhoadon : Form
    {
        TaoMoiHD add_hd;
        SuaHD edit_hd;
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";
        int id_edit = -1;
        public quanlyhoadon()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM hoadon"; // Truy vấn lấy dữ liệu từ bảng
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gắn dữ liệu vào DataGridView
                    dataGridView1.DataSource = dt;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dataGridView1.Columns["id"].HeaderText = "ID";
                    dataGridView1.Columns["khach_hang"].HeaderText = "mã khách hàng";
                    dataGridView1.Columns["nhan_vien"].HeaderText = "mã nhân viên";
                    dataGridView1.Columns["ngay"].HeaderText = "ngày tạo hóa đơn";
                    dataGridView1.Columns["thanhtien"].HeaderText = "thành tiền";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void quanlyhoadon_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime ngayHienTai = DateTime.Now.Date;
            string chuoiNgay = ngayHienTai.ToString("M-d-yyyy");
            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();
                    string query_cl = "SELECT MAX(id) AS id FROM hoadon";

                    int maxId = 0;
                    // Câu lệnh INSERT
                    string query = "INSERT INTO hoadon (ngay) VALUES (@day)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Gắn giá trị vào các tham số
                    cmd.Parameters.AddWithValue("@day", chuoiNgay);
                    
                    // Thực thi lệnh
                    int rowsInserted = cmd.ExecuteNonQuery();

                    if (rowsInserted > 0)
                    {
                        
                        // Tải lại dữ liệu lên DataGridView (nếu có)
                            try
                            {

                                MySqlCommand command = new MySqlCommand(query_cl, conn);
                                
                                    object result = command.ExecuteScalar();

                                    if (result != DBNull.Value && result != null)
                                    {
                                        maxId = Convert.ToInt32(result);
                                        
                                    }

                                add_hd = new TaoMoiHD(maxId, 0);
                                add_hd.Show();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Lỗi: {ex.Message}");
                            }
                        
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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Lấy ID từ dòng đã chọn
                id_edit = Convert.ToInt32(row.Cells["id"].Value);

                // Gọi hàm để tải dữ liệu vào các TextBox và ComboBox

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim(); // Lấy từ khóa tìm kiếm

            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();

                    // Truy vấn tìm kiếm theo tên
                    string query = "SELECT * FROM hoadon WHERE ngay LIKE @keyword";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    // Đọc dữ liệu và hiển thị lên DataGridView
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id_edit != -1)
            {
                add_hd = new TaoMoiHD(id_edit, 1);
                add_hd.Show();
            }
            else
            {
                MessageBox.Show("bạn cần chọn hóa đơn cần sửa", "canh bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            }
        }
    }
}
