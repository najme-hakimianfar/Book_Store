using Book_Store.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.DTOs.Users
{
    public class UserFilterDto:BasePagination
    {
        public string Name { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
