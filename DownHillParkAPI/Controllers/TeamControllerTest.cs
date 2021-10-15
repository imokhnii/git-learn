using DownHillParkAPI.Models;
using DownHillParkAPI.Services;
using Xunit;
using System.Threading.Tasks;

public class TeamControllerTest
{
    public TeamControllerTest(ITeamService teamService)
    {
        _teamService = teamService;
    }
    private readonly ITeamService _teamService;
    
    [Fact]
    public async Task TestGetByIdAsync()
    {
        int expectedId = 2;
        Team result = await _teamService.FindByIdAsync(2);
        Assert.Equal(expectedId, result.Id);
    }
}