using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;
using POC1.Application.Common;
using POC1.Application.Extension;
using POC1.Application.Queries.ApiBlobs;
using POC1.Application.Queries.ApiLogs;
using POC1.Infrastructure.Extension;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<GetApiLogsQueryValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddMediatR(typeof(GetApiBlobQuery));

builder.Services.AddAutoMapper(typeof(ApiLogProfile));

builder.Services.AddInfrastructureDependency();
builder.Services.AddApplicationDependency();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Logs API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Logs Api");
});

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseEndpoints(endpoints => { endpoints.MapControllers(); });



app.Run();
