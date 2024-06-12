using DQQ.Entities;

namespace DQQ.Commons.DTOs
{
	public class BuildDTO
	{
		public static BuildDTO New(ActorBuild bd)
		{
			return new BuildDTO
			{
				ActorId = bd.ActorId,
				BuildId = bd.Id,
				BuildName = bd.Name,
				BuildDescription = bd.BuildDescribe
			};
		}
		public Guid? ActorId { get; set; }
		public Guid? BuildId { get; set; }
		public string? BuildName { get; set; }
		public string? BuildDescription { get; set; }
	}
}
