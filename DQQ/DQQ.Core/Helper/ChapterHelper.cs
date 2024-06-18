using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class ChapterHelper
	{
		public static EnumChapter NextChapter(this EnumChapter chapter)
		{
			var intChapter = (int)chapter + 1;

			EnumChapter chap;
			if (Enum.IsDefined(typeof(EnumChapter), intChapter))
			{
				chap = (EnumChapter)intChapter;
				return chap;
			}
			else
			{
				return chapter;
			}
		}
	}
}
