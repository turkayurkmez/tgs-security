using System.ComponentModel.DataAnnotations;

namespace InjectionAttacks.Models
{
    public class UserRegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.MultilineText)]
        public string? UserInfo { get; set; }

    }
}
