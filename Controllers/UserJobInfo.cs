

using DotnetAPI.Data.Dapper;
using DotnetAPI.Modules.UserJobInfo;
using DotnetAPI.Modules.UserSalary;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoController(IConfiguration config) : ControllerBase
{

    private readonly DataContextDapper _dapper = new(config);
    private readonly string userJobInfoTable = "TutorialAppSchema.UserJobInfo";

    [HttpGet()]
    public UserJobInfo GetUserJobInfo(int userId)
    {

        string sql = $"SELECT * FROM {userJobInfoTable} WHERE userId = {userId}";

        return _dapper.LoadDataSingle<UserJobInfo>(sql);
    }

    [HttpPost()]
    public IActionResult RegisterUserJobInfo(UserJobInfo UserJobInfo)
    {

        string sql = @$"
            INSERT INTO 
            {userJobInfoTable}([UserId], [JobTitle], [Department])
            VALUES({UserJobInfo.UserId},'{UserJobInfo.JobTitle}','{UserJobInfo.Department}')
        ".Trim();

        bool HasFinishedWithSucess = _dapper.ExecuteSql(sql);

        return HasFinishedWithSucess ? Ok() : throw new Exception("Não foi possível Criar registro do trabalho");
    }

    [HttpPut()]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {

        string sql = @$"
            UPDATE {userJobInfoTable}
            SET
                [JobTitle] = '{userJobInfo.JobTitle}',
                [Department] = '{userJobInfo.Department}'
            WHERE userId = {userJobInfo.UserId}
        ".Trim();

        bool HasFinishedWithSucess = _dapper.ExecuteSql(sql);

        return HasFinishedWithSucess ? Ok() : throw new Exception("Não foi possível Atualizar o registro do usuário");
    }

    [HttpDelete()]
    public IActionResult DeleteUserJobInfo(int userId)
    {

        string sql = $"DELETE FROM {userJobInfoTable} WHERE UserId = {userId}";

        bool HasFinishedWithSucess = _dapper.ExecuteSql(sql);

        return HasFinishedWithSucess ? Ok() : throw new Exception("Não foi possível Deletar o registro do usuário");
    }
}