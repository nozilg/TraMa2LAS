Imports Cizeta.TraMa2LAS

Module Module1

    Sub Main()
        Dim StationName As String = "ST60.09"
        Dim BadgeCode As String = "gothamcity" ' "myBadgeCode"
        Dim wa As New WorkerAuthenticator()
        Dim ret As WorkerLoginResult = wa.LoginByBadgeCode(BadgeCode, StationName)
        Select Case ret
            Case WorkerLoginResult.Ok
                With wa.CurrentWorker
                    Console.WriteLine(String.Format("LoginName: {0}", .LoginName))
                    Console.WriteLine(String.Format("Name: {0}", .Name))
                    Console.WriteLine(String.Format("Code: {0}", .Code))
                    Console.WriteLine(String.Format("RoleName: {0}", .RoleName))
                    Console.WriteLine(String.Format("AccessLevel: {0}", .AccessLevel))
                End With
            Case WorkerLoginResult.Failed
                Console.WriteLine(String.Format("Worker login failed"))
                If wa.ExceptionMessage <> String.Empty Then
                    Console.WriteLine(String.Format("Exception: {0}", wa.ExceptionMessage))
                End If
            Case WorkerLoginResult.NotEnabled
                Console.WriteLine(String.Format("Worker not enabled on station {0}", StationName))
        End Select
        Console.ReadLine()
    End Sub

End Module
