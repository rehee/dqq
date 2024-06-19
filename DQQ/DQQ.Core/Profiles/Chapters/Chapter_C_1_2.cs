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
	public class Chapter_C_1_2 : ChapterProfile
	{
		public override EnumChapter ProfileNumber => EnumChapter.C_1_2;
		public override string? Name => "序章 2";
		public override string? Discription => "捡垃圾";

		public override EnumChapter? NextChapter => EnumChapter.C_1_3;

		public override EnumChapter? CalculateNextChapter(Character? character, IMap? map = null)
		{
			if (map?.MapClear != true)
			{
				return character?.Chapter;
			}
			if (character?.EquipItems.Where(b => b.Value?.EquipProfile != null).Count() > 1)
			{
				return NextChapter;
			}
			return character?.Chapter;
		}
	}
}
