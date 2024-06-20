using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;

namespace DQQ.Profiles.Chapters
{
	[Pooled]
	public class Chapter_C_1_5 : ChapterProfile
	{
		public override EnumChapter ProfileNumber => EnumChapter.C_1_5;
		public override string? Name => "序章 5";
		public override string? Discription => "治疗 以及更多波次";

		public override EnumChapter? NextChapter => null;

		public override EnumChapter? CalculateNextChapter(Character? character, IMap? map = null)
		{
			

			return NextChapter;
		}
	}
}
