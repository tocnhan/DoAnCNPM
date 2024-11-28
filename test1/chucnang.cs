using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test1
{
    public partial class chucnang : Form
    {
        private string username;
        quanlyhanghoa frm_hanghoa;
        quanlyhoadon frm_hoadon;
        quanlykhachhang frm_khachhang;
        quanlynhanvien frm_nhanvien;
        thongke frm_thongke;
        Home frm_home;

        
        public chucnang(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(frm_home == null)
            {
                frm_home = new Home();
                frm_home.FormClosed += Frm_home_FormClosed;
                frm_home.MdiParent = this;
                frm_home.Dock = DockStyle.Fill;
                frm_home.Show();
            }
            else
            {
                frm_home.Activate();
            }
        }

        private void Frm_home_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_home = null;
        }

        private void chucnang_Load(object sender, EventArgs e)
        {
            label4.Text = $"xin chào {username}!";
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (frm_nhanvien == null)
            {
                frm_nhanvien = new quanlynhanvien();
                frm_nhanvien.FormClosed += Frm_nhanvien_FormClosed;
                frm_nhanvien.MdiParent = this;
                frm_nhanvien.Dock = DockStyle.Fill;
                frm_nhanvien.Show();
            }
            else
            {
                frm_nhanvien.Activate();
            }
        }

        private void Frm_nhanvien_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_nhanvien = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ban co muon thoat chuong trinh khong", "canh bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (frm_khachhang == null)
            {
                frm_khachhang = new quanlykhachhang();
                frm_khachhang.FormClosed += Frm_khachhang_FormClosed;
                frm_khachhang.MdiParent = this;
                frm_khachhang.Dock = DockStyle.Fill;
                frm_khachhang.Show();
            }
            else {
                frm_khachhang.Activate();
            }
        }

        private void Frm_khachhang_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_khachhang = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (frm_hoadon == null)
            {
                frm_hoadon = new quanlyhoadon();
                frm_hoadon.FormClosed += Frm_hoadon_FormClosed;
                frm_hoadon.MdiParent = this;
                frm_hoadon.Dock = DockStyle.Fill;
                frm_hoadon.Show();
            }
            else { 
                frm_hoadon.Activate();
            }
        }

        private void Frm_hoadon_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_hoadon = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (frm_hanghoa == null)
            {
                frm_hanghoa = new quanlyhanghoa();
                frm_hanghoa.FormClosed += Frm_hanghoa_FormClosed;
                frm_hanghoa.MdiParent= this;
                frm_hanghoa.Dock = DockStyle.Fill;
                frm_hanghoa.Show();
            }
            else
            {
                frm_hanghoa.Activate();
            }
        }

        private void Frm_hanghoa_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_hanghoa=null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (frm_thongke == null)
            {
                frm_thongke = new thongke();
                frm_thongke.FormClosed += Frm_thongke_FormClosed;
                frm_thongke.MdiParent = this;
                frm_thongke.Dock = DockStyle.Fill;
                frm_thongke.Show();
            }
            else
            {
                frm_thongke.Activate();
            }
        }

        private void Frm_thongke_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_thongke = null;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
