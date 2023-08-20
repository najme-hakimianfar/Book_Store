using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Services.FileManager
{
    public interface IFileManager
    {
        string SaveFileAndReturnName(IFormFile file,string savePath);
    }
}
