using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test1.QlHoaDon;

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

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
}
