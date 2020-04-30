using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DeviceTracker
{
    public class DataBase
    {
        private SqlConnection ConnessioneDB = new SqlConnection();

        public SqlConnection Connessione
        {
            get
            {
                return ConnessioneDB;
            }
        }

        public int StatoConnessione
        {
            get
            {
                return (int)ConnessioneDB.State;
            }
        }

        public object connettidb()
        {
            try
            {
                ConnessioneDB.ConnectionString = fun_Get_ConnectionString();
                ConnessioneDB.Open();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return ConnessioneDB;
        }

        public object ChiudiDb()
        {
            object ChiudiDbRet = null;
            try
            {
                if ((int)ConnessioneDB.State == 1)
                {
                    ConnessioneDB.Close();
                    ConnessioneDB.Dispose();
                    SqlConnection.ClearPool(ConnessioneDB);
                }

                ChiudiDbRet = 1;
            }
            catch (Exception ex)
            {
                ChiudiDbRet = -1;
                return null;
            }

            return ChiudiDbRet;
        }

        public string fun_Get_ConnectionString()
        {
            string strConn = ConfigurationManager.ConnectionStrings["PtmldataConnectionString"].ConnectionString;
            return strConn;
        }
    }
}