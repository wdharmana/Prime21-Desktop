using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Prime21
{
    class dbConnection
    {
        public string connStr;
        public SqlConnection masterConn;

        public void createConn()
        {
            connStr = "DATA SOURCE='DESKTOP-H7N9FL9\\SQLEXPRESS';" +
                "INITIAL CATALOG='prime21';" +
                "INTEGRATED SECURITY=TRUE;";
            masterConn = new SqlConnection(connStr);
            masterConn.Open();

            if (masterConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Connection Failure");
                masterConn.Close();
            }
            masterConn.Close();
        }
    }
}
