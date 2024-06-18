using DQQ.Enums;
using DQQ.Profiles.Mobs;
using ReheeCmf.Helpers;

namespace DQQ.Web.Resources
{
	public static class ResourceMapping
	{
		public static Dictionary<EnumMob, Type> MobResourceTypes { get; set; } = new Dictionary<EnumMob, Type>();


		public static void Init(Type type)
		{

			if (type.IsInheritance(typeof(ResourceBase<EnumMob>)))
			{
				if (!type.IsAbstract)
				{
					var instance = Activator.CreateInstance(type);
					if (instance is ResourceBase<EnumMob> b)
					{
						MobResourceTypes.Add(b.Profile, type);
					}
				}
			}
		}
	}
}
