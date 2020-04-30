Imports System
Imports System.Configuration
Imports System.Data.SqlClient

Namespace DeviceTracker
    Public Class DataBase
        Private ConnessioneDB As SqlConnection = New SqlConnection()

        Public ReadOnly Property Connessione As SqlConnection
            Get
                Return ConnessioneDB
            End Get
        End Property

        Public ReadOnly Property StatoConnessione As Integer
            Get
                Return ConnessioneDB.State
            End Get
        End Property

        Public Function connettidb() As Object
            Try
                ConnessioneDB.ConnectionString = fun_Get_ConnectionString()
                ConnessioneDB.Open()
            Catch ex As Exception
                Return -1
            End Try

            Return ConnessioneDB
        End Function

        Public Function ChiudiDb() As Object
            Dim ChiudiDbRet As Object = Nothing

            Try

                If CInt(ConnessioneDB.State) = 1 Then
                    ConnessioneDB.Close()
                    ConnessioneDB.Dispose()
                    SqlConnection.ClearPool(ConnessioneDB)
                End If

                ChiudiDbRet = 1
            Catch ex As Exception
                ChiudiDbRet = -1
                Return Nothing
            End Try

            Return ChiudiDbRet
        End Function

        Public Function fun_Get_ConnectionString() As String
            Dim strConn As String = ConfigurationManager.ConnectionStrings("PtmldataConnectionString").ConnectionString
            Return strConn
        End Function
    End Class
End Namespace
