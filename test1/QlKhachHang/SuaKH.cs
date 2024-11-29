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

namespace test1.QlKhachHang
{
    public partial class SuaKH : Form
    {
        private int id;
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";
        public SuaKH(int id)
        {
            
            this.id = id;
            InitializeComponent();
            LoadDataToFields(id);
        }
        private void LoadDataToFields(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();

                    // Truy vấn dữ liệu dựa trên ID
                    string query = "SELECT * FROM khachhang WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Gán dữ liệu vào các TextBox và ComboBox
                        txtName.Text = reader["name"].ToString();
                        txtSdt.Text = reader["sdt"].ToString();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void SuaKH_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ban co muon thoat chuong trinh khong", "canh bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox và ComboBox
            string ten = txtName.Text.Trim();
            string sdt = txtSdt.Text.Trim();

            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();

                    // Câu lệnh UPDATE
                    string query = "UPDATE khachhang SET name=@ten, sdt=@sdt WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Gắn giá trị vào các tham số
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@ten", ten);
                    cmd.Parameters.AddWithValue("@sdt", sdt);

                    // Thực thi lệnh
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                        // Tải lại dữ liệu lên DataGridView
                        Close();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
