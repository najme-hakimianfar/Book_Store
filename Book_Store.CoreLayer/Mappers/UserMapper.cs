using Book_Store.CoreLayer.DTOs.Users;
using Book_Store.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Mappers
{
    public class UserMapper
    {
        public static User MapRegisterDtoToUser(UserRegisterDto dto)
        {
            return new User()
            {
                FullName = dto.FullName,
                Email = dto.Email,
                IsDelete = false,
                Role = UserRole.Customer,
                CreationDate = DateTime.Now,
                Address = dto.Address,
                Tel = dto.Tel
            };
        }

        public static User MapCreateDtoToUser(RegisterDto dto)
        {
            return new User()
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Address = dto.Address,
                Tel = dto.Tel,
                IsDelete = false,
                CreationDate = DateTime.Now,
                Password = dto.Password,
                Role = dto.Role
            };
        }

        public static User MapEditDtoToUser(EditUserDto editDto,User user)
        {
            user.FullName = editDto.FullName;
            user.Email = editDto.Email;
            user.Tel = editDto.Tel;
            user.Password = editDto.Password;
            user.Role = editDto.Role;
            return user;
            
        }

        public static UserDto MapToDto(User user)
        {
            return new UserDto()
            {
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address,
                Tel = user.Tel,
                Password = user.Password,
                Role = user.Role,
                UserId=user.Id,
                RegisterDate=user.CreationDate          
            };
        }

        public static UserDto MapUserToUserDto(User user)
        {
            return new UserDto()
            {
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                Address = user.Address,
                Tel = user.Tel,
                RegisterDate = user.CreationDate,
                UserId = user.Id
            };
        }
    }
}
