using Microsoft.OpenApi.Models;
using OdontoBackend.Services.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "01-ODONTO-BACKEND API",
        Version = "v1",
        Description = "Backend para una cl�nica dental con ASP.NET Core Web API",
        Contact = new OpenApiContact
        {
            Name = "Carlos Anchundia",
            Email = "e.du.ingcharles@hotmail.com",
            //Url = null,
            //Url = new Uri("https://example.com/contact"),
        },
    });

});
//Implmentada para a�adir las dependencias de los Servicios
builder.Services.AddDependencyInjectionConfiguration();

//La carga de datos desde appsettings.jsonla nueva clase se realizar� mediante el Configurem�todo en Program.csuna vez que agregue esta l�nea de c�digo:
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});
app.UseAuthorization();
//Implementada para escribir los log en archivo
app.UseMiddlewareLogs();
app.MapControllers();

app.Run();
