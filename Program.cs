// Contiene toda la configuracion del proyecto (incluyendo la forma en la que se va a aejecutar) 
using webapi;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   // Swagger -> Ayuda a generar documentacion para APIs

builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("localConnection"));

// La inyeccion de dependencias debe hacerse antes de que se compile la aplicacion, la inyeccion de dependencias es una forma para agregar funcionalidades a una aplicacion (especificamente, a los controladores), facilitando la mantencion del codigo ya que separa las responsabilidades de la aplicacion
builder.Services.AddScoped<IHelloWorldService, HelloWorldService>();
// AddScoped<interface, class>() -> Crea una instancia de la dependencia que se está inyectando. Cada vez que se inyecte la interfaz, se crea un nuevo objeto del segundo parametro internamente. La interfaz es lo que reciben todos los controladores que usen esta dependencia, y el tipo del objeto que se va a crear es del tipo del segundo parametro (la clase). Crea uno nuevo por cada solicitud pero es el mismo durante la solicitud.
// AddSigleton() -> Cre una unica instancia de la dependencia a nivel de toda la API. Hace que se creen en memoria y que permanezca ahí (independientemente de la cantidad de request que se hagan). La misma instancia es inyectada y utilizada por todas las clases que lo utilicen
// AddTransient<>() -> Crea una instancia de la clase cada vez que se utiliza la dependencia. Es decir, es un nuevo objeto por cada request ya que la clase que implementa la dependencia creara su propia instancia (la cual será nueva por cada reuqest que se haga)

// builder.Services.AddScoped<IHelloWorldService>(e => new HelloWorldService());   // Inyeccion similar a la de la linea 13, se utiliza cuando el constructor de la clase requiere de algun parametro

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ITareaService, TareaService>();


var app = builder.Build();

// Middlewares actuales del proyectos (Los metodos que inicien con "USE" son middelwares) {

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())    // Solo se configura la documentacion swagger si el ambiente del proyecto es de desarrollo (ya que, la documentacion de la API no debe ser publica en los ambientes de produccion para evitar hackeos)
{
    app.UseSwagger();   // URL para acceder a la documentacion de swagger -> servidor/swagger
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// }

// Los custom middlewares deben ir entre el middleWare "UseAutorizathion" y El "Mapontrollers"
// app.UseWelcomePage();   // UseWelcomePage() -> Permite añadir una pagina de bienvenida con la informacion general de la API (Desde el navegador)

// app.UseTimeMiddleware();    // Utilizamos el customMiddleware

app.MapControllers();

app.Run();
