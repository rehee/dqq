using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
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
		public static EnumChapter NextChapter(this Character character, IMap? map = null)
		{
			var chapterProfile = DQQPool.TryGet<ChapterProfile, EnumChapter>(character.Chapter);
			var nextChapter = chapterProfile?.CalculateNextChapter(character, map);
			if (nextChapter == null)
			{
				return character.Chapter;
			}
			return nextChapter.Value;
		}
		public static bool IsUnlocked(this EnumChapter chapter, Character? character)
		{
			if (character == null)
			{
				return false;
			}
			var currentChapter = (int)character.Chapter;
			var checkChapter = (int)chapter;
			return currentChapter >= checkChapter;
		}
		public static bool IsUnlocked<T>(this T source, T? compare) where T : struct, Enum
		{
			if (compare == null)
			{
				return false;
			}
			var currentChapter = Convert.ToInt32(compare);
			var checkChapter = Convert.ToInt32(source);
			return currentChapter >= checkChapter;
		}

	}
}
