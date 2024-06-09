using DQQ.Profiles.Skills;

namespace DQQ.Helper
{
	public static class WebSkillHelper
	{
		public static string SkillGroup(this SkillProfile profile)
		{
			return $" {(int)profile.Category}. {profile.Category.GetEnumString()}";
		}
	}
}
