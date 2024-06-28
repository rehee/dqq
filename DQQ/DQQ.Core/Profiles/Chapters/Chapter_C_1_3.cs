using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;

namespace DQQ.Profiles.Chapters
{
	[Pooled]
	public class Chapter_C_1_3 : ChapterProfile
	{
		public override EnumChapter ProfileNumber => EnumChapter.C_1_3;
		public override string? Name => "序章 3";
		public override string? Discription => "改变目标";

		public override EnumChapter? NextChapter => EnumChapter.C_1_4;

		
		public override EnumMapNumber? UnlockNumber => EnumMapNumber.Map_1_2;
		
	}
}
