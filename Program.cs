using GameStore.Api.Api;
using GameStore.Api.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

builder.AddGameStoreDb();

var app = builder.Build();

app.MapGameEndPoints();
app.MapGenreEndPoints();

app.MigrateDb();

app.Run();
