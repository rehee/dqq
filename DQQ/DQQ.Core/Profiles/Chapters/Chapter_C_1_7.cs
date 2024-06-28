using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;

namespace DQQ.Profiles.Chapters
{
	[Pooled]
	public class Chapter_C_1_7 : ChapterProfile
	{
		public override EnumChapter ProfileNumber => EnumChapter.C_1_7;
		public override string? Name => "序章 7";
		public override string? Discription => "战胜铁三角";

		public override EnumChapter? NextChapter => EnumChapter.C_1_8;

		public override EnumMapNumber? UnlockNumber => EnumMapNumber.Map_1_5;
	}
}
