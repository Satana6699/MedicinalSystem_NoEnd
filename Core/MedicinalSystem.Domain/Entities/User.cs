using Microsoft.AspNetCore.Identity;

/*namespace MedicinalSystem.Domain.Entities
{
    public class User
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}*/

namespace MedicinalSystem.Domain.Entities
{
    public class User
    {
        // Дополнительные поля, которые могут понадобиться для пользователя
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime PasswordTime { get; set; }
        public string? Role { get; set; }
    }
}
