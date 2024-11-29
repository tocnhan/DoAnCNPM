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
    public partial class input_soluong : Form
    {
        public int EnteredNumber { get; private set; }
        public input_soluong()
        {
            InitializeComponent();
            button1.Click += (sender, e) =>
            {
                if (int.TryParse(textBox1.Text.Trim(), out int number))
                {
                    EnteredNumber = number; // Lưu số nhập vào
                    DialogResult = DialogResult.OK; // Đóng form với kết quả OK
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Gắn sự kiện cho nút Cancel
            button2.Click += (sender, e) =>
            {
                DialogResult = DialogResult.Cancel; // Đóng form với kết quả Cancel
            };
        }

        private void input_soluong_Load(object sender, EventArgs e)
        {

        }
    }
}
