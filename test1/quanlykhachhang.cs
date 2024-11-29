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
using test1.QlHangHoa;
using test1.QlKhachHang;

namespace test1
{
    public partial class quanlykhachhang : Form
    {
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";
        int id_edit = -1;
        ThemKH addKH;
        SuaKH editKH;
        public quanlykhachhang()
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
                    string query = "SELECT * FROM khachhang"; // Truy vấn lấy dữ liệu từ bảng
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gắn dữ liệu vào DataGridView
                    dataGridView1.DataSource = dt;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dataGridView1.Columns["id"].HeaderText = "ID";
                    dataGridView1.Columns["name"].HeaderText = "Tên Khách hàng";
                    dataGridView1.Columns["sdt"].HeaderText = "số điện thoại";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void quanlykhachhang_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addKH = new ThemKH();
            addKH.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id_edit != -1)
            {
                editKH = new SuaKH(id_edit);
                editKH.Show();
            }
            else
            {
                MessageBox.Show("bạn cần chọn hàng hóa cần sửa", "canh bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id_edit != -1)
            {
                delete_KH(id_edit);
            }
            else
            {
                MessageBox.Show("bạn cần chọn tên khách hàng cần xóa", "canh bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            }
        }
        private void delete_KH(int id)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection(MysqlCon))
                {
                    try
                    {
                        conn.Open();

                        // Lệnh DELETE
                        string query = "DELETE FROM khachhang WHERE id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id);

                        // Thực thi lệnh
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa khách hàng thành công!");
                            LoadDataGrid(); // Tải lại dữ liệu lên DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên để xóa!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
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
                    string query = "SELECT * FROM khachhang WHERE name LIKE @keyword";
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
