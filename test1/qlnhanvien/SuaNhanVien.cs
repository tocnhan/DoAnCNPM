using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test1.qlnhanvien
{
    public partial class SuaNhanVien : Form
    {
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";

        private int id;
        public SuaNhanVien(int id)
        {
            this.id = id;
            InitializeComponent();
            MySqlConnection mySqlConnection = new MySqlConnection(MysqlCon);
            try
            {
                mySqlConnection.Open();
                MessageBox.Show("connection success");

                // Câu lệnh SQL để lấy dữ liệu
                string query = "SELECT id, tenbophan FROM bophan;";

                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Gán dữ liệu vào ComboBox
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "tenbophan"; // Tên cột hiển thị
                comboBox1.ValueMember = "id";    // Giá trị cột ẩn


                string[] values = { "1", "2", "3", "4" };

                // Gán danh sách giá trị vào ComboBox2
                comboBox2.Items.AddRange(values);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
            //lấy data đã chọn để gán dữ liệu vào

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
                    string query = "SELECT * FROM nhanvien WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Gán dữ liệu vào các TextBox và ComboBox
                        txtName.Text = reader["name"].ToString();
                        comboBox1.SelectedItem = reader["bophan"].ToString();
                        comboBox2.SelectedItem = reader["calamviec"].ToString();
                        txtLuong.Text = reader["luong"].ToString();
                        txtCccd.Text = reader["cccd"].ToString();
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


        private void SuaNhanVien_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            // Lấy thông tin từ các TextBox và ComboBox
            string ten = txtName.Text.Trim();
            int bophan = int.Parse(comboBox1.SelectedValue.ToString());
            int calam = int.Parse(comboBox2.SelectedItem.ToString());
            float luong = float.Parse(txtLuong.Text.Trim());
            string cccd = txtCccd.Text.Trim();
            string sdt = txtSdt.Text.Trim();
            
            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();

                    // Câu lệnh UPDATE
                    string query = "UPDATE nhanvien SET name=@ten, bophan=@bophan, calamviec=@calam, luong=@luong, cccd=@cccd, sdt=@sdt WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Gắn giá trị vào các tham số
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@ten", ten);
                    cmd.Parameters.AddWithValue("@bophan", bophan);
                    cmd.Parameters.AddWithValue("@calam", calam);
                    cmd.Parameters.AddWithValue("@luong", luong);
                    cmd.Parameters.AddWithValue("@cccd", cccd);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ban co muon thoat chuong trinh khong", "canh bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
