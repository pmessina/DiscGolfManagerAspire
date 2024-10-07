using DiscGolfManagerAspire.DiscsApiService;
using Discs.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.Services.AddDbContext<DiscGolfDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler();
}

app.MapDefaultEndpoints();

app.MapDiscsApi();

app.Run();

