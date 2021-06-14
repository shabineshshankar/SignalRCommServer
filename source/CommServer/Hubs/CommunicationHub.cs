using CommServer.DirectoyOpsHandler;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CommServer
{
    public class CommunicationHub : Hub
    {
        FileOperationHandler Fops = new FileOperationHandler();

        public async Task AppenedFileName(string text)
        {
            try
            {
                if(Fops.RenameFile(text, true)==false)
                {
                    SendMessagetocaller("There are no files in the directory");
                }
            }
            catch (System.Exception ex)
            {

                await Clients.Caller.SendAsync("HandleError", "Operation Failed" + ex.Message);
            }
             
        }
        public async Task RenameFile(string FileName)
        {
            try
            {
                if(Fops.RenameFile(FileName)==false)
                {
                    SendMessagetocaller("There are no files in the directory");
                }
            }
            catch (System.Exception ex)
            {

                await Clients.Caller.SendAsync("HandleError", "Operation Failed" + ex.Message);
            }
            
        }
        public async Task CreateFile(string FileName)
        {
            try
            {
                Fops.CreateFile(FileName);
            }
            catch (System.Exception ex)
            {

                await Clients.Caller.SendAsync("HandleError", "Operation Failed" + ex.Message);
            }
           
        }
        public async Task DeleteFile()
        {
            try
            {
                if (Fops.DeleteFile() == false)
                {
                    SendMessagetocaller("There are no files in the directory");
                }
            }
            catch (System.Exception ex)
            {

                await Clients.Caller.SendAsync("HandleError", "Operation Failed" + ex.Message);
            }
            
           
        }

        public async Task SendMessagetocaller(string Message)
        {
            await Clients.Caller.SendAsync("HandleError", Message);
        }
    }
}
