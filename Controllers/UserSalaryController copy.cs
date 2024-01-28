

using DotnetAPI.Data.Dapper;
using DotnetAPI.Modules.UserSalary;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSalaryController(IConfiguration config) : ControllerBase
{

    private readonly DataContextDapper _dapper = new(config);
    private readonly string userSalaryTable = "TutorialAppSchema.UserSalary";

    [HttpGet()]
    public UserSalary GetUserSalary(int userId)
    {

        string sql = $"SELECT * FROM {userSalaryTable} WHERE userId = {userId}";

        return _dapper.LoadDataSingle<UserSalary>(sql);
    }

    [HttpPost()]
    public IActionResult RegisterUserSalary(UserSalary userSalary)
    {

        string sql = @$"
            INSERT INTO 
            {userSalaryTable}([UserId], [Salary])
            VALUES({userSalary.UserId}, {userSalary.Salary})
        ".Trim();

        bool HasFinishedWithSucess = _dapper.ExecuteSql(sql);

        return HasFinishedWithSucess ? Ok() : throw new Exception("Não foi possível Criar registro de salário");
    }

    [HttpPut()]
    public IActionResult EditUserSalary(int userId, decimal newSalary)
    {

        string sql = @$"
            UPDATE {userSalaryTable}
            SET
                [Salary] = {newSalary}
            WHERE userId = {userId}
        ".Trim();

        bool HasFinishedWithSucess = _dapper.ExecuteSql(sql);

        return HasFinishedWithSucess ? Ok() : throw new Exception("Não foi possível Atualizar o registro de salário");
    }

    [HttpDelete()]
    public IActionResult DeleteUserSalary(int userId)
    {

        string sql = $"DELETE FROM {userSalaryTable} WHERE UserId = {userId}";

        bool HasFinishedWithSucess = _dapper.ExecuteSql(sql);

        return HasFinishedWithSucess ? Ok() : throw new Exception("Não foi possível Deletar o registro de salário");
    }
}