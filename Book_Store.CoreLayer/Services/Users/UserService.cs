using Book_Store.CoreLayer.DTOs.Users;
using Book_Store.CoreLayer.Mappers;
using Book_Store.CoreLayer.Utilities;
using Book_Store.DataLayer.Context;
using Book_Store.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly DataBaseContext _context;
        public UserService(DataBaseContext context)
        {
            _context = context;
        }
        public OperationResult RegisterUser(UserRegisterDto registerDto)
        {
            var isEmailExist = _context.Users.Any(u => u.Email == registerDto.Email);
            if (isEmailExist)
                return OperationResult.Error("ایمیل تکراری است");

            var PasswordHash = registerDto.Password.EncodeToMd5();
            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

            var match = Regex.Match(registerDto.Email, emailRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
                return OperationResult.Error("لطفا یک ایمیل وارد کنید");
            var user = UserMapper.MapRegisterDtoToUser(registerDto);
            user.Password = PasswordHash;
            _context.Users.Add(user);

            _context.SaveChanges();
            return OperationResult.Success();
        }

        public UserDto LoginUser(LoginUserDto loginDto)
        {
            var passwordHashed = loginDto.Password.EncodeToMd5();
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email && u.Password == passwordHashed);

            if (user == null)
                return null;

            var userDto = UserMapper.MapUserToUserDto(user);
            return userDto;
        }

        public UserDto GetUserById(int userId)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return null;

            return UserMapper.MapToDto(user);
        }

        public OperationResult CreateUser(RegisterDto command)
        {
            var isEmailExist = _context.Users.Any(u => u.Email == command.Email);
            if (isEmailExist)
                return OperationResult.Error("ایمیل تکراری است");

            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

            var match = Regex.Match(command.Email, emailRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
                return OperationResult.Error("لطفا یک ایمیل وارد کنید");

            var user = UserMapper.MapCreateDtoToUser(command);
            _context.Users.Add(user);
            _context.SaveChanges();

            return OperationResult.Success();
        }

        public OperationResult EditUser(EditUserDto command)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == command.UserId);
            if (user == null)
                return OperationResult.NotFound();

            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

            var match = Regex.Match(command.Email, emailRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
                return OperationResult.Error("لطفا یک ایمیل وارد کنید");

            UserMapper.MapEditDtoToUser(command, user);

            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == userId);

            if (user == null)
                return OperationResult.NotFound();

            user.IsDelete = true;
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public UserFilterDto GetUsersByFilter(int pageId, int take, string name)
        {
            var users = _context.Users.OrderByDescending(d => d.Id).AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                users = users.Where(r => r.FullName.Contains(name));

            var skip = (pageId - 1) * take;
            var model = new UserFilterDto()
            {
                Users = users.Skip(skip).Take(take).Select(user => UserMapper.MapToDto(user)).ToList()
            };
            model.GeneratePaging(users, take, pageId);
            return model;
        }
    }
}
