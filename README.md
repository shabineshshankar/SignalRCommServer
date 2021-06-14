# SignalRCommServer
This is a sample SignalR server which implements a few file operations

# Assumptions
While Developing this project below assumptions are made
<ul>
<li>The software will only deal with .txt files.</li>
<li>The directory added to the file watcher is not empty.</li> 
</ul> 

# Configuration
The directory to be added to the FileWatcher can be configured in the apsettings.json using the key '<b>WatcherDirectoryPath<b>'.
*<b>Note<b>:If this key is left empty  or invalid path is given the application will create a directory and added it to the filewatcher.*
