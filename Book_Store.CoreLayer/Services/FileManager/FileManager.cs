using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Book_Store.CoreLayer.Services.FileManager
{
    public class FileManager : IFileManager
    {
        public string SaveFileAndReturnName(IFormFile file, string savePath)
        {
            if(file==null)
                throw new Exception("File Is Null");

            var fileName = $"{Guid.NewGuid()}{file.FileName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/","\\"));

            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fullPath = Path.Combine(folderPath , fileName);

            using var stream = new FileStream(fullPath,FileMode.Create);
            file.CopyTo(stream);
            return fileName;        
        }
    }
}
