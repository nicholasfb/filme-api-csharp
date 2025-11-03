using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinhaFilmeAPI.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString
    ("FilmeConnection");

builder.Services.AddDbContext<FilmeContext>(opts =>
opts.UseMySql(builder.Configuration
.GetConnectionString("FilmeConnection"),
ServerVersion.AutoDetect(connectionString)));
//After that, use console to run the following commands:
//Add-Migration CriandoTabelaDeFilme
//Update-Database


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmesAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
