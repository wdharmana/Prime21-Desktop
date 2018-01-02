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
    public partial class FrmLogin : Form
    {

        private string sql;
        private SqlConnection conn;

        private int idItem;

        public FrmLogin()
        {
            InitializeComponent();
            dbConnection dbConn = new dbConnection();
            dbConn.createConn();
            conn = dbConn.masterConn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sql = "SELECT * FROM tb_users WHERE username = '" + txtUsername.Text + "' AND password=HASHBYTES('md5', '"+txtPassword.Text+"')";
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
                    String id       = dr["id"].ToString();
                    String username = dr["username"].ToString();

                    Form1 frm = new Form1(id, username);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Username & password wrong!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
