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
        private int id;
        public TaoMoiHD(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void TaoMoiHD_Load(object sender, EventArgs e)
        {

        }
        private void AddButtonColumnToDataGridView()
        {
            // Tạo cột kiểu nút
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "Action";
            buttonColumn.HeaderText = "Thao tác";
            buttonColumn.Text = "thêm"; // Văn bản hiển thị trên nút
            buttonColumn.UseColumnTextForButtonValue = true; // Dùng văn bản mặc định cho nút

            // Thêm cột vào DataGridView
            dt_sp.Columns.Add(buttonColumn);
        }
    }
}
