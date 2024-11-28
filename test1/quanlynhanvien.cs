using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test1.qlnhanvien;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace test1
{
    public partial class quanlynhanvien : Form
    {
        ThemNhanVien AddNV;
        SuaNhanVien EditNV;

        public quanlynhanvien()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddNV = new ThemNhanVien();
            AddNV.Show();
        }

        private void quanlynhanvien_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
