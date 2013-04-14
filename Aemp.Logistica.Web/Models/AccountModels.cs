using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Aemp.Logistica.Web.Models
{
  public class UsersContext : DbContext
  {
    public UsersContext()
      : base("Aemp.Security")
    {
    }

    public DbSet<UserProfile> UserProfiles { get; set; }
  }

  [Table("UserProfile")]
  public class UserProfile
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
  }

  public class RegisterExternalLoginModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    public string ExternalLoginData { get; set; }
  }

  public class LocalPasswordModel
  {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña Actual")]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "La contraseña {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Nueva Contraseña")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirme Nueva Contraseña")]
    [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la contraseña de confirmación no coinciden.")]
    public string ConfirmPassword { get; set; }
  }

  public class LoginModel
  {
    [Required]
    [Display(Name = "Nombre Usuario")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Password { get; set; }

    [Display(Name = "Recuerdame?")]
    public bool RememberMe { get; set; }
  }

  public class RegisterModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }

  public class ExternalLogin
  {
    public string Provider { get; set; }
    public string ProviderDisplayName { get; set; }
    public string ProviderUserId { get; set; }
  }
}
