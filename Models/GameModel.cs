using System;

namespace GameStore.Api.Models;

public class GameModel
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public GenreModel? Genre { get; set; }

    public int GenreId { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
}
