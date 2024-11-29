using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test1.QlHoaDon
{
    public partial class add_HD_HH : Form
    {
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";
        private int id;
        input_soluong input_Soluong;
        public add_HD_HH(int id)
        {
            this.id = id;
            InitializeComponent();
            dataGridView2.CellClick += dataGridView2_CellClick;
            dataGridView1.CellClick += dataGridView1_CellClick;

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
                    dataGridView2.DataSource = dt;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dataGridView2.Columns["id"].HeaderText = "ID";
                    dataGridView2.Columns["tenhang"].HeaderText = "Tên hàng";
                    dataGridView2.Columns["dongia"].HeaderText = "đơn giá";


                    MySqlDataAdapter da_sl = new MySqlDataAdapter(query_selected, conn);
                    DataTable dt_sl= new DataTable();
                    da_sl.Fill(dt_sl);

                    // Gắn dữ liệu vào DataGridView
                    dataGridView1.DataSource = dt_sl;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dataGridView1.Columns["id"].HeaderText = "ID";
                    dataGridView1.Columns["tenhang"].HeaderText = "Tên hàng";
                    dataGridView1.Columns["soluong"].HeaderText = "số lượng";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void add_HD_HH_Load(object sender, EventArgs e)
        {

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
            dataGridView2.Columns.Add(buttonColumn);
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
            dataGridView1.Columns.Add(buttonColumn1);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView2.Columns["Action"].Index)
            {
                // Lấy giá trị ID từ cột "ID" của dòng tương ứng
                int id_sp = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["ID"].Value);
                string name =Convert.ToString( dataGridView2.Rows[e.RowIndex].Cells["tenhang"].Value);
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
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
