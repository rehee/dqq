using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Commons.DTOs;
using ReheeCmf.Helpers;
using ReheeCmf.Modules.Controllers;
using ReheeCmf.Services;
using System.ComponentModel.DataAnnotations;

namespace DQQ.Api.Apis
{
  [Route("Auths")]
  [ApiController]
  public class AuthController : ReheeCmfController
  {
    private readonly IUserService us;

    public AuthController(IServiceProvider sp, IUserService us) : base(sp)
    {
      this.us = us;
    }
    [Route("Register")]
    [HttpPost]
    public async Task<string> Register([FromBody] RegisterDTO? dto)
    {
      if (dto == null)
      {
        return "";
      }

      var properties = new Dictionary<string, string?>
      {
        ["Username"] = dto.Email,
        ["Password"] = dto.Password,
      };
      var result = await us.CreateUserAsync(properties);
      return result;

    }
  }

  public class RegisterDTO : IValidatableObject
  {
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? ConfirmPassword { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var result = new HashSet<ValidationResult>();
      if (String.IsNullOrEmpty(Password) || !String.Equals(Password, ConfirmPassword))
      {
        result.Add(ValidationResultHelper.New("Password not Matched", nameof(Password), nameof(ConfirmPassword)));
      }
      return result;
    }
  }
}
