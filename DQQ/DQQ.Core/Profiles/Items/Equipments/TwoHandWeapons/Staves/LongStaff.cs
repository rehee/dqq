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
    public class LongStaff : AbStave
    {
        public override decimal AttackPerSecond => 1.3m;

        public override decimal Rarity => 1;

        public override EnumItem ProfileNumber => EnumItem.LongStaff;

        public override string? Name => "长杖";

        public override string? Discription => "长杖";
    }
}
