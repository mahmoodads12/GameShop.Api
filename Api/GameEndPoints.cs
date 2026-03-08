using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Api;

public static class GameEndPoints
{
    const string GetGameEndPoint = "GetGame";

    public static void MapGameEndPoints(this WebApplication app)
    {

        var group = app.MapGroup("/games");

        group.MapGet("/", async (GameStoreContext context)
            => await context.Games
            .Include(game => game.Genre)
            .Select(game => new GameSummaryDto(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            ))
            .AsNoTracking()
            .ToListAsync());

        group.MapGet("/{id}", async (int id, GameStoreContext context) =>
        {
            var game = await context.Games.FindAsync(id);


            return game is null ? Results.NotFound() : Results.Ok(new GameDetailsDto(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            ));
        }).WithName(GetGameEndPoint);


        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext context) =>
        {
            GameModel game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            context.Games.Add(game);
            await context.SaveChangesAsync();

            GameDetailsDto gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute(GetGameEndPoint, new { id = gameDto.Id }, gameDto);
        });

        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext context) =>
        {
            var game = await context.Games.FindAsync(id);

            if (game is null)
            {
                return Results.NotFound();
            }

            game.Name = updatedGame.Name;
            game.GenreId = updatedGame.GenreId;
            game.Price = updatedGame.Price;
            game.ReleaseDate = updatedGame.ReleaseDate;

            await context.SaveChangesAsync();

            return Results.Text($"Game with id {id} has been updated");
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext context) =>
        {
            var game = await context.Games.FindAsync(id);

            if (game is null)
            {
                return Results.NotFound();
            }

            context.Games.Remove(game);
            await context.SaveChangesAsync();

            return Results.Text($"Game with id {id} has been deleted");
        });
    }
}
