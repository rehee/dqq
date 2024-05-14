using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Commons.DTOs;
using ReheeCmf.Modules.Controllers;
using ReheeCmf.Services;

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
    public async Task<IActionResult> Register([FromBody]Dictionary<string, string?> properties)
    {
      var result = await us.CreateUserAsync(properties);
      return Content("");

    }
  }
}
