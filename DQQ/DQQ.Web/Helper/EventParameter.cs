namespace DQQ.Helper
{
  public class EventParameter : IDisposable
  {
    public event EventHandler<EventArgs>? Event;

    public void InvokeEvent(object sender, EventArgs arg)
    {
      if (Event != null)
      {
        this.Event(sender, arg);
      }
    }

    bool dispose { get; set; }
    public void Dispose()
    {
      if (dispose)
      {
        return;
      }
      dispose = true;
      Event = null;
    }
  }
}
