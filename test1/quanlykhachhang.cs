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

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
