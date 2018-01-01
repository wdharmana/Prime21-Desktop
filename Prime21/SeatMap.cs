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
    public partial class SeatMap : Form
    {

        private string sql;
        private SqlConnection conn;
        private int rowSeat = 0, columnSeat = 0, idSch, price = 0;
        private List<Button> buttons = new List<Button>();

        public SeatMap()
        {
            InitializeComponent();
            dbConnection dbConn = new dbConnection();
            dbConn.createConn();
            conn = dbConn.masterConn;
        }

        private void SeatMap_Load(object sender, EventArgs e)
        {
            getSchedules();
            panelScreen.Visible = false;
        }

        void resizePanel()
        {
            panel2.Width = this.Width - panel1.Width;
        }

        private void draw()
        {

            clear();

            var alpha = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            int width = 40; int top = 0; int len = columnSeat, row = rowSeat, screenWidth = 0;
           

            for (int r = 0; r < row; r++)
            {

               screenWidth = 0;

                string alp = alpha[r];

                top += width + 5;
                int space = 20;
               
                for (int i = 0; i < len; i++)
                {
                    Button newButton = new Button();
                    newButton.Width = width;

                    if (i > 0)
                    {
                        space += (width + 5);
                    }
                    else
                    {
                        space += width / 2;
                    }

                    if (i == len / 2)
                    {

                        space += (width);

                    }

                    screenWidth += width + 10;
                  
                    newButton.Left = space;
                    newButton.Top = top;
                    newButton.Text = alp + (i + 1);
                    newButton.Height = width;

                    newButton.Click += (source, e) =>
                    {
                        if (newButton.BackColor == Color.Red)
                        {
                            MessageBox.Show("Already booked!");
                        }
                        else
                        {
                            newButton.BackColor = Color.Yellow;
                        }
                    };

                    if (i % 2 == 0)
                    {
                        newButton.BackColor = Color.Red;
                    }

                    buttons.Add(newButton);
                    panel2.Controls.Add(newButton);
                }

            }

            panelScreen.Top = top + 60;
            panelScreen.Width = screenWidth;
            panelScreen.Height = 10;
            panelScreen.Left = 20;

            var margin = panelScreen.Margin;
            margin.Bottom = 20;
           panelScreen.Margin = margin;


        }

        void clear()
        {

            foreach(Button b in buttons)
            {
                this.Controls.Remove(b);
            }
           
        }

        private void cmbSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDetailSchedule(Convert.ToInt32(cmbSchedule.SelectedValue));
        }

        private void SeatMap_ResizeEnd(object sender, EventArgs e)
        {
            
        }

        private void SeatMap_Resize(object sender, EventArgs e)
        {
            resizePanel();
        }

        void getSchedules()
        {
                sql = "SELECT CONCAT('(',tb_movies.title,') - ',tb_studios.name,' - ',tb_schedules.schedule_time) AS display, tb_schedules.id, tb_movies.title as movie, tb_studios.name as studio, schedule_time as time, price, status " +
                    "FROM tb_schedules LEFT JOIN tb_movies ON tb_schedules.movie_id = tb_movies.id " +
                    "LEFT JOIN tb_studios ON tb_schedules.studio_id = tb_studios.id ORDER BY id DESC";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                conn.Open();
                DataSet ds = new DataSet();
                da.Fill(ds, "tb_schedules");
                cmbSchedule.DisplayMember = "display";
                cmbSchedule.ValueMember = "id";
                cmbSchedule.DataSource = ds.Tables["tb_schedules"];
                conn.Close();

        }


        void getDetailSchedule(int id)
        {
            sql = "SELECT sch.id, sch.price, s.row_seat, s.column_seat "+
                " FROM tb_schedules sch INNER JOIN tb_studios s ON s.id = sch.studio_id "+
                " WHERE sch.id  = " + id + "";
            
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmdSql = new SqlCommand(sql, conn);
                SqlDataReader dr = cmdSql.ExecuteReader();
                if (dr.Read())
                {
                    idSch = id;
                    rowSeat = Convert.ToInt32(dr["row_seat"].ToString());
                    columnSeat = Convert.ToInt32(dr["column_seat"].ToString());
                    price = Convert.ToInt32(dr["price"].ToString());
                }
                else
                {
                    
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            
            draw();
            panelScreen.Visible = true;


        }
    }
}
