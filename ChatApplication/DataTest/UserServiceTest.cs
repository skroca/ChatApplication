using AutoMapper;
using Data;
using Data.Models;
using Data.Repositories;
using Data.Services;
using DataTest.MockData;
using Moq;

namespace DataTest
{
    public class Tests
    {

        private UserService _service;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddUserTest()
        {
            var _userRepository = new Mock<IUserRepository>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            _userRepository.Setup(_ => _.AddUser(It.IsAny<User>())).Returns(UserServiceData.User);
            _service = new UserService(mapper, _userRepository.Object);

            var user = _service.AddUser(UserServiceData.UserDTO());
            Assert.IsNotNull(user);
        }
    }
}