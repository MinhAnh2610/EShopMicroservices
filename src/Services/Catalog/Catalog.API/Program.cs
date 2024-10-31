using BuildingBlocks.Behaviours;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var assemply = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssembly(assemply);
  config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddCarter();

builder.Services.AddValidatorsFromAssembly(assemply);

builder.Services.AddMarten(options =>
{
  options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(exceptionHandlerApp =>
{
  exceptionHandlerApp.Run(async context =>
  {
    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
    if (exception == null) return;

    var problemDetails = new ProblemDetails
    {
      Title = exception.Message,
      Status = StatusCodes.Status500InternalServerError,
      Detail = exception.StackTrace
    };

    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogError(exception, exception.Message);

    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    context.Response.ContentType = "application/problem+json";

    await context.Response.WriteAsJsonAsync(problemDetails);
  });
});

app.Run();
