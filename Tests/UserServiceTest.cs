using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Infrastructure.Services;
using Moq;

namespace Tests;

public class UserServiceTest
{
    private readonly IUserService _userService;
    private readonly Mock<IUserRepository> _mockUserRepository;

    public UserServiceTest()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object);
    }

    [Fact]
    public void TestHashPawwsord()
    {
        var inputPwd = "test";
        var hashedPassword = _userService.HashPassword(inputPwd);

        var isOk = _userService.VerifyPassword(inputPwd, hashedPassword);

        Assert.True(isOk);
    }
}