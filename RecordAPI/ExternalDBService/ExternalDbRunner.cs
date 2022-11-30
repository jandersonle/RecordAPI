using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using NuGet.Protocol;

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
            this.init(iconnStr);

        }


        /// <summary>
        /// Method <c>init</c> initalizes an internal DataAcessLayer object passing in the provided database connection string.
        /// </summary>
        private void init(String iconnStr)
        {
            _dataAccess = new();
            _dataAccess.initialize(iconnStr);
            return;
        }


        /// <summary>
        /// Method <c>getExposure</c> retreives the Exposure with the provided EXPO_ID_NB and returns it in a json format.
        /// If the exposure does not exist, an empty string is returned.
        /// </summary>
        public String getExposure(long id)
        {
            String query = @"select *
                            from Exposure
                            where EXPO_ID_NB = " + id + ";";

            if (_dataAccess != null)
            {
                var res = JsonConvert.SerializeObject(_dataAccess.queryDatabase(query));
                return res;
            }

            return "";
        }


        /// <summary>
        /// Method <c>geCall</c> retreives the Call with the provided CALL_ID_NB and returns it in a json format
        /// If the call does not exist, an empty string is returned.
        /// </summary>
        public String getCall(long id)
        {
            String query = @"select *
                            from Call
                            where CALL_ID_NB = " + id + ";";

            if (_dataAccess != null)
            {
                var res = JsonConvert.SerializeObject(_dataAccess.queryDatabase(query));
                return res;
            }

            return "";
        }

    }
}
