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
	public class Chapter_0_0 : ChapterProfile
	{
		public override EnumChapter ProfileNumber => EnumChapter.None;
		public override string? Name => "初始";
		public override string? Discription => "初始";
	}
}
