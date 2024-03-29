﻿using RegistrUser.WebApi.Helpers;
using RegistrUser.WebApi.Interfaces.Services;

namespace RegistrUser.WebApi.Services
{
    public class FileService : IFileService 
    {
        private readonly string _basePath = string.Empty;
        private readonly string _imageFolderName = "images";

        public FileService(IWebHostEnvironment webHost)
        {
            _basePath = webHost.ContentRootPath;
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            string fileName = ImageHelper.MakeImageName(file.FileName);
            string partPath = Path.Combine(_imageFolderName, fileName);
            string path = Path.Combine(_basePath, "wwwroot", partPath);

            var stream = File.Create(path);
            await file.CopyToAsync(stream);
            stream.Close();
            return partPath;
        }
    }
}
