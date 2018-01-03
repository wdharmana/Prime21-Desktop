using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Prime21
{
    public partial class FrmTransList : Form
    {

        private string sql, userId;
        private SqlConnection conn;

        private int idItem;

        public FrmTransList(String uId)
        {
            InitializeComponent();
            dbConnection dbConn = new dbConnection();
            dbConn.createConn();
            conn = dbConn.masterConn;

            userId = uId;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmTransList_Load(object sender, EventArgs e)
        {
            getList();
        }

        void getList()
        {
            sql = "SELECT t.id, t.trans_date, t.trans_time, m.title as movie, s.name as studio, sch.schedule_time, t.total, t.paid FROM tb_transactions t "+
                " LEFT JOIN tb_schedules sch ON sch.id = t.schedule_id LEFT JOIN tb_movies m ON m.id = sch.movie_id LEFT JOIN tb_studios s ON s.id = sch.studio_id  ORDER BY t.id DESC";
            conn.Open();
            SqlDataAdapter SDA = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder SCB = new SqlCommandBuilder(SDA);
            DataTable datatable = new DataTable();
            SDA.Fill(datatable);
            dataGridView1.DataSource = datatable;
            conn.Close();

            //this.Visible = true;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            SeatMap frm = new SeatMap(userId);
            frm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
            getList();
        }

        void delete()
        {
            sql = "DELETE FROM tb_transactions "
                + "WHERE id = " + idItem;
            

            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                idItem = Convert.ToInt16(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["id"].FormattedValue.ToString());
               
            }
            catch (Exception ex)
            {

            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            getList();
        }
    }
}
