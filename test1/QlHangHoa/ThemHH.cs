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

namespace test1.QlHangHoa
{
    public partial class ThemHH : Form
    {
        string MysqlCon = " server=127.0.0.1; user=root; database=quanlysieuthi; password= ";

        public ThemHH()
        {
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
        }

        private void ThemHH_Load(object sender, EventArgs e)
        {

        }
    }
}
