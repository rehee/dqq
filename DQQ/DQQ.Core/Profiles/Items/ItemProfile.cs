using DQQ.Components.Items;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items
{
  public abstract class ItemProfile : DQQProfile<EnumItem>
  {
    public abstract bool IsStack { get; }
    public abstract int DropQuantity { get; }
    public abstract decimal Rarity { get; }
    
    public virtual ItemComponent GenerateComponent(int? itemLevel, int? quantity, EnumRarity rarity = EnumRarity.Normal)
    {
      var result = ItemComponent.New();
      result.Initialize(this, itemLevel, quantity);
      return result;
    }

  }
}
