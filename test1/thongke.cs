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

namespace test1
{
    public partial class thongke : Form
    {
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";
        public thongke()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        private void thongke_Load(object sender, EventArgs e)
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

                    // Truy vấn JOIN để lấy dữ liệu từ 2 bảng
                    string query = @"
                SELECT 
                    hanghoa.tenhang AS 'Tên Hàng', 
                    hd_sp.soluong AS 'Số Lượng', 
                    hanghoa.dongia AS 'Đơn Giá', 
                    (hd_sp.soluong * hanghoa.dongia) AS 'Thành Tiền'
                FROM 
                    hd_sp
                JOIN 
                    hanghoa ON hd_sp.id_sp = hanghoa.id";

                    // Sử dụng MySqlDataAdapter để lấy dữ liệu
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gắn dữ liệu vào DataGridView
                    dataGridView1.DataSource = dt;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dataGridView1.Columns["Tên Hàng"].HeaderText = "Tên Hàng";
                    dataGridView1.Columns["Số Lượng"].HeaderText = "Số Lượng";
                    dataGridView1.Columns["Đơn Giá"].HeaderText = "Đơn Giá";
                    dataGridView1.Columns["Thành Tiền"].HeaderText = "Thành Tiền";

                    // Định dạng tiền tệ cho cột "Thành Tiền"
                    dataGridView1.Columns["Thành Tiền"].DefaultCellStyle.Format = "C";
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu xảy ra lỗi
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
