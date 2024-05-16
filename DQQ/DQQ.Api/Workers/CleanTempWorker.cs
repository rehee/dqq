
using DQQ.Api.Services.Itemservices;

namespace DQQ.Api.Workers
{
  public class CleanTempWorker : BackgroundService
  {
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      do
      {
        if (stoppingToken.IsCancellationRequested)
        {
          break;
        }
        await Task.Delay(1000 * 60);
        foreach (var records in TemporaryService.TemporaryItemPool.Values)
        {
          foreach (var r in records)
          {
            if (r.CreateTime <= DateTime.UtcNow.AddMinutes(-15))
            {
              records.Remove(r);
            }
          }
        }
      } while (true);
      return;
    }
  }
}
