﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinalSystem.Application.Dtos.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; }
    }
}
