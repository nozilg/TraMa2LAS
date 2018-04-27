Imports Cizeta.TraMa2LAS
Imports Cizeta.TraMaAuth

Module Module1

    Sub Main()
        Dim Username As String = "marco.dallera"
        Dim Password As String = ""
        Dim StationName As String = "ST60.08"
        Dim BadgeCode As String = "gothamcity"
        Dim a As Authenticator
        Dim ret As LoginResult

        Try
            Console.WriteLine(String.Format("Logging worker with badge {0} on station {1}...", BadgeCode, StationName))
            a = New Authenticator(AuthenticationMode.BadgeCode)
            ret = a.LoginByBadgeCode(BadgeCode, StationName)
            Select Case ret
                Case LoginResult.Ok
                    With a.CurrentWorker
                        Console.WriteLine(String.Format("LoginName: {0}", .LoginName))
                        Console.WriteLine(String.Format("Name: {0}", .Name))
                        Console.WriteLine(String.Format("Code: {0}", .Code))
                        Console.WriteLine(String.Format("RoleName: {0}", .RoleName))
                        Console.WriteLine(String.Format("AccessLevel: {0}", .AccessLevel))
                        Console.WriteLine(String.Format("IsLoggedOnStation: {0}", .IsLoggedOnStation(StationName)))
                    End With
                Case WorkerLoginResult.Failed
                    Console.WriteLine(String.Format("Worker login failed"))
                Case WorkerLoginResult.NotEnabled
                    Console.WriteLine(String.Format("Worker not enabled on station {0}", StationName))
            End Select
        Catch ex As Exception
            Console.WriteLine(String.Format("Exception: {0}", ex.Message))
        End Try
        Console.ReadLine()

        Try
            Console.WriteLine(String.Format("Logging worker with username {0} and password {1} on station {2}...", Username, Password, StationName))
            a = New Authenticator(AuthenticationMode.UserPassword)
            ret = a.LoginByPassword(Username, Password, StationName)
            Select Case ret
                Case LoginResult.Ok
                    With a.CurrentWorker
                        Console.WriteLine(String.Format("LoginName: {0}", .LoginName))
                        Console.WriteLine(String.Format("Name: {0}", .Name))
                        Console.WriteLine(String.Format("Code: {0}", .Code))
                        Console.WriteLine(String.Format("RoleName: {0}", .RoleName))
                        Console.WriteLine(String.Format("AccessLevel: {0}", .AccessLevel))
                        Console.WriteLine(String.Format("IsLoggedOnStation: {0}", .IsLoggedOnStation(StationName)))
                    End With
                Case WorkerLoginResult.Failed
                    Console.WriteLine(String.Format("Worker login failed"))
                Case WorkerLoginResult.NotEnabled
                    Console.WriteLine(String.Format("Worker not enabled on station {0}", StationName))
            End Select
        Catch ex As Exception
            Console.WriteLine(String.Format("Exception: {0}", ex.Message))
        End Try
        Console.ReadLine()

    End Sub

End Module
