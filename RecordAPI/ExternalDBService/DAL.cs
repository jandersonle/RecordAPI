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
        public void initialize(String iconnStr)
        {
            _connection = new(iconnStr);
        }


        /// <summary>
        /// Method <c>queryDatabase</c> executes the provided query on the database and returns all results within a DataTable.
        /// </summary>
        public DataTable queryDatabase(string iQuery = "")
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
                _connection.Open();
                rows_returned = sda.Fill(dt);
                _connection.Close();
            }

            return dt;
        
        }
    }
}
