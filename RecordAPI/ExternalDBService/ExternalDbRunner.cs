using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Data;

namespace RecordAPI.ExternalDBService
{

    /// <summary>
    /// Class <c>ExternalDbRunner</c> acts as a service runner for fetching Exposures and Calls from the database.
    /// Relies on an internal Data Access Layer (DAL) object for database level interactions.
    /// </summary>
    public class ExternalDbRunner
    {

        private DAL? _dataAccess;

        public ExternalDbRunner(String iconnStr)
        {
            this.Init(iconnStr);

        }


        /// <summary>
        /// Method <c>init</c> initalizes an internal DataAcessLayer object passing in the provided database connection string.
        /// </summary>
        private void Init(String iconnStr)
        {
            _dataAccess = new();
            bool isValid =_dataAccess.Initialize(iconnStr);
            return;
        }


        /// <summary>
        /// Method <c>getExposure</c> retreives the Exposure with the provided EXPO_ID_NB and returns it in a json format.
        /// If the exposure does not exist, an empty string is returned.
        /// </summary>
        public String GetExposure(long id)
        {
            String query = @"select *
                            from Exposure
                            where EXPO_ID_NB = " + id + ";";

            if (_dataAccess != null && _dataAccess.IsValidConnection())
            {
                var res = JsonConvert.SerializeObject(_dataAccess.QueryDatabase(query));
                return res;
            }

            return "";
        }


        /// <summary>
        /// Method <c>geCall</c> retreives the Call with the provided CALL_ID_NB and returns it in a json format
        /// If the call does not exist, an empty string is returned.
        /// </summary>
        public String GetCall(long id)
        {
            String query = @"select *
                            from Call
                            where CALL_ID_NB = " + id + ";";

            if (_dataAccess != null && _dataAccess.IsValidConnection())
            {
                //DataTable res = _dataAccess.QueryDatabase(query);
                //String context = "";
                //for(int i = 1; i < res.Columns.Count; i++)
                //{
                //    if (res.Rows[0][i] != DBNull.Value)
                //    {
                //        if (i != res.Columns.Count - 1)
                //        {
                //            // TODO handle imcompatible string converts
                //            // Parse the 32bit integers into strings
                //            String curr = (string)res.Rows[0][i];
                //            curr = curr.Trim();
                //            context += curr + ",";
                //        }
                //        else
                //        {
                //            context += res.Rows[0][i];
                //        }
                //    } 
                //    else
                //    {
                //        continue;
                //    }
                //}
                var res = JsonConvert.SerializeObject(_dataAccess.QueryDatabase(query));
                return res;
            }

            return "";
        }

    }
}
