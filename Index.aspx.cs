using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;


namespace DeviceTracker
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                su_LoadData();
            }
        }

        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            int DEVICEID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            Response.Redirect("~/AndriodDeviceLocation.aspx?deviceId=" + DEVICEID);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
          //  su_LoadData();
        }

        private List<long> su_LoadDeviceId()
        {
            var deviceIds = new List<long>();

            var strSQL = string.Empty;
            var dbConnect = new DataBase();

            if (dbConnect.StatoConnessione == 0)
            {
                dbConnect.connettidb();
            }
            var objCommand = new SqlCommand();
            strSQL = "SELECT  DISTINCT [ID_ANDROID_DEVICE] FROM [Android].[DEVICE_LOCATION]";
            objCommand.CommandText = strSQL;
            objCommand.CommandType = CommandType.Text;
            objCommand.Connection = dbConnect.Connessione;
            SqlDataReader reader = objCommand.ExecuteReader();
            while (reader.Read())
            {
                deviceIds.Add(Convert.ToInt64(reader["ID_ANDROID_DEVICE"]));
            }
            dbConnect.ChiudiDb();

            return deviceIds;
        }

        private void su_LoadData()
        {
            var strSQL = string.Empty;

            var deveiceIds = su_LoadDeviceId();

            DataTable sourceTable = new DataTable();
            sourceTable.Columns.Add("DEVICE");
            sourceTable.Columns.Add("IMEI");
            sourceTable.Columns.Add("LASTCHECKIN");
            sourceTable.Columns.Add("STATUS");
            sourceTable.Columns.Add("DEVICEID");
            foreach (var deveiceId in deveiceIds)
            {
                var dbConnect = new DataBase();

                if (dbConnect.StatoConnessione == 0)
                {
                    dbConnect.connettidb();
                }
                var objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.Text;
                objCommand.Connection = dbConnect.Connessione;

                strSQL = string.Empty;
                strSQL = $@"SELECT TOP 1 NAME_DEVICE, COD_IMEI,STATUS, ID_ANDROID_DEVICE FROM Android.vs_Get_DeviceRegion
                            WHERE [ID_ANDROID_DEVICE] = {deveiceId} 
                            ORDER BY CREATED_AT DESC; 
                            SELECT TOP 1 [CREATED_AT] 
                            FROM [Android].[DEVICE_LOCATION]
                             WHERE[ID_ANDROID_DEVICE] = {deveiceId} ORDER BY CREATED_AT DESC";

                objCommand.CommandText = strSQL;
                DataRow datarow = sourceTable.NewRow();

                SqlDataReader reader = objCommand.ExecuteReader();

                while (reader.Read())
                {
                    datarow["DEVICE"] = reader["NAME_DEVICE"];
                    datarow["IMEI"] = reader["COD_IMEI"];
                    datarow["STATUS"] = reader["STATUS"];
                    datarow["DEVICEID"] = reader["ID_ANDROID_DEVICE"];
                }

                DateTime createdAt;
                while (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        createdAt = (DateTime)reader["CREATED_AT"];
                        datarow["LASTCHECKIN"] = su_relativeDate(createdAt);
                    }
                }
                sourceTable.Rows.Add(datarow);

                dbConnect.ChiudiDb();
            }
            gvDeviceTracker.DataSource = sourceTable;
            gvDeviceTracker.DataBind();
        }


        public string su_relativeDate(DateTime createdAt)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - createdAt.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
        protected void gvDeviceTracker_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == "True")
                {
                    e.Row.Cells[3].Text = "Yes";
                }
                else if (e.Row.Cells[3].Text == "False")
                {
                    e.Row.Cells[3].Text = "No";
                    e.Row.Cells[3].BackColor = Color.PaleVioletRed;
                    e.Row.Cells[3].CssClass = "cellColor";


                }
            }
        }
    }
}