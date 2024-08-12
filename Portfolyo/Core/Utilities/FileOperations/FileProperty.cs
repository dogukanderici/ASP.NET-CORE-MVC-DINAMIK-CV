using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.FileOperations
{
    public class FileProperty
    {
        public IFormFile UserAddedFile { get; set; }
        public string UserAddedFilePath { get; set; }
    }
}
