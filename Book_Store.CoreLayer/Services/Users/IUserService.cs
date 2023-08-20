using Book_Store.CoreLayer.DTOs.Books;
using Book_Store.CoreLayer.DTOs.Users;
using Book_Store.CoreLayer.Utilities;
using Book_Store.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Services.Users
{
    public interface IUserService
    {
        UserDto GetUserById(int userId);
        UserFilterDto GetUsersByFilter(int pageId, int take,string name);
        OperationResult CreateUser(RegisterDto command);
        OperationResult EditUser(EditUserDto command);
        OperationResult RegisterUser(UserRegisterDto registerDto);
        UserDto LoginUser(LoginUserDto loginDto);
        OperationResult DeleteUser(int userId);
    }
}
