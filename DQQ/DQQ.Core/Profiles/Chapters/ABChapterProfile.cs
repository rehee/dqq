using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Chapters
{
	public abstract class ChapterProfile : DQQProfile<EnumChapter>
	{
		public abstract EnumChapter? NextChapter { get; }

		public virtual EnumChapter? CalculateNextChapter(Character? character, IMap? map = null)
		{
			if (AvaliableToUnlock(character, map))
			{
				return NextChapter;
			}
			return character?.Chapter;
		}

		public virtual int? UnlockLevel => null;
		public virtual EnumMapNumber? UnlockNumber => null;
		public virtual EnumChapter? UnlockChapter => ProfileNumber;

		public virtual bool AvaliableToUnlock(Character? character, IMap? map = null)
		{
			if (character == null)
			{
				return false;
			}
			var levelUnlock = UnlockLevel == null || character?.Level >= UnlockLevel;
			var chapterUnlock = UnlockChapter == null || UnlockChapter?.IsUnlocked(character)==true;
			var mapClearRequired = UnlockNumber == null || (
				map?.MapNumber== UnlockNumber &&
				map?.MapClear== true
			);
			return levelUnlock && chapterUnlock && mapClearRequired;

		}
	}
}
