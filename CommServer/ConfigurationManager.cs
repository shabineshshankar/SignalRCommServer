using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommServer
{
    public class ConfigurationManager
    {



        public static string DirectoryPathToWatch { get; set; }
        public ConfigurationManager(IConfiguration config)
        {
            DirectoryPathToWatch = config["WatcherDirectoryPath"];
            if (Directory.Exists(DirectoryPathToWatch) == false)
            {
                Console.WriteLine("Directory path not specified");
                Console.WriteLine("Application will use default directory");
                DirectoryPathToWatch = CreateCustomDirectory();
            }
            else
            {
                DirectoryPathToWatch = CreateCustomDirectory();
            }
        }


        public string CreateCustomDirectory()
        {
            string loc = System.Reflection.Assembly.GetEntryAssembly().Location;
            string ApllicationPath = Path.Combine(Path.GetDirectoryName(loc), "TestFolder");

            if (!Directory.Exists(ApllicationPath))
            {
                Directory.CreateDirectory(ApllicationPath);
            }
            try
            {

                for (int i = 1; i < 4; i++)
                {
                    StreamWriter sw = new StreamWriter(Path.Combine(ApllicationPath, "Test" + i + ".txt"));
                    sw.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
            return ApllicationPath;
        }
    }
}
