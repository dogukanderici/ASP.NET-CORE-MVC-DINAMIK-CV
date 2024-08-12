using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.FileOperations
{
    public interface IFileOperationHelper
    {
        Task<string> SaveFileToFolder(FileProperty fileProperty);
    }
}
