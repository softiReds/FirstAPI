public class HelloWorldService : IHelloWorldService // Servicio, hereda de una interfaz e implementa los metodos de dicha interfaz la cual es utilizada para la inyeccion de dependencias en el proyecto
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