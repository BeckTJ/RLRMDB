using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ULRMDB
{
    public class connection
    {
        SqlConnection conn;
        string connString;

        public connection()
        {
            var datasource = @"localhost";
            var database = "RLRMDB";
            var username = "SA";
            var password = "FR*@ger12";

            connString = @"Data Source=" + datasource + "; Initial Catalog=" + database + ";TrustServerCertificate=True;Persist Security Info=True;User ID=" + username + ";Password=" + password;
        }
        public void openConnection()
        {
            conn = new SqlConnection(connString);
            conn.Open();
        }
        public void closeConnection()
        {
            conn.Close();
        }
        public void executeQueries(string query)
        {
            SqlCommand cmd = new SqlCommand(query,conn);
            cmd.ExecuteNonQuery();
        }
        public SqlDataReader dataReader(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        public object showDataInGridView(string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataSet set = new DataSet();
            object data = set.Tables[0];
            return data;
        }          
    }
}