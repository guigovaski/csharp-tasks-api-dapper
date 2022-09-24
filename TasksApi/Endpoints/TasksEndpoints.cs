using Dapper.Contrib.Extensions;
using TasksApi.Data;

namespace TasksApi.Endpoints;

public static class TasksEndpoints
{
    public static void MapTasksEndpoints(this WebApplication app)
    {
        app.MapGet("/tasks", async (GetConnection connection) =>
        {
            using var con = await connection();
            var task = con.GetAll<TaskE>().ToList();

            if (task is null) return Results.NotFound();

            return Results.Ok(task);
        });

        app.MapGet("/tasks/{id:int}", async (int id, GetConnection connection) =>
        {
            using var con = await connection();
            return con.Get<TaskE>(id) is TaskE task ? Results.Ok(task) : Results.NotFound();
        });

        app.MapPost("/tasks", async (TaskE task, GetConnection connection) =>
        {
            using var con = await connection();
            con.Insert(task);
            return Results.Created($"/tasks/{task.Id}", task);
        });

        app.MapPut("/tasks", async (TaskE task, GetConnection connection) =>
        {
            var con = await connection();
            con.Update(task);
            return Results.NoContent();
        });

        app.MapDelete("/tasks/{id:int}", async (int id, GetConnection connection) =>
        {
            var con = await connection();
            var task = con.Get<TaskE>(id);
            
            if (task is null)
            {
                return Results.NotFound();
            }

            con.Delete(task);
            return Results.Ok();
        });
    }
}
