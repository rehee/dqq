using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.TwoHandWeapons.Bows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons.Staves
{
    [Pooled]
    public class RoyalStaff : AbStave
    {
        public override decimal AttackPerSecond => 1.15m;

        public override decimal Rarity => 1;

        public override EnumItem ProfileNumber => EnumItem.RoyalStaff;

        public override string? Name => "皇家长杖";

        public override string? Discription => "皇家长杖";
    }
}
