using TasksApi.Endpoints;
using TasksApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddPersistence();

var app = builder.Build();

app.MapTasksEndpoints();

app.Run();
