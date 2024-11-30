using System.ComponentModel.DataAnnotations;

namespace MedicinalSystem.Application.Dtos.Users
{
    public class UserForCreationDto
    {
        public string UserName { get; set; }
        public string? FullName { get; set; }
        public string Password { get; set; }
        public DateTime PasswordTime { get; set; }
        public string Role {  get; set; }
    }
}
