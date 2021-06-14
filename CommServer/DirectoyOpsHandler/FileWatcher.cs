using CommServer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CommServer
{
    /// <summary>
    /// class which implemets the file watcher logic 
    /// </summary>
    public class FileWatcher
    {
       
        private  IHubContext<CommunicationHub> _hub { get; set; }
        public FileWatcher(IHubContext<CommunicationHub> hub)
        {

            _hub = hub;

        }

     
        /// <summary>
        /// File watcher implementation
        /// </summary>
        public void AddDirectoryWatch(string FilePath)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = FilePath;
            watcher.IncludeSubdirectories = false;//disable watching subdirectoreies
            

            watcher.NotifyFilter = NotifyFilters.Attributes |
                                     NotifyFilters.CreationTime |
                                     NotifyFilters.DirectoryName |
                                     NotifyFilters.FileName |
                                     NotifyFilters.LastAccess |
                                     NotifyFilters.LastWrite |
                                     NotifyFilters.Security |
                                     NotifyFilters.Size;

            // Watch all files.  
            watcher.Filter = "*.*";


            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;
        }


        /// <summary>
        /// method will trigger when files 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                FileOpsModel fm = new FileOpsModel();
                fm.DateTime = DateTime.Now.ToString();
                fm.FileName = e.Name;
                fm.OldFileName = ""; ;
                fm.Event = e.ChangeType.ToString(); ;
                SendData(fm);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

       
        /// <summary>
        /// event triggered on file renamed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            try
            {
                FileOpsModel fm = new FileOpsModel();
                fm.DateTime = DateTime.Now.ToString();
                fm.FileName = e.Name;
                fm.OldFileName = e.OldName;
                fm.Event = "Renamed/Appended";//since rename and appending the file name is the same
                SendData(fm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// sends data to all the connected signalR clients
        /// </summary>
        /// <param name="FileOpsRes">FileOpsModel </param>
        /// <returns>task</returns>
        public async Task SendData(FileOpsModel FileOpsRes)
        {
            await _hub.Clients.All.SendAsync("ReceiveMessage", FileOpsRes);
        }
    }
}
