using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prime21
{
    public partial class Form1 : Form
    {
        String userId;
        public Form1(String id, String username)
        {
            InitializeComponent();
            lblWelcome.Text = "Welcome " + username + ",";
            userId = username;
        }


        private void movieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMovie frm = new FrmMovie();
            frm.ShowDialog();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void studioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStudio frm = new FrmStudio();
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            FrmLogin frm = new FrmLogin();
            frm.Dispose();
        }

        private void scheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSchedule frm = new FrmSchedule();
            frm.ShowDialog();
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTransList frm = new FrmTransList(userId);
            //SeatMap frm = new SeatMap();
            //FrmUser frm = new FrmUser();
            frm.ShowDialog();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
           FrmUser frm = new FrmUser();
            
            frm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
