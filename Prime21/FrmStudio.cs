﻿using System;
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
    public partial class FrmStudio : Form
    {

        private string sql;
        private SqlConnection conn;

        private int idItem;

        public FrmStudio()
        {
            InitializeComponent();
            dbConnection dbConn = new dbConnection();
            dbConn.createConn();
            conn = dbConn.masterConn;    
        }


        void getList()
        {
            sql = "SELECT * FROM tb_studios";
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
            sql = "SELECT * FROM tb_studios WHERE id = " + id + "";
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
                    txtName.Text = dr["name"].ToString();
                    txtRow.Text = dr["row_seat"].ToString();
                    txtColumn.Text = dr["column_seat"].ToString();
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
            txtName.Text = "";
            txtRow.Text = "0";
            txtColumn.Text = "0";
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            emptyNew();
        }

        void delete()
        {
            sql = "DELETE FROM tb_studios "
                + "WHERE id = " + idItem + " ";

            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void save()
        {

            sql = "INSERT INTO tb_studios "
                + "VALUES ('" + txtName.Text + "', '" + txtRow.Text + "', '" + txtColumn.Text + "' )";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void update()
        {
            sql = "UPDATE tb_studios "
                + "SET title = '" + txtName.Text + "', "
                + "row_seat = '" + txtRow.Text + "', "
                + "column_seat = '" + txtColumn.Text + "' "
                + "WHERE id = " + idItem + " ";

            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        private void btnSave_Click_1(object sender, EventArgs e)
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

        private void FrmStudio_Load(object sender, EventArgs e)
        {
            getList();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
