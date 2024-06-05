using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.Armors.ArmorTypes;
using DQQ.Profiles.Items.Equipments.Armors.BodyArmor;

namespace DQQ.Profiles.Items.Equipments.Armors.Helmet
{
  [Pooled]
  public class LeatherHelmet : AbHelmet<ArmorTypeDefence>
  {
    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.LeatherHelmet;

    public override string? Name => "皮甲头盔";

    public override string? Discription => "皮甲头盔";
  }
}
