using System.ComponentModel.DataAnnotations;

namespace InjectionAttacks.Models
{
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
