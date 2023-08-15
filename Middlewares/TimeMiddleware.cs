public class TimeMiddleware // Clase que implementa el middleware en ASP.NET
{
    readonly RequestDelegate next;  // RequestDelegate -> Representa el siguiente middleware que se ejecutará

    public TimeMiddleware(RequestDelegate nextRequest)  // El siguiente Request es tomado automaticamente al momento de ejecutar el programa
    {
        // En este caso, nuestro siguiente request es la ejecucion de los controladores (la respuesta de los mismos)
        next = nextRequest;
    }

    public async Task Invoke(HttpContext context)   // Todos los middleware tienen un metodo Invoke, que es el lugar en el que se ejecuta el middleware
    {
        // HttpContext -> Proporciona acceso a la informacion y detalles de una solicitud HTTP (incluyendo la respuesta de la solicitud)

        // await next(context); Podemos modificar el orden de ejcucion del siguiente middleware, en este caso se invocaria primero la respuesta del controlador y luego se ejecutaria la accion del middleware

        if (context.Request.Query.Any(e => e.Key == "time")) await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
        // Request -> Proporciona acceso a la informacion de la solicitud HTTP
        // Query -> Coleccion que contiene los parametros de la consulta en la URL de la solicitud. Dichos parametros se especifican de la siguiente manera: urlCompleta?parametroKey=parametroValue&parametroKey2=parametroValue2...
        // e.Key -> Dentro de Request.Query.Any, la clave de un elemento representa el nombre del parametro (y el valor del parametro es StringValues, ya que un parametro de consulta puede tener multiples valoers)

        // context.Response.WriteAsync(DateTime.Now.ToShortTimeString() -> Escribe la hora actual en la respuesta de la solicitud

        await next(context);
        /*
            Si solo existe un cunstom middleware configurado, nuestro siguiente contexto serán las respuestas de los controladores ejecutados en la solicitud HTTP. En este caso, solo tenemos este middleware configurado, por lo que lo siguiente que se ejecutará (en este caso) despues del custom middleware será la respuesta del controlador que se esté ejecutando (siempre y cuando en la URL esté el parametro de consulta "time", si el parametro no está, la respues de la solicitud será normal. Este custom middlewar no se ejecutará)
        */
    }
}

public static class TimeMiddlewareExtension // Clase de extension
{
    public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder builder)   // IApplicationBuilder -> Permite configurar el middleware en la aplicacion
    {
        // this -> Metodos de extensión. En este caso, especifica que este metodo puede ser invocado en cualquier instancia de IApplicationBuilder.
        return builder.UseMiddleware<TimeMiddleware>(); // Agrega el middleware a la tuberia de procesamiento
        // Si el constructor del middleware necesita parametros, se deben pasar en los parentesis que están despues de que se cierran los simbolos <> (Obviamente, estos parametros deben ser recibidos en el constructor de este metodo)
    }
}