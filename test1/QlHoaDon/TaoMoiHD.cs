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
        private List<string> selectedProducts = new List<string>();
        private BindingSource bsSanPham = new BindingSource();
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";
        int id_edit = -1;
        public TaoMoiHD()
        {
            InitializeComponent();
            LoadDataGrid_khachhang();
            LoadDataGrid_hanghoa();
            // Tạo cột checkbox
            DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
            chkColumn.Name = "Chon";
            chkColumn.HeaderText = "Chọn";
            chkColumn.Width = 50; // Đặt độ rộng cột (tùy chỉnh)
            chkColumn.TrueValue = true; // Giá trị khi được tích
            chkColumn.FalseValue = false; // Giá trị khi không được tích

            // Thêm cột vào DataGridView
            dataHH.Columns.Add(chkColumn);

        }

        private void LoadDataGrid_khachhang()
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
                    dataKH.DataSource = dt;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dataKH.Columns["id"].HeaderText = "ID";
                    dataKH.Columns["name"].HeaderText = "Tên";
                    dataKH.Columns["sdt"].HeaderText = "SĐT";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void LoadDataGrid_hanghoa()
        {
            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, tenhang, dongia FROM hanghoa"; // Truy vấn lấy dữ liệu từ bảng
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataHH.EditMode = DataGridViewEditMode.EditOnEnter;
                    // Gắn dữ liệu vào DataGridView
                    bsSanPham.DataSource = dt;
                    dataHH.DataSource = dt;

                    // Tùy chỉnh tiêu đề cột (nếu cần)
                    dataHH.Columns["id"].HeaderText = "ID";
                    dataHH.Columns["tenhang"].HeaderText = "Ten";
                    dataHH.Columns["dongia"].HeaderText = "đơn giá";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void TaoMoiHD_Load(object sender, EventArgs e)
        {
            dataHH.CellValueChanged += dataHH_CellValueChanged;
            dataHH.CurrentCellDirtyStateChanged += dataHH_CurrentCellDirtyStateChanged;
        }

        private void dataKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataHH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (e.ColumnIndex == dataHH.Columns["Chon"].Index)
            {
                bool isChecked = Convert.ToBoolean(dataHH.Rows[e.RowIndex].Cells["Chon"].Value);
                MessageBox.Show($"Sản phẩm dòng {e.RowIndex + 1} đã {(isChecked ? "được chọn" : "bỏ chọn")}!");
            }*/
        }
        private void dataHH_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu cột thay đổi là cột checkbox
            if (e.ColumnIndex == dataHH.Columns["Chon"].Index)
            {
                bool isChecked = Convert.ToBoolean(dataHH.Rows[e.RowIndex].Cells["Chon"].Value);
                if (isChecked)
                {
                    MessageBox.Show($"Sản phẩm dòng {e.RowIndex + 1} đã được chọn!");
                }
                else
                {
                   
                }
            }
        }
        private void dataHH_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataHH.IsCurrentCellDirty)
            {
                dataHH.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SaveCheckedStates();
            string filter = txtSearchHH.Text.Trim();

            if (string.IsNullOrEmpty(filter))
            {
                bsSanPham.RemoveFilter(); // Hiển thị toàn bộ dữ liệu
            }
            else
            {
                // Lọc dựa trên cột Tên sản phẩm (TenSP)
                bsSanPham.Filter = $"tenhang LIKE '%{filter}%'";
            }
            RestoreCheckedStates();
        }
        private void SaveCheckedStates()
        {
            selectedProducts.Clear();
            foreach (DataGridViewRow row in dataHH.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Chon"].Value))
                {
                    string maSP = row.Cells["id"].Value.ToString();
                    selectedProducts.Add(maSP);
                }
            }
        }
        private void RestoreCheckedStates()
        {
            foreach (DataGridViewRow row in dataHH.Rows)
            {
                string maSP = row.Cells["id"].Value.ToString();
                row.Cells["Chon"].Value = selectedProducts.Contains(maSP);
            }
        }


        private void txtSearchKH_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchKH.Text.Trim(); // Lấy từ khóa tìm kiếm

            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();

                    // Truy vấn tìm kiếm theo tên
                    string query = "SELECT * FROM khachhang WHERE sdt LIKE @keyword";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    // Đọc dữ liệu và hiển thị lên DataGridView
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataKH.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
    }
}
