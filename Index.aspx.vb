Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.UI.WebControls

Namespace DeviceTracker
    Public Partial Class Index
        Inherits Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not IsPostBack Then
                su_LoadData()
            End If
        End Sub

        Protected Sub lnkSelect_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim DEVICEID As Integer = Convert.ToInt32(TryCast(sender, LinkButton).CommandArgument)
            Response.Redirect("~/AndriodDeviceLocation.aspx?deviceId=" & DEVICEID)
        End Sub

        Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
            '  su_LoadData();
        End Sub

        Private Function su_LoadDeviceId() As List(Of Long)
            Dim deviceIds = New List(Of Long)()
            Dim strSQL = String.Empty
            Dim dbConnect = New DataBase()

            If dbConnect.StatoConnessione = 0 Then
                dbConnect.connettidb()
            End If

            Dim objCommand = New SqlCommand()
            strSQL = "SELECT  DISTINCT [ID_ANDROID_DEVICE] FROM [Android].[DEVICE_LOCATION]"
            objCommand.CommandText = strSQL
            objCommand.CommandType = CommandType.Text
            objCommand.Connection = dbConnect.Connessione
            Dim reader As SqlDataReader = objCommand.ExecuteReader()

            While reader.Read()
                deviceIds.Add(Convert.ToInt64(reader("ID_ANDROID_DEVICE")))
            End While

            dbConnect.ChiudiDb()
            Return deviceIds
        End Function

        Private Sub su_LoadData()
            Dim strSQL = String.Empty
            Dim deveiceIds = su_LoadDeviceId()
            Dim sourceTable As DataTable = New DataTable()
            sourceTable.Columns.Add("DEVICE")
            sourceTable.Columns.Add("IMEI")
            sourceTable.Columns.Add("LASTCHECKIN")
            sourceTable.Columns.Add("STATUS")
            sourceTable.Columns.Add("DEVICEID")

            For Each deveiceId In deveiceIds
                Dim dbConnect = New DataBase()

                If dbConnect.StatoConnessione = 0 Then
                    dbConnect.connettidb()
                End If

                Dim objCommand = New SqlCommand()
                objCommand.CommandType = CommandType.Text
                objCommand.Connection = dbConnect.Connessione
                strSQL = String.Empty
                strSQL = $"SELECT TOP 1 NAME_DEVICE, COD_IMEI,STATUS, ID_ANDROID_DEVICE FROM Android.vs_Get_DeviceRegion
                            WHERE [ID_ANDROID_DEVICE] = {deveiceId} 
                            ORDER BY CREATED_AT DESC; 
                            SELECT TOP 1 [CREATED_AT] 
                            FROM [Android].[DEVICE_LOCATION]
                             WHERE[ID_ANDROID_DEVICE] = {deveiceId} ORDER BY CREATED_AT DESC"
                objCommand.CommandText = strSQL
                Dim datarow As DataRow = sourceTable.NewRow()
                Dim reader As SqlDataReader = objCommand.ExecuteReader()

                While reader.Read()
                    datarow("DEVICE") = reader("NAME_DEVICE")
                    datarow("IMEI") = reader("COD_IMEI")
                    datarow("STATUS") = reader("STATUS")
                    datarow("DEVICEID") = reader("ID_ANDROID_DEVICE")
                End While

                Dim createdAt As Date

                While reader.NextResult()

                    While reader.Read()
                        createdAt = CDate(reader("CREATED_AT"))
                        datarow("LASTCHECKIN") = su_relativeDate(createdAt)
                    End While
                End While

                sourceTable.Rows.Add(datarow)
                dbConnect.ChiudiDb()
            Next

            gvDeviceTracker.DataSource = sourceTable
            gvDeviceTracker.DataBind()
        End Sub

        Public Function su_relativeDate(ByVal createdAt As Date) As String
            Const SECOND As Integer = 1
            Const MINUTE As Integer = 60 * SECOND
            Const HOUR As Integer = 60 * MINUTE
            Const DAY As Integer = 24 * HOUR
            Const MONTH As Integer = 30 * DAY
            Dim ts = New TimeSpan(Date.Now.Ticks - createdAt.Ticks)
            Dim delta As Double = Math.Abs(ts.TotalSeconds)
            If delta < 1 * MINUTE Then Return If(ts.Seconds = 1, "one second ago", ts.Seconds & " seconds ago")
            If delta < 2 * MINUTE Then Return "a minute ago"
            If delta < 45 * MINUTE Then Return ts.Minutes & " minutes ago"
            If delta < 90 * MINUTE Then Return "an hour ago"
            If delta < 24 * HOUR Then Return ts.Hours & " hours ago"
            If delta < 48 * HOUR Then Return "yesterday"
            If delta < 30 * DAY Then Return ts.Days & " days ago"

            If delta < 12 * MONTH Then
                Dim months As Integer = Convert.ToInt32(Math.Floor(ts.Days / 30))
                Return If(months <= 1, "one month ago", months & " months ago")
            Else
                Dim years As Integer = Convert.ToInt32(Math.Floor(ts.Days / 365))
                Return If(years <= 1, "one year ago", years & " years ago")
            End If
        End Function

        Protected Sub gvDeviceTracker_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            If e.Row.RowType = DataControlRowType.DataRow Then

                If Equals(e.Row.Cells(3).Text, "True") Then
                    e.Row.Cells(3).Text = "Yes"
                ElseIf Equals(e.Row.Cells(3).Text, "False") Then
                    e.Row.Cells(3).Text = "No"
                    e.Row.Cells(3).BackColor = Color.PaleVioletRed
                    e.Row.Cells(3).CssClass = "cellColor"
                End If
            End If
        End Sub
    End Class
End Namespace
