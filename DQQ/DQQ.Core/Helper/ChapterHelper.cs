using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Chapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class ChapterHelper
	{
		public static EnumChapter NextChapter(this Character character)
		{
			var chapterProfile = DQQPool.TryGet<ChapterProfile, EnumChapter>(character.Chapter);
			var nextChapter = chapterProfile?.CalculateNextChapter(character);
			if (nextChapter == null)
			{
				return character.Chapter;
			}
			return nextChapter.Value;
		}
	}
}
