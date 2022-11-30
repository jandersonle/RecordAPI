using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace RecordAPI.ExternalDBService
{
    public class ExternalDbRunner
    {

        private DAL? _dataAccess;

        public ExternalDbRunner(String iconnStr)
        {
            this.init(iconnStr);

        }

        private void init(String iconnStr)
        {
            _dataAccess = new();
            _dataAccess.initialize(iconnStr);
            return;
        }

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
