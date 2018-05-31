using LayIM.AspNetCore.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Application
{
    public interface ILayIMFileUploader
    {
        Task<FileUploadResult> Upload(HttpContext context);
    }
}
