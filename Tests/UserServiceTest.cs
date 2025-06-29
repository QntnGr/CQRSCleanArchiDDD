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
        var hashedPassword = _userService.HashPassword("test");

        Assert.Equal("$2a$11$7gbUe7C2iz/fuowyMbDOquPNbSOf2UuOYJxJYQa7b31AxY8Wyy/wa", hashedPassword);
    }
}