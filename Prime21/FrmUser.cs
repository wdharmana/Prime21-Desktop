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
    public partial class FrmUser : Form
    {

        private string sql;
        private SqlConnection conn;

        private int idItem;

        public FrmUser()
        {
            InitializeComponent();
            dbConnection dbConn = new dbConnection();
            dbConn.createConn();
            conn = dbConn.masterConn;
        }

        void getList()
        {
            sql = "SELECT id, username,address FROM tb_users";
            conn.Open();
            SqlDataAdapter SDA = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder SCB = new SqlCommandBuilder(SDA);
            DataTable datatable = new DataTable();
            SDA.Fill(datatable);
            dataGridView1.DataSource = datatable;
            conn.Close();
        }

        void getDetail(int id)
        {
            sql = "SELECT * FROM tb_users WHERE id = " + id + "";
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
                    txtUsername.Text = dr["username"].ToString();
                    txtPassword.Text = "";
                    txtAddress.Text = dr["address"].ToString();
                    lblPass.Text = "New Password";
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
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtAddress.Text = "";
            lblPass.Text = "Password";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            emptyNew();
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            getList();
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

            sql = "INSERT INTO tb_users "
                + "VALUES ('" + txtUsername.Text + "',HASHBYTES('md5','" + txtPassword.Text + "'), '" + txtAddress.Text + "' )";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            btnNew.PerformClick();
        }

        void update()
        {

            String pass = "";
             
            if(txtPassword.Text != "")
            {
                pass = "password = HASHBYTES('md5','" + txtPassword.Text + "'), ";
            }

            sql = "UPDATE tb_users "
                + "SET username = '" + txtUsername.Text + "', "
                + pass
                + "address = '" + txtAddress.Text + "' "
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
            getList();
            btnNew.PerformClick();
        }

        void delete()
        {
            sql = "DELETE FROM tb_users "
                + "WHERE id = " + idItem + " ";

            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            emptyNew();
        }
    }
}
