using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Chapters
{
	[Pooled]
	public class Chapter_C_1_1 : ChapterProfile
	{
		public override EnumChapter ProfileNumber => EnumChapter.C_1_1;
		public override string? Name => "序章 1";
		public override string? Discription => "序章 1";

		public override EnumChapter? NextChapter => null;
	}
}
