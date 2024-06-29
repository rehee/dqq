using DQQ.Web.Enums;

namespace DQQ.Web.Pages.Auths
{
	public class OfflineModePage: AuthBasePage
	{
		public override EnumPlayMode PlayMode => EnumPlayMode.Offline;
	}
}