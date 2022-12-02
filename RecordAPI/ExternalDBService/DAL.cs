using Microsoft.Data.SqlClient;
using System.Data;

namespace RecordAPI.ExternalDBService
{

    /// <summary>
    /// Class <c>DAL</c> (Data Access Layer) represents a class of core SQL commands for interaction with a database.
    /// </summary>
    public class DAL
    {
        private SqlConnection? _connection;


        /// <summary>
        /// Method <c>initialize</c> estbalishes a SQL connection object binded to the provided connection string.
        /// </summary>
        public bool Initialize(String iconnStr)
        {
            _connection = new(iconnStr);
            bool isValid = false;
            try
            {
                _connection.Open();
                isValid = true;

            } catch (SqlException ex) 
            {
                isValid = false;
                _connection = null;
            }

            return isValid;

        }


        /// <summary>
        /// Method <c>isValidConnection</c> returns if the internal SqlConnection object is valid.
        /// </summary>
        public bool IsValidConnection()
        {
            return _connection != null;
        }


        /// <summary>
        /// Method <c>queryDatabase</c> executes the provided query on the database and returns all results within a DataTable.
        /// </summary>
        public DataTable QueryDatabase(string iQuery = "")
        {
            if (iQuery.Equals(""))
            {
                return new DataTable();
            }
            
            DataTable dt = new();
            var rows_returned = 0;
            using(SqlCommand cmd = _connection.CreateCommand())
            using(SqlDataAdapter sda = new(cmd))
            { 
                cmd.CommandText = iQuery;
                cmd.CommandType = CommandType.Text;
                rows_returned = sda.Fill(dt);
            }

            return dt;
        
        }
    }
}
