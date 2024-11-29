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
    public class User : IdentityUser
    {
        // Дополнительные поля, которые могут понадобиться для пользователя
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
