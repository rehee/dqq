using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;

namespace DQQ.Profiles.Chapters
{
	[Pooled]
	public class Chapter_Non_Open : ChapterProfile
	{
		public override EnumChapter ProfileNumber => EnumChapter.C_Non_Open;
		public override string? Name => "无尽割草";
		public override string? Discription => "无尽割草";
		public override EnumChapter? NextChapter => null;

		
	}
}
