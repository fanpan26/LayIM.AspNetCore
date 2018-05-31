using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LayIM.AspNetCore.Core.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace LayIM.AspNetCore.Core.Application
{
    public class DefaultFileUploader : ILayIMFileUploader
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public DefaultFileUploader(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public async Task<FileUploadResult> Upload(HttpContext context)
        {
            var files = context.Request.Form.Files;
            if (files == null || files.Count == 0)
            {
                return new FileUploadResult
                {
                    ErrorMsg = "无效的文件"
                };
            }
            IFormFile file = files[0];
            string filePath = BuildFilePath(file.FileName,out var fileUrl);
            if (filePath == null) {
                return new FileUploadResult
                {
                    ErrorMsg = "不允许上传的文件类型"
                };
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return new FileUploadResult
            {
                FileName = file.FileName,
                FileUrl = fileUrl
            };

        }

        private string BuildFilePath(string fileName,out string fileUrl)
        {
            string ext = Path.GetExtension(fileName);
            if (IsDanger(ext))
            {
                fileUrl = null;
                return null;
            }
            string newFileName = $"{Guid.NewGuid().ToString()}{ext}";
            string path = hostingEnvironment.WebRootPath;
            var dir = path + "\\upload\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            fileUrl = "/upload/" + newFileName;
            return dir + newFileName;
        }

        //做简单限制
        private static readonly string[] DangerFiles = new string[] { "ASP", "JSP", "EXE", "COM", "CMD", "ASPX", "BAT" };
        private bool IsDanger(string ext)
        {
            if (string.IsNullOrEmpty(ext)) { return true; }
            string extUpper = ext.ToUpperInvariant();
            return DangerFiles.Any(x => x.Equals(extUpper));
        }
    }
}
