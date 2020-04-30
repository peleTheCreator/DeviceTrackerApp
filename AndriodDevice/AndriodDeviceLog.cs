using System.Data;
using System.Data.SqlClient;


namespace DeviceTracker.AndriodDevice
{
    public class AndroidDeviceLog
    {
        private object Conversions;

        public string UpdateDeviceTrackingregion(string Body)
        {
            string strJson = "";
            var connessioneDb = new DataBase();
            var objCommand = new SqlCommand();
            var mySqlAdapter = new SqlDataAdapter(objCommand);
            string Json = null;
            if (connessioneDb.StatoConnessione == 0)
            {
                connessioneDb.connettidb();
                if (!string.IsNullOrEmpty(Body))
                {
                    if (connessioneDb.StatoConnessione > 0)
                    {
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandText = "Android.RestAPI_Json_DeviceTrackingregion";
                        objCommand.Connection = connessioneDb.Connessione;
                        objCommand.Parameters.AddWithValue("@JSON", Body);
                        SqlDataReader objDataReader;
                        objDataReader = objCommand.ExecuteReader();
                        if (objDataReader.HasRows)
                        {
                            objDataReader.Read();
                            Json = objDataReader[0].ToString();
                        }
                    }
                }
                else
                {
                    return "{'success': false,'message': 'Request does not have a body'}";
                }
            }

            connessioneDb.ChiudiDb();
            return Json;
        }

        public string UploadDeviceLocation(string Body)
        {
            string strJson = "";
            var connessioneDb = new DataBase();
            var objCommand = new SqlCommand();
            var mySqlAdapter = new SqlDataAdapter(objCommand);
            string Json = null;
            if (connessioneDb.StatoConnessione == 0)
            {
                connessioneDb.connettidb();
                if (!string.IsNullOrEmpty(Body))
                {
                    if (connessioneDb.StatoConnessione > 0)
                    {
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandText = "Android.RestAPI_Json_DeviceLocation";
                        objCommand.Connection = connessioneDb.Connessione;
                        objCommand.Parameters.AddWithValue("@JSON", Body);
                        SqlDataReader objDataReader;
                        objDataReader = objCommand.ExecuteReader();
                        if (objDataReader.HasRows)
                        {
                            objDataReader.Read();
                            Json = objDataReader[0].ToString();
                            objDataReader.Close();
                        }
                    }
                }
                else
                {
                    return "{'success': false,'message': 'Request does not have a body'}";
                }
            }

            connessioneDb.ChiudiDb();
            return Json;
        }
    }
}