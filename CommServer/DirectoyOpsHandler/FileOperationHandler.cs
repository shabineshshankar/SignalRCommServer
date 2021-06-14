using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommServer.DirectoyOpsHandler
{
    /// <summary>
    /// THis class is used to perform file operations 
    /// Create,delete,Appened and rename
    /// </summary>
    public class FileOperationHandler
    {


      

        /// <summary>
        /// Rename or append a text to a filename
        /// </summary>
        /// <param name="NewFileName"></param>
        /// <param name="isAppendFileName"></param>
        /// <returns></returns>
        public bool RenameFile(string NewFileName, bool isAppendFileName = false)
        {
            bool isSuccessful = false;
            try
            {

                string FilePath;
                if (GetAllFiles(out FilePath)==false)
                {
                    return isSuccessful;
                };

                string tempFileName = Path.GetFileNameWithoutExtension(FilePath);
                if (isAppendFileName)
                {
                    NewFileName = tempFileName + NewFileName;
                }

                string fullfileName= Path.GetFileName(FilePath);
                


                string NewFile = FilePath.Replace(fullfileName, NewFileName+".txt");
                // Create a FileInfo  
                System.IO.FileInfo fi = new System.IO.FileInfo(FilePath);
                // Check if file is there  
                if (fi.Exists)
                {
                    // Move file with a new name. Hence renamed.  
                    fi.MoveTo(NewFile);
                    
                }
                isSuccessful = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return isSuccessful;
        }

        /// <summary>
        /// creates a new file and writes the file name and date time as its contents 
        /// If the file exits it will delte and create a new one 
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public void CreateFile(string FileName)
        {
           
            try
            {
                string FilePath = Path.Combine(ConfigurationManager.DirectoryPathToWatch, FileName + ".txt");
                if (File.Exists(FilePath))
                {
                    DeleteFile(FilePath);
                }
                FileInfo fi = new FileInfo(FilePath);
                using (StreamWriter str = fi.CreateText())
                {
                    StringBuilder sb = new StringBuilder(Path.GetFileNameWithoutExtension(FilePath)).Append(DateTime.Now.ToString());
                    str.WriteLine(sb);
                    str.Close();
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }


        /// <summary>
        /// delets the file 
        /// if file parh is not supplied deletes the first file in the directory
        /// </summary>
        /// <param name="Filepath"></param>
        public bool DeleteFile(string Filepath = "")
        {
            bool isSuccess = false;
            try
            {
                
                if (string.IsNullOrEmpty(Filepath))
                {
                    string FirstFileInDir;
                    if (GetAllFiles(out FirstFileInDir) == false)
                    {
                        return isSuccess;
                    };
                    Filepath = FirstFileInDir;
                }

              
                if (File.Exists(Filepath))
                {
                    File.Delete(Filepath);
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return isSuccess;
        }


        public bool GetAllFiles(out string Firstfile)
        {
            bool isSuccess = false;
            try
            {
                string[] Files = Directory.GetFiles(ConfigurationManager.DirectoryPathToWatch);
                //if directory is empty
                if (Files.Length == 0)
                {
                    Firstfile = "";
                    return isSuccess;
                }
                Firstfile = Files[0];
                isSuccess = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return isSuccess;


        }
    }
}
