using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Chapters
{
	public abstract class ChapterProfile : DQQProfile<EnumChapter>
	{
		public abstract EnumChapter? NextChapter { get; }

		public virtual EnumChapter? CalculateNextChapter(Character? character)
		{
			return NextChapter;
		}
	}
}
