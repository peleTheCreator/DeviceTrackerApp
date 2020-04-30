Imports System.Data
Imports System.Data.SqlClient

Namespace DeviceTracker.AndriodDevice
    Public Class AndroidDeviceLog
        Private Conversions As Object

        Public Function UpdateDeviceTrackingregion(ByVal Body As String) As String
            Dim strJson As String = ""
            Dim connessioneDb = New DataBase()
            Dim objCommand = New SqlCommand()
            Dim mySqlAdapter = New SqlDataAdapter(objCommand)
            Dim Json As String = Nothing

            If connessioneDb.StatoConnessione = 0 Then
                connessioneDb.connettidb()

                If Not String.IsNullOrEmpty(Body) Then

                    If connessioneDb.StatoConnessione > 0 Then
                        objCommand.CommandType = CommandType.StoredProcedure
                        objCommand.CommandText = "Android.RestAPI_Json_DeviceTrackingregion"
                        objCommand.Connection = connessioneDb.Connessione
                        objCommand.Parameters.AddWithValue("@JSON", Body)
                        Dim objDataReader As SqlDataReader
                        objDataReader = objCommand.ExecuteReader()

                        If objDataReader.HasRows Then
                            objDataReader.Read()
                            Json = objDataReader(0).ToString()
                        End If
                    End If
                Else
                    Return "{'success': false,'message': 'Request does not have a body'}"
                End If
            End If

            connessioneDb.ChiudiDb()
            Return Json
        End Function

        Public Function UploadDeviceLocation(ByVal Body As String) As String
            Dim strJson As String = ""
            Dim connessioneDb = New DataBase()
            Dim objCommand = New SqlCommand()
            Dim mySqlAdapter = New SqlDataAdapter(objCommand)
            Dim Json As String = Nothing

            If connessioneDb.StatoConnessione = 0 Then
                connessioneDb.connettidb()

                If Not String.IsNullOrEmpty(Body) Then

                    If connessioneDb.StatoConnessione > 0 Then
                        objCommand.CommandType = CommandType.StoredProcedure
                        objCommand.CommandText = "Android.RestAPI_Json_DeviceLocation"
                        objCommand.Connection = connessioneDb.Connessione
                        objCommand.Parameters.AddWithValue("@JSON", Body)
                        Dim objDataReader As SqlDataReader
                        objDataReader = objCommand.ExecuteReader()

                        If objDataReader.HasRows Then
                            objDataReader.Read()
                            Json = objDataReader(0).ToString()
                            objDataReader.Close()
                        End If
                    End If
                Else
                    Return "{'success': false,'message': 'Request does not have a body'}"
                End If
            End If

            connessioneDb.ChiudiDb()
            Return Json
        End Function
    End Class
End Namespace
