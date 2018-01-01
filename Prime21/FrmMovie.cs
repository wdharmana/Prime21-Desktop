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
    public partial class FrmMovie : Form
    {
        private string sql;
        private SqlConnection conn;

        private int idItem;

        public FrmMovie()
        {
            InitializeComponent();
            dbConnection dbConn = new dbConnection();
            dbConn.createConn();
            conn = dbConn.masterConn;    
        }

        private void FrmMovie_Load(object sender, EventArgs e)
        {
            getList();
        }

        void getList()
        {
            sql = "SELECT * FROM tb_movies";
            conn.Open();
            SqlDataAdapter SDA = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder SCB = new SqlCommandBuilder(SDA);
            DataTable datatable = new DataTable();
            SDA.Fill(datatable);
            dataGridView1.DataSource = datatable;
            conn.Close();

            this.Visible = true;
            
        }

        void getDetail(int id)
        {
            sql = "SELECT * FROM tb_movies WHERE id = " + id + "";
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
                    txtTitle.Text = dr["title"].ToString();
                    txtDescription.Text = dr["description"].ToString();
                    txtYear.Text = dr["year"].ToString();
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
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtYear.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblIdBrg_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            emptyNew();
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

        void delete()
        {
            sql = "DELETE FROM tb_movies "
                + "WHERE id = " + idItem + " ";
           
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void save()
        {
           
            sql = "INSERT INTO tb_movies "
                + "VALUES ('" + txtTitle.Text + "', '" + txtDescription.Text + "', '" + txtYear.Text + "' )";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void update()
        {
            sql = "UPDATE tb_movies "
                + "SET title = '" + txtTitle.Text + "', "
                + "description = '" + txtDescription.Text + "', "
                + "year = '" + txtYear.Text + "' "
                + "WHERE id = " + idItem + " ";
            
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
            getList();
            btnNew.PerformClick();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
