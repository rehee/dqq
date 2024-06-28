using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;

namespace DQQ.Profiles.Chapters
{
	[Pooled]
	public class Chapter_C_1_4 : ChapterProfile
	{
		public override EnumChapter ProfileNumber => EnumChapter.C_1_4;
		public override string? Name => "序章 4";
		public override string? Discription => "练级";

		public override EnumChapter? NextChapter => EnumChapter.C_1_5;
	
	
		public override int? UnlockLevel => 2;
		
	}
}
