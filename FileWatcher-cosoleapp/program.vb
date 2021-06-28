Imports System.IO
Imports System.Threading
Imports System.Windows.Forms

Module program
    Public ContextMenu1 As New ContextMenu
    Public _Notify As New NotifyIcon
    Private mre As New ManualResetEvent(False)
    Private Declare Auto Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
    Private Declare Auto Function GetConsoleWindow Lib "kernel32.dll" () As IntPtr
    Sub Main()
        Dim hWndConsole As IntPtr
        hWndConsole = GetConsoleWindow()
        ShowWindow(hWndConsole, Int16.Parse(GetSetting("SW_HIDE")))
        Using watcher = New FileSystemWatcher(GetSetting("path_file"))
            watcher.NotifyFilter = NotifyFilters.Attributes Or
                                   NotifyFilters.CreationTime Or
                                   NotifyFilters.DirectoryName Or
                                   NotifyFilters.FileName Or
                                   NotifyFilters.LastAccess Or
                                   NotifyFilters.LastWrite Or
                                   NotifyFilters.Security Or
                                   NotifyFilters.Size

            AddHandler watcher.Changed, AddressOf OnChanged
            AddHandler watcher.Created, AddressOf OnCreated
            AddHandler watcher.Deleted, AddressOf OnDeleted
            AddHandler watcher.Renamed, AddressOf OnRenamed
            AddHandler watcher.Error, AddressOf OnError

            watcher.Filter = "*." + GetSetting("format_file")
            watcher.IncludeSubdirectories = True
            watcher.EnableRaisingEvents = True

            Console.WriteLine("Watcher file (" + GetSetting("format_file") + ") at " + GetSetting("path_file") + " is started...")
            Console.ReadLine()
        End Using
    End Sub


    Private Sub OnChanged(sender As Object, e As FileSystemEventArgs)
        If ((GetSetting("event_on_changed") = "no")) Then Exit Sub
        If e.ChangeType <> WatcherChangeTypes.Changed Then
            Return
        End If
        NotifierManager("Changed", $"File: {e.FullPath} have been changed")
        Console.WriteLine($"Changed: {e.FullPath} {Now.ToString("dd-MM-yyyy hh:mm:ss")}")
    End Sub

    Private Sub OnCreated(sender As Object, e As FileSystemEventArgs)
        If ((GetSetting("event_on_created") = "no")) Then Exit Sub
        Dim value As String = $"Created: {e.FullPath}"
        NotifierManager("Created", $"File: {e.FullPath} have been created")
        Console.WriteLine(value + Now.ToString("dd-MM-yyyy hh:mm:ss"))
    End Sub

    Private Sub OnDeleted(sender As Object, e As FileSystemEventArgs)
        If ((GetSetting("event_on_deleted") = "no")) Then Exit Sub
        NotifierManager("Deleted", $"File: {e.FullPath} have been deleted")
        Console.WriteLine($"Deleted: {e.FullPath} {Now.ToString("dd-MM-yyyy hh:mm:ss")}")
    End Sub

    Private Sub OnRenamed(sender As Object, e As RenamedEventArgs)
        If ((GetSetting("event_on_renanmed") = "no")) Then Exit Sub
        NotifierManager("Renamed", $"File: {e.OldFullPath} have been renamed")
        Console.WriteLine($"Renamed: {Now.ToString("dd-MM-yyyy hh:mm:ss")}")
        Console.WriteLine($"    Old: {e.OldFullPath}")
        Console.WriteLine($"    New: {e.FullPath}")
    End Sub

    Private Sub OnError(sender As Object, e As ErrorEventArgs)
        If ((GetSetting("event_on_errored") = "no")) Then Exit Sub
        PrintException(e.GetException())
    End Sub

    Private Sub PrintException(ex As Exception)
        If ex IsNot Nothing Then
            NotifierManager("Errored", ex.Message)
            Console.WriteLine({Now.ToString("dd-MM-yyyy hh:mm:ss")})
            Console.WriteLine($"Message: {ex.Message}")
            Console.WriteLine("Stacktrace:")
            Console.WriteLine(ex.StackTrace)
            Console.WriteLine()
            PrintException(ex.InnerException)
        End If
    End Sub

End Module
