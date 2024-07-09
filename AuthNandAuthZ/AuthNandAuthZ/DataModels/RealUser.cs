using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthNandAuthZ.DataModels;

public partial class RealUser
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = "EMPTY";

    public string UserName { get; set; } = null!;

    public string Role { get; set; } = null!;

    [NotMapped]
    public string Password { get; set; }
}
