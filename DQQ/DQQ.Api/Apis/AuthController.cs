using DQQ.Commons.DTOs;
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
    [HttpPost("Register")]
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

  
}
