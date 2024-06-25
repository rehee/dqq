using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;

namespace DQQ.Profiles.Chapters
{
	[Pooled]
	public class Chapter_C_1_6 : ChapterProfile
	{
		public override EnumChapter ProfileNumber => EnumChapter.C_1_6;
		public override string? Name => "序章 6";
		public override string? Discription => "辅助技能. 改变能力";

		public override EnumChapter? NextChapter => null;

		public override EnumChapter? CalculateNextChapter(Character? character, IMap? map = null)
		{
			if (!ProfileNumber.IsUnlocked(character))
			{
				return character?.Chapter;
			}

			return NextChapter;
		}
	}
}
