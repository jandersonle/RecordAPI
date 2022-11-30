using Microsoft.Data.SqlClient;
using System.Data;

namespace RecordAPI.ExternalDBService
{
    public class DAL
    {
        private SqlConnection _connection;

        public void initialize(String iconnStr)
        {
            _connection = new(iconnStr);
        }

        public DataTable queryDatabase(string iQuery)
        {
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
