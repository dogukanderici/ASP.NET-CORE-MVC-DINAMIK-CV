using Core.Utilities.Result;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.FileOperations
{
    public class FileOperationHelper : IFileOperationHelper
    {
        public async Task<string> SaveFileToFolder(FileProperty fileProperty)
        {
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(fileProperty.UserAddedFile.FileName);
            var userFileName = Guid.NewGuid() + extension;
            var saveLocation = resource + fileProperty.UserAddedFilePath + userFileName;
            var stream = new FileStream(saveLocation, FileMode.Create);

            await fileProperty.UserAddedFile.CopyToAsync(stream);

            return userFileName;
        }
    }
}
