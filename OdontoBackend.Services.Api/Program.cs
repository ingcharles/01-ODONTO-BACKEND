using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OdontoBackend.Services.Api.Configurations;
using OdontoBackend.Services.Api.Configurations.Token.Helper;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddScoped<TasksDbContext>();

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//builder.Services.AddAuthentication(opt => {
//    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = TokenHelper.Issuer,
        ValidAudience = TokenHelper.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper.Secret)),
        ValidateLifetime = true,
        //ClockSkew = TimeSpan.FromMinutes(0)
        ClockSkew = TimeSpan.Zero
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
            }
            return Task.CompletedTask;
        }
    };
    });

builder.Services.AddAuthorization();
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
//builder.Services.AddDbContext<TasksDbContext>();
//builder.Services.AddScoped<IHttpClientFactory>();
var app = builder.Build();

    //I injected the HttpClient in the ConfigureService method available in Startup in this way:

 //services.AddHttpClient<FixturesViewComponent>(options =>
 //{
 //    options.BaseAddress = new Uri("http://80.350.485.118/api/v2");
 //});

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
