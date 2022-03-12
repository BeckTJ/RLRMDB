using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace RLRMBL
{
    public class Connection
    {
        string connectionString;

        SqlConnection connection = new SqlConnection();
        public Connection()
        {
            var datasource = @"localhost";
            var database = "RLRMDB";
            var username = "SA";
            var password = "FR*@ger12";

            connectionString = @"Data Source=" + datasource + "; Initial Catalog=" + database + ";TrustServerCertificate=True;Persist Security Info=True;User ID=" + username + ";Password=" + password;
        }
        public void OpenConnection()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        public void CloseConnection()
        {
            connection.Close();
        }
        public void executeQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }
        public SqlDataReader DataReader(string query)
        {
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        public object ShowDataInGridView(string query)
        {
            DataSet set = new DataSet();
            object data = set.Tables[0];
            return data;
        }
    }
}