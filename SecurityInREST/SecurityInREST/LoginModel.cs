﻿using System.ComponentModel.DataAnnotations;

namespace SecurityInREST
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set;}
        [Required]
        public string Password { get; set;}
    }
}
