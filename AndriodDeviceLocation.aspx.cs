using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeviceTracker
{
    public partial class AndriodDeviceLocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long deviceId = Convert.ToInt64(Request.QueryString["deviceId"]);
                su_LoadDeviceLocation(deviceId);
            }
        }

        private void su_LoadDeviceLocation(long deviceId)
        {
            var strSQL = string.Empty;
            var dbConnect = new DataBase();

            if (dbConnect.StatoConnessione == 0)
            {
                dbConnect.connettidb();
            }
            var objCommand = new SqlCommand();
            strSQL = $@"SELECT TOP 5 [LONGITUDE], [LATITUDE], [CREATED_AT] 
                       FROM [Android].[DEVICE_LOCATION]
                       WHERE[ID_ANDROID_DEVICE] = {deviceId} ORDER BY CREATED_AT DESC ";
            objCommand.CommandText = strSQL;
            objCommand.CommandType = CommandType.Text;
            objCommand.Connection = dbConnect.Connessione;
            SqlDataReader reader = objCommand.ExecuteReader();
            //if reader is null, display location does not exit
            rptMarkers.DataSource = reader;
            rptMarkers.DataBind();
        }
    }
}