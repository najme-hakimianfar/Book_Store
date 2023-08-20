using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Utilities
{
    public class Directories
    {
        //مسیر ساخت پوشه عکس
        public const string BookImage = "wwwroot/images/books";

        //مسیر دریافت پوشه عکس
        public static string GetBookImage(string imageName) => $"{BookImage.Replace("wwwroot", "")}/{imageName}";
    }

}
