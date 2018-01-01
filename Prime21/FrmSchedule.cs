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
    public partial class FrmSchedule : Form
    {

        private string sql;
        private SqlConnection conn;

        private int idItem, idStudio, idMovie;

        public FrmSchedule()
        {
            InitializeComponent();
            dbConnection dbConn = new dbConnection();
            dbConn.createConn();
            conn = dbConn.masterConn;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            emptyNew();
        }

        private void FrmSchedule_Load(object sender, EventArgs e)
        {
            getList();
            getMovies();
            getStudios();
        }

        void getList()
        {
            sql = "SELECT tb_schedules.id, tb_movies.title as movie, tb_studios.name as studio, schedule_time as time, price, status "+
                "FROM tb_schedules LEFT JOIN tb_movies ON tb_schedules.movie_id = tb_movies.id "+
                "LEFT JOIN tb_studios ON tb_schedules.studio_id = tb_studios.id ORDER BY id DESC";
            conn.Open();
            SqlDataAdapter SDA = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder SCB = new SqlCommandBuilder(SDA);
            DataTable datatable = new DataTable();
            SDA.Fill(datatable);
            dataGridView1.DataSource = datatable;
            conn.Close();

            this.Visible = true;

        }

        void getMovies()
        { 
            sql = "SELECT id, title FROM tb_movies ORDER BY title ASC";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            conn.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "tb_movies");
            cmbMovie.DisplayMember = "title";
            cmbMovie.ValueMember = "id";
            cmbMovie.DataSource = ds.Tables["tb_movies"];
            conn.Close();

        }

        void getStudios()
        {
            sql = "SELECT id, name FROM tb_studios ORDER BY name ASC";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            conn.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "tb_studios");
            cmbStudio.DisplayMember = "name";
            cmbStudio.ValueMember = "id";
            cmbStudio.DataSource = ds.Tables["tb_studios"];
            conn.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lblId.Text == "Auto")
            {
                save();
                getList();
            }
            else
            {
                update();
                getList();
            }
        }

        void save()
        {
            int status = 0;
            if(cbActive.Checked==true)
            {
                status = 1;
            }
            sql = "INSERT INTO tb_schedules VALUES (" + idMovie + ", " + idStudio + ", '" + txtTime.Text + "','"+txtPrice.Text+"',"+status+" )";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void update()
        {

            int status = 0;
            if (cbActive.Checked == true)
            {
                status = 1;
            }

            sql = "UPDATE tb_schedules "
                + "SET movie_id = " + idMovie + ", "
                + "studio_id = '" + idStudio + "', "
                + "status='"+status+"',"
                + "price='"+txtPrice.Text+"',"
                + "schedule_time = '" + txtTime.Text + "' "
                + "WHERE id = " + idItem + " ";

            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void cmbMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMovie.SelectedItem != null)
            {
                DataRowView drv = cmbMovie.SelectedItem as DataRowView;
                idMovie = Convert.ToInt32(drv.Row["id"].ToString());
            }
        }

        private void cmbStudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStudio.SelectedItem != null)
            {
                DataRowView drv = cmbStudio.SelectedItem as DataRowView;
                idStudio = Convert.ToInt32(drv.Row["id"].ToString());

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
            getList();
            btnNew.PerformClick();
        }

        void delete()
        {
            sql = "DELETE FROM tb_schedules "
                + "WHERE id = " + idItem + " ";

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
                lblId.Text = idItem.ToString();
                getDetail(idItem);
            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void getDetail(int id)
        {
            sql = "SELECT * FROM tb_schedules WHERE id = " + id + "";
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                SqlCommand cmdSql = new SqlCommand(sql, conn);
                SqlDataReader dr = cmdSql.ExecuteReader();
                if (dr.Read())
                {
                    lblId.Text = id.ToString();
                    txtPrice.Text = dr["price"].ToString();
                    txtTime.Text = dr["schedule_time"].ToString();
                    String stt = dr["status"].ToString();

                    if(stt=="0")
                    {
                        cbActive.Checked = false;
                    } else
                    {
                        cbActive.Checked = true;
                    }

                    cmbMovie.SelectedValue = dr["movie_id"].ToString();
                    cmbStudio.SelectedValue = dr["studio_id"].ToString();
                }
                else
                {
                    emptyNew();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
            finally
            {
                conn.Close();
            }


        }

        void emptyNew()
        {
            lblId.Text = "Auto";
            txtPrice.Text = "0";
            txtTime.Text = "";
            cmbMovie.SelectedText = null;
            cmbStudio.SelectedText = null;
        }
        
    }
}
