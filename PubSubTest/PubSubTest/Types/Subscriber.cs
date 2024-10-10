namespace PubSubTest.Types;

public class Subscriber
{
    private readonly string _name;

    public Subscriber(string name, Publisher publisher)
    {
        _name = name;
        publisher.OnMessageAction += Receive;
    }
    
    private void Receive(string message)
    {
        Console.WriteLine($"{_name} message received: {message}");
    }
 
}