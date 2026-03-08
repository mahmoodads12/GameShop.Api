using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Api;

public static class GenreEndPoints
{
    public static void MapGenreEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        group.MapGet("/", async (GameStoreContext context) =>
            await context.Genres
            .Select(genre => new GenreDto(
                genre.Id,
                genre.Name
            ))
            .AsNoTracking()
            .ToListAsync());
    }
}
