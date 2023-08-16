public class HelloWorldService : IHelloWorldService // Dependencia
{
    public string GetHelloWorld()
    {
        return "Hello World!";
    }
}

public interface IHelloWorldService
{
    string GetHelloWorld();
}