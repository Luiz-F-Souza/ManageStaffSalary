using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IConfiguration config) : ControllerBase
{
    // private DataContextDapper _dapper = new DataContextDapper(config); ===
    private readonly DataContextDapper _dapper = new(config);


    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
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


    [HttpPut("EditUser")]
    // Precisa ser um model para inferir que desejamos receber um json no body
    public IActionResult EditUser(User user)
    {

        string sql = $@"
            UPDATE TutorialAppSchema.Users
            SET 
                [FirstName] = '{user.FirstName}',
                [LastName]  = '{user.LastName}',
                [Email]     = '{user.Email}',
                [Gender]    = '{user.Gender}',
                [Active]    = {(user.Active ? 1 : 0)}
            WHERE UserId = {user.UserId}
        ".Trim();
        bool HasFinishedWithSuccess = _dapper.ExecuteSql(sql);

        return HasFinishedWithSuccess ? Ok() : throw new Exception("Não foi possível editar o usuário");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(CreateUserDTO user)
    {
        string sql = $@"
            INSERT INTO TutorialAppSchema.Users(
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active]
            ) VALUES (
               '{user.FirstName}',
               '{user.LastName}',
               '{user.Email}',
               '{user.Gender}',
               {(user.Active ? 1 : 0)}
            )
        ".Trim();

        bool HasFinishedWithSucess = _dapper.ExecuteSql(sql);

        return HasFinishedWithSucess ? Ok() : throw new Exception("Não foi possível criar o usuário");
    }

}
