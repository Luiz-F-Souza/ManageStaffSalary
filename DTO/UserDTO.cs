
namespace DotnetAPI
{
    // CREATE USER MODEL
    public partial class CreateUserDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Gender { get; set; }
        public bool Active { get; set; }
    }
}
