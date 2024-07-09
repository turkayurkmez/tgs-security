using System.ComponentModel.DataAnnotations;

namespace AuthNandAuthZ.Models
{
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }

    }
}
