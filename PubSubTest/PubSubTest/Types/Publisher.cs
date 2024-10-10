namespace PubSubTest.Types;

public class Publisher
{
    public Action<string> OnMessageAction { get; set; }



    public void Publish(string message)
    {
        OnMessageAction?.Invoke(message);
    }
}