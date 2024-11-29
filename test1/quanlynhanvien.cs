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
using test1.qlnhanvien;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace test1
{
    public partial class quanlynhanvien : Form
    {
        //sql
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";
        int id_edit = -1 ;
        ThemNhanVien AddNV;
        SuaNhanVien EditNV;

        public quanlynhanvien()
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
                    string query = "SELECT * FROM nhanvien"; // Truy vấn lấy dữ liệu từ bảng
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gắn dữ liệu vào DataGridView
                    dataGridView1.DataSource = dt;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dataGridView1.Columns["id"].HeaderText = "ID";
                    dataGridView1.Columns["name"].HeaderText = "Tên";
                    dataGridView1.Columns["bophan"].HeaderText = "Bộ phận";
                    dataGridView1.Columns["calamviec"].HeaderText = "Ca làm";
                    dataGridView1.Columns["luong"].HeaderText = "Lương";
                    dataGridView1.Columns["cccd"].HeaderText = "CCCD";
                    dataGridView1.Columns["sdt"].HeaderText = "SĐT";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddNV = new ThemNhanVien();
            AddNV.Show();
        }

        private void quanlynhanvien_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id_edit != -1)
            {
                delete_NV(id_edit);
            }
            else
            {
                MessageBox.Show("bạn cần chọn nhân viên cần xóa", "canh bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            }
        }
        private void delete_NV(int id)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection(MysqlCon))
                {
                    try
                    {
                        conn.Open();

                        // Lệnh DELETE
                        string query = "DELETE FROM nhanvien WHERE id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id);

                        // Thực thi lệnh
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa nhân viên thành công!");
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id_edit != -1)
            {
                EditNV = new SuaNhanVien(id_edit);
                EditNV.Show();
            }
            else
            {
                MessageBox.Show("bạn cần chọn nhân viên cần sửa", "canh bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
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

        private void button3_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txtSearch.TextChanged += txtSearch_TextChanged;

            // Tải dữ liệu mặc định vào DataGridView
            LoadDataGrid();
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
                    string query = "SELECT * FROM nhanvien WHERE name LIKE @keyword";
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
