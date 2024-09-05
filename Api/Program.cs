using System.Text.Json;
using API.Extentions;
using API.Helpers;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
// Configure Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); 
});

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Register application services
builder.Services.AddApplicationServices();

// Configure DbContext with SQL Server provider
builder.Services.AddDbContext<IrbidContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IrbidConnection"));
});

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
