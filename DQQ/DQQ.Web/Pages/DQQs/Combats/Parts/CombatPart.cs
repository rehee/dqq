using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Combats.Parts
{
	public class CombatPart : ComponentBase, IAsyncDisposable
	{
		public bool IsDisposed { get; private set; }
		public async ValueTask DisposeAsync()
		{
			await Task.CompletedTask;
			if (IsDisposed)
			{
				return;
			}
			IsDisposed = true;
			OnDispose();
		}
		protected virtual void OnDispose()
		{

		}

	}
}
