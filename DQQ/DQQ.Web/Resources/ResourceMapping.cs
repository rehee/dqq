using DQQ.Enums;
using DQQ.Profiles.Mobs;
using DQQ.Web.Resources.Chapters;
using ReheeCmf.Helpers;

namespace DQQ.Web.Resources
{
	public static class ResourceMapping
	{
		public static Dictionary<EnumMob, Type> MobResourceTypes { get; set; } = new Dictionary<EnumMob, Type>();
		public static Dictionary<EnumChapter, Type> ChapterResourceTypes { get; set; } = new Dictionary<EnumChapter, Type>();
		public static Type? GetChapterResourceTypes(this EnumChapter chapter)
		{
			if (ChapterResourceTypes.TryGetValue(chapter, out var r))
			{
				return r;
			}
			return null;
		}
		public static Dictionary<EnumChapter, Type> ChapterInfoResourceTypes { get; set; } = new Dictionary<EnumChapter, Type>();
		public static Type? GetChapterInfoTypes(this EnumChapter chapter)
		{
			if (ChapterInfoResourceTypes.TryGetValue(chapter, out var r))
			{
				return r;
			}
			return null;
		}

		public static void Init(Type type)
		{

			if (type.IsInheritance(typeof(ResourceMonster)))
			{
				if (!type.IsAbstract)
				{
					var instance = Activator.CreateInstance(type);
					if (instance is ResourceMonster b)
					{
						MobResourceTypes.Add(b.Profile, type);
					}
				}
			}

			if (type.IsInheritance(typeof(ChapterPageBase)))
			{
				if (!type.IsAbstract)
				{
					var instance = Activator.CreateInstance(type);
					if (instance is ChapterPageBase chapter)
					{
						ChapterResourceTypes.Add(chapter.Chapter, type);
					}
				}
			}
			if (type.IsInheritance(typeof(ChapterInfoBase)))
			{
				if (!type.IsAbstract)
				{
					var instance = Activator.CreateInstance(type);
					if (instance is ChapterInfoBase chapter)
					{
						ChapterInfoResourceTypes.Add(chapter.Chapter, type);
					}
				}
			}
		}
	}
}
