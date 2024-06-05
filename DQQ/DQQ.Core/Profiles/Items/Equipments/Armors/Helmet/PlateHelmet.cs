using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.Armors.ArmorTypes;
using DQQ.Profiles.Items.Equipments.Armors.BodyArmor;

namespace DQQ.Profiles.Items.Equipments.Armors.Helmet
{
  [Pooled]
  public class PlateHelmet : AbHelmet<ArmorTypeArmor>
  {
    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.PlateHelmet;

    public override string? Name => "板甲头盔";

    public override string? Discription => "板甲头盔";
  }
}
