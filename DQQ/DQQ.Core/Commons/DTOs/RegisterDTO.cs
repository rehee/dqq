using ReheeCmf.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons.DTOs
{
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
