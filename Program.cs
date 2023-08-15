// Contiene toda la configuracion del proyecto (incluyendo la forma en la que se va a aejecutar) 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   // Swagger -> Ayuda a generar documentacion para APIs

var app = builder.Build();

// Middlewares actuales del proyectos (Los metodos que inicien con "USE" son middelwares) {

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// }

// Los custom middlewares deben ir entre el middleWare "UseAutorizathion" y El "Mapontrollers"
// app.UseWelcomePage();   // UseWelcomePage() -> Permite añadir una pagina de bienvenida con la informacion general de la API (Desde el navegador)

app.UseTimeMiddleware();    // Utilizamos el customMiddleware

app.MapControllers();

app.Run();
