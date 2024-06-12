using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes
{
	public abstract class SuffixProfile : AffixeProfile
	{
		public override bool IsPrefix => false;
	}
}
