using DQQ.Components;
using ReheeCmf.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Entities
{
  public interface IDQQEntity : IId<Guid>
  {
    string? Name { get; set; }
  }
  public interface IDQQEntity<T> : IDQQEntity where T : IDQQComponent, new()
  {
    T GenerateComponent();
  }
}
