using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }

    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("GameStore");
        builder.Services.AddSqlite<GameStoreContext>(
            connectionString,
            optionsAction: options => options.UseSeeding((context, _) =>
        {
            if (!context.Set<GenreModel>().Any())
            {
                context.Set<GenreModel>().AddRange(
                    new GenreModel { Name = "Action" },
                    new GenreModel { Name = "Adventure" },
                    new GenreModel { Name = "RPG" },
                    new GenreModel { Name = "Strategy" },
                    new GenreModel { Name = "Sports" }
                );
                context.SaveChanges();
            }
        }));
    }
}
