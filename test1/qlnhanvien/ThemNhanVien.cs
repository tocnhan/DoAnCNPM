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
                MessageBox.Show("Giá trị được chọn: " + selectedValue);
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
