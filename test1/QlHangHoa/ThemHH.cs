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

        private void ThemHH_Load(object sender, EventArgs e)
        {

        }
    }
}
