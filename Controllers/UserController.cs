using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IConfiguration config) : ControllerBase
{
    // private DataContextDapper _dapper = new DataContextDapper(config); ===
    private readonly DataContextDapper _dapper = new(config);


    [HttpGet("TestConnection")]
    public DateTime TestConnection(){
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }


    [HttpGet("GetUsers")]
    // public IActionResult Test()
    public IEnumerable<User> GetUsers()
    {

        string sql = "SELECT * FROM TutorialAppSchema.Users";

        IEnumerable<User> Users = _dapper.LoadData<User>(sql);

        
        return Users;
    }

        // Se eu não passar aqui o /{userId} e deixar apenas no método ele vira um searchParam
    [HttpGet("GetSingleUser/{userId}")]
    // public IActionResult Test()
    public User GetSingleUser(int userId)
    {
        string sql = @$"SELECT * FROM TutorialAppSchema.Users WHERE UserId = {userId} ";

        User ReturnedUser = _dapper.LoadDataSingle<User>(sql);

        return ReturnedUser;
    }

}
