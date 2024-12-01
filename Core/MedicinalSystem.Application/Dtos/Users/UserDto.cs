using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinalSystem.Application.Dtos.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime PasswordTime { get; set; }
        public string? Role { get; set; }
    }
}
