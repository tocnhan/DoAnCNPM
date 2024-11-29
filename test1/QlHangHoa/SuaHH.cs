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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace test1.QlHangHoa
{
    public partial class SuaHH : Form
    {
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";
        private int id;
        public SuaHH(int id)
        {
            this.id = id;
            InitializeComponent();
            MySqlConnection mySqlConnection = new MySqlConnection(MysqlCon);
            try
            {
                mySqlConnection.Open();
                MessageBox.Show("connection success");

                // Câu lệnh SQL để lấy dữ liệu thể loại
                string query_theloai = "SELECT id, mota FROM theloai;";

                MySqlCommand cmd = new MySqlCommand(query_theloai, mySqlConnection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Gán dữ liệu vào ComboBox theloai
                theloai.DataSource = dt;
                theloai.DisplayMember = "mota"; // Tên cột hiển thị
                theloai.ValueMember = "id";    // Giá trị cột ẩn

                // Câu lệnh SQL để lấy dữ liệu nguongoc
                string query_nguongoc = "SELECT id, quocgia FROM nguongoc;";

                MySqlCommand cmd_ng = new MySqlCommand(query_nguongoc, mySqlConnection);
                MySqlDataAdapter da_ng = new MySqlDataAdapter(cmd_ng);
                DataTable dt_ng = new DataTable();
                da_ng.Fill(dt_ng);

                // Gán dữ liệu vào ComboBox
                nguongoc.DataSource = dt_ng;
                nguongoc.DisplayMember = "quocgia"; // Tên cột hiển thị
                nguongoc.ValueMember = "id";
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
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
                    string query = "SELECT * FROM hanghoa WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Gán dữ liệu vào các TextBox và ComboBox
                        txtName.Text = reader["tenhang"].ToString();
                        theloai.SelectedValue = reader["theloai"].ToString();
                        txtSoluong.Text = reader["soluong"].ToString();
                        nguongoc.SelectedValue = reader["nguongoc"].ToString();
                        txtMota.Text = reader["mota"].ToString();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void SuaHH_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox và ComboBox
            string ten = txtName.Text.Trim();
            int theloai_mota = int.Parse(theloai.SelectedValue.ToString());
            int sluong = int.Parse(txtSoluong.Text.Trim());
            int quocgia = int.Parse(nguongoc.SelectedValue.ToString());
            string mota = txtMota.Text.Trim();

            using (MySqlConnection conn = new MySqlConnection(MysqlCon))
            {
                try
                {
                    conn.Open();

                    // Câu lệnh UPDATE
                    string query = "UPDATE hanghoa SET tenhang=@ten, theloai=@theloai, soluong=@soluong, nguongoc=@nguongoc, mota=@mota WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Gắn giá trị vào các tham số
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@ten", ten);
                    cmd.Parameters.AddWithValue("@theloai", theloai_mota);
                    cmd.Parameters.AddWithValue("@soluong", sluong);
                    cmd.Parameters.AddWithValue("@nguongoc", quocgia);
                    cmd.Parameters.AddWithValue("@mota", mota);

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
