using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;

namespace DQQ.Profiles.Chapters
{
	[Pooled]
	public class Chapter_C_1_8 : ChapterProfile
	{
		public override EnumChapter ProfileNumber => EnumChapter.C_1_8;
		public override string? Name => "序章 8";
		public override string? Discription => "通往外界的道路";

		public override EnumChapter? NextChapter => EnumChapter.C_Non_Open;

		public override EnumMapNumber? UnlockNumber => EnumMapNumber.Map_1_6;
	}
}
