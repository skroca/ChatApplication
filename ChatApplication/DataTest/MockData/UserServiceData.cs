using Data.DTOModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTest.MockData
{
    internal class UserServiceData
    {
        public static User User()
        {
            return new User()
            {
                IdUser = 1,
                FirstName = "Test",
                LastName = "Test",
                Email = "test01@test.com",
                Password = "Password1"
            };
        }
        public static UserDTO UserDTO()
        {
            return new UserDTO()
            {
                IdUser = 1,
                FirstName = "Test",
                LastName = "Test",
                Email = "test01@test.com",
                Password = "Password1",
            };
        }
    }
}
