# FileWatcher
It's a console app. Used to view the movement of files. Notifications can be sent in Windows Notification(by default) and also sent to email. when there is an action on the file
  
# Set route and format file
  Set in App.config file. Change the value for the route and format file you want.
  
    <add key="path_file" value="\\dnth_sv03\Common Folder\IT\Josky"/>
    
    <add key="format_file" value="pdf"/>
    
# Set email notification (Optional)
  Set in App.config file. Change the value for the sender mail and receiver email.

     <!--Change the value for the sender email.->
    <add key="Sender_Default" value="zerq.root@gmail.com"/>
    
    <!--Change the value for the sender email password.->
    <add key="Sender_Default_Password" value="xxxxxx"/>
    
    <!--Change the value for the receiver email.->
    <add key="Receiver_Default" value="josky2130@gmail.com"/>
    
# Set event to listener
  Set in App.config file. Change an event enable/disable for watching action in that file.
  
    <add key="event_on_changed" value="no"/>
    <add key="event_on_created" value="yes"/>
    <add key="event_on_deleted" value="yes"/>
    <add key="event_on_renanmed" value="yes"/>
    <add key="event_on_errored" value="no"/>
