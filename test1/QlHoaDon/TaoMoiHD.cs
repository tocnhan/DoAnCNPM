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

namespace test1.QlHoaDon
{
    public partial class TaoMoiHD : Form
    {
        private int id;
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";
        public TaoMoiHD(int id)
        {
            InitializeComponent();
            this.id = id;
            dt_sp.CellClick += dataGridView2_CellClick;
            dt_addsp.CellClick += dataGridView1_CellClick;

            // Thêm cột nút vào DataGridView
            AddButtonColumnToDataGridView();
            AddButtonColumnToDataGridView1();
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
                    dt_addsp.DataSource = dt;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dt_addsp.Columns["id"].HeaderText = "ID";
                    dt_addsp.Columns["tenhang"].HeaderText = "Tên hàng";
                    dt_addsp.Columns["dongia"].HeaderText = "đơn giá";


                    MySqlDataAdapter da_sl = new MySqlDataAdapter(query_selected, conn);
                    DataTable dt_sl = new DataTable();
                    da_sl.Fill(dt_sl);

                    // Gắn dữ liệu vào DataGridView
                    dt_sp.DataSource = dt_sl;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dt_sp.Columns["id"].HeaderText = "ID";
                    dt_sp.Columns["tenhang"].HeaderText = "Tên hàng";
                    dt_sp.Columns["soluong"].HeaderText = "số lượng";
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
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView2.Columns["Action"].Index)
            {
                // Lấy giá trị ID từ cột "ID" của dòng tương ứng
                int id_sp = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["ID"].Value);
                string name = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells["tenhang"].Value);
                // Hiển thị hoặc xử lý giá trị ID
                using (input_Soluong = new input_soluong())
                {
                    if (input_Soluong.ShowDialog() == DialogResult.OK)
                    {
                        // Lấy giá trị số được nhập
                        int enteredNumber = input_Soluong.EnteredNumber;
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
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Action"].Index)
            {
                // Lấy giá trị ID từ cột "ID" của dòng tương ứng
                int id_selec = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
                delete_selected(id_selec);


            }
        }
        private void TaoMoiHD_Load(object sender, EventArgs e)
        {

        }
        
    }
}
