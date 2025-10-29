using CodebridgeDogs.Data.Context;
using CodebridgeDogs.Interfaces;
using CodebridgeDogs.Interfaces.IRepositories;
using CodebridgeDogs.Interfaces.IServices;
using CodebridgeDogs.Middleware;
using CodebridgeDogs.Repositories;
using CodebridgeDogs.Services;
using CodebridgeDogs.Validator;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContextEf>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});

builder.Services.AddScoped<IDogRepository, DogRepository>();
builder.Services.AddScoped<IDogService, DogService>();

builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<CreateDogDtoValidator>();
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandler>();
app.UseMiddleware<RateLimitingMiddleware>();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContextEf>();
    dbContext.Database.Migrate();
}

app.Run();
