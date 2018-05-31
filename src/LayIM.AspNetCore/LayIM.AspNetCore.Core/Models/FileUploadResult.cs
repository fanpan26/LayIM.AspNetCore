using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models
{
    public class FileUploadResult
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string ErrorMsg { get; set; }
    }
}
