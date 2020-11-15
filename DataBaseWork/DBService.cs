using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace lab1Lect
{
    class DBService
    {
        
        private  SqlConnection _sqlConnection = null;
        
        public SqlConnection GetSqlConnection() { return _sqlConnection; }
        
        public SqlConnection ShowConnectionStatus()
        {
            return _sqlConnection;
        }

        public bool OpenConnection()
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
               return true;
             
            }
            return false;
           
        }
        public bool CloseConnection()
        {
            if (_sqlConnection?.State != ConnectionState.Closed)
            {
                _sqlConnection?.Close();
                return true;
            }
            return false;
        }

    }
}
