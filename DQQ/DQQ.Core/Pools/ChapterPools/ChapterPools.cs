using DQQ.Enums;
using DQQ.Profiles.Chapters;
using DQQ.Profiles.Items;
using DQQ.Profiles.ZProgress;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumChapter, ChapterProfile> ChapterPools { get; set; } = new Dictionary<EnumChapter, ChapterProfile>();
  }
}
