namespace DotnetAPI.Modules.UserJobInfo
{
    public partial class UserJobInfo
    {
        public int UserId { get; set; }
        public required string JobTitle { get; set; }
        public required string Department { get; set; }

    }
}