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
using test1.qlnhanvien;

namespace test1
{
    public partial class quanlyhanghoa : Form
    {
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";
        int id_edit = -1;
        ThemHH addHH;
        SuaHH editHH;
        public quanlyhanghoa()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        private void quanlyhanghoa_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void LoadDataGrid()
        {
            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM hanghoa"; // Truy vấn lấy dữ liệu từ bảng
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gắn dữ liệu vào DataGridView
                    dataGridView1.DataSource = dt;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dataGridView1.Columns["id"].HeaderText = "ID";
                    dataGridView1.Columns["tenhang"].HeaderText = "Tên hàng";
                    dataGridView1.Columns["theloai"].HeaderText = "thể loại";
                    dataGridView1.Columns["soluong"].HeaderText = "tồn kho";
                    dataGridView1.Columns["nguongoc"].HeaderText = "xuất xứ";
                    dataGridView1.Columns["mota"].HeaderText = "mô tả";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

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

        private void button5_Click(object sender, EventArgs e)
        {
            addHH = new ThemHH();
            addHH.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id_edit != -1)
            {
                editHH = new SuaHH(id_edit);
                editHH.Show();
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
                delete_HH(id_edit);
            }
            else
            {
                MessageBox.Show("bạn cần chọn nhân viên cần xóa", "canh bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            }
        }
        private void delete_HH(int id)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hàng hóa này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection(MysqlCon))
                {
                    try
                    {
                        conn.Open();

                        // Lệnh DELETE
                        string query = "DELETE FROM hanghoa WHERE id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id);

                        // Thực thi lệnh
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa thông tin hàng hóa thành công!");
                            LoadDataGrid(); // Tải lại dữ liệu lên DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy hàng hóa để xóa!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
