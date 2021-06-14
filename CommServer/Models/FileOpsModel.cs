using System;
using System.Collections.Generic;
using System.Text;

namespace CommServer.Models
{
   
    public class FileOpsModel
    {
        public string FileName { get;set;}
        public string OldFileName { get; set; }
        public string DateTime { get; set; }
        public string Event { get; set; }

    }
}
