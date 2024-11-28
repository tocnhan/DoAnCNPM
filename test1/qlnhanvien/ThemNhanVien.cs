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
using MySql.Data.MySqlClient;

namespace test1.qlnhanvien
{
    public partial class ThemNhanVien : Form
    {
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";

        public ThemNhanVien()
        {
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
            
        }
        /*private void LoadComboBoxData()
        {
            
                try
                {
                    // Câu lệnh SQL để lấy dữ liệu
                    string query = "SELECT id, tenbophan FROM bophan;";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "tenbbophan"; // Tên cột hiển thị
                    comboBox1.ValueMember = "id";    // Giá trị cột ẩn
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }*/
        
        private void ThemNhanVien_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                string selectedValue = comboBox1.SelectedValue.ToString();
                MessageBox.Show("Bộ phận làm việc được chọn: " + selectedValue);
            }
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
            // Lấy dữ liệu từ các TextBox và ComboBox
            string ten = txtName.Text;
            int bophan = int.Parse(comboBox1.SelectedValue.ToString());
            int calam = int.Parse(comboBox2.SelectedItem.ToString());
            float luong = float.Parse(txtLuong.Text);
            string cccd = txtCccd.Text;
            string sdt = txtSdt.Text;

            // Kiểm tra dữ liệu hợp lệ
            if (cccd.Length != 12)
            {
                MessageBox.Show("CCCD phải gồm 12 chữ số!");
                return;
            }

            // Chèn dữ liệu vào MySQL
            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO nhanvien (name, bophan, calamviec, luong, cccd, sdt) " +
                                   "VALUES (@ten, @bophan, @calam, @luong, @cccd, @sdt)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ten", ten);
                    cmd.Parameters.AddWithValue("@bophan", bophan);
                    cmd.Parameters.AddWithValue("@calam", calam);
                    cmd.Parameters.AddWithValue("@luong", luong);
                    cmd.Parameters.AddWithValue("@cccd", cccd);
                    cmd.Parameters.AddWithValue("@sdt", sdt);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Thêm nhân viên thành công!");
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi thêm nhân viên!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void ClearFields()
        {
            txtName.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            txtLuong.Clear();
            txtCccd.Clear();
            txtSdt.Clear();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                string selectedValue = comboBox2.SelectedItem.ToString();
                MessageBox.Show("Ca làm việc được chọn: " + selectedValue);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
