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
    public class EclipseStaff : AbStave
    {
        public override decimal AttackPerSecond => 1.2m;

        public override decimal Rarity => 1;

        public override EnumItem ProfileNumber => EnumItem.EclipseStaff;

        public override string? Name => "月牙铲";

        public override string? Discription => "月牙铲";
    }
}
