using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Chapters
{
	[Pooled]
	public class Chapter_0_0 : ChapterProfile
	{
		public override EnumChapter ProfileNumber => EnumChapter.None;
		public override string? Name => "初始";
		public override string? Discription => "初始";

		public override EnumChapter? NextChapter => EnumChapter.C_1_1;
		public override EnumChapter? CalculateNextChapter(Character? character, IMap? map = null)
		{
			return NextChapter;
		}
	}
}
