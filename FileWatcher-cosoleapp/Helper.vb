Imports System.Configuration
Imports System.Drawing
Imports System.Net.Mail
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Module Helper
    <Extension()> Friend Function GetSetting(ByVal Key As String) As String
        Dim Result As String = Nothing
        Dim _appSettings = ConfigurationSettings.AppSettings
        Try
            If _appSettings.Count = 0 Then
            Else
                Result = _appSettings(Key)
            End If
        Catch Ex As ConfigurationErrorsException
        End Try
        Return Result
    End Function
    <Extension()> Friend Sub NotifierManager(title As String, msg As String)
        PutNotifyIcon(title, msg)
        SendMail(title, msg)
    End Sub
    <Extension()> Friend Sub PutNotifyIcon(title As String, msg As String)
        Try
            _Notify.Visible = True
            _Notify.Icon = SystemIcons.Application
            _Notify.BalloonTipIcon = ToolTipIcon.Info
            _Notify.BalloonTipTitle = "Watcher alert " + title
            _Notify.BalloonTipText = msg
            _Notify.ShowBalloonTip(50000)
        Catch ex As Exception

        End Try
    End Sub
    <Extension()> Friend Sub SendMail(ByVal _subject As String, ByVal _msgBody As String)
        Try
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New _
        Net.NetworkCredential(GetSetting("Sender_Default"), GetSetting("Sender_Default_Password"))
            SmtpServer.Port = 587
            SmtpServer.Host = "smtp.gmail.com"
            SmtpServer.EnableSsl = True
            'SmtpServer.UseDefaultCredentials = False 
            mail.From = New MailAddress(GetSetting("Sender_Default"))
            mail.To.Add(GetSetting("Receiver_Default"))
            mail.Subject = "Watcher alert " + _subject
            mail.Body = _msgBody
            SmtpServer.Send(mail)
        Catch ex As Exception
        End Try
    End Sub
    <Extension()> Friend Function EmailValid(ByVal _MailStr As String) As Boolean
        Return Regex.IsMatch(_MailStr, "(?<=[_a-z0-9-]+(.[a-z0-9-]+)@[mM]axxis.co.th\z)")
    End Function
End Module
