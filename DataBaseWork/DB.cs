using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace lab1Lect
{
    class DB
    {
        
        private  SqlConnection _sqlConnection = null;
        
        public SqlConnection GetSqlConnection() { return _sqlConnection; }
        public void ShowConnectionStatus(SqlConnection connection)
        {
            Console.WriteLine("***INFO ABOUT YOUR CONNECTION***");
            Console.WriteLine($"Database location: {connection.DataSource}");
            Console.WriteLine($"Database name: {connection.Database}");
            Console.WriteLine($"Timeout : {connection.ConnectionTimeout}");
            Console.WriteLine($"Connection state: {connection.State}\n");

        }

        public void OpenConnection()
        {
            SqlConnectionStringBuilder cnStringBuilder = new SqlConnectionStringBuilder
            {
                InitialCatalog = "Lecturer",
                DataSource = @"DESKTOP-5RBNBON\SQLEXPRESS",
                ConnectTimeout = 30,
                IntegratedSecurity = true
            };
            _sqlConnection = new SqlConnection { ConnectionString = cnStringBuilder.ConnectionString };
            _sqlConnection.Open();
            if (_sqlConnection?.State == ConnectionState.Open)
            {
                Console.WriteLine("Connection is successful");
            }
            ///else
            ShowConnectionStatus(_sqlConnection);
        }
        public void CloseConnection()
        {
            if (_sqlConnection?.State != ConnectionState.Closed)
            {
                _sqlConnection?.Close();
                Console.WriteLine("Connection is closed");
            }
        }

    }
}
