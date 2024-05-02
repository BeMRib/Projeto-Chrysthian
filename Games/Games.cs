using NSwag.AspNetCore;
using Games.Models;
using Games.Data;

class Helloweb
{
     static void Main(string[] args)
    {

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddOpenApiDocument(config =>
        {
            config.DocumentName = "Games";
            config.Title = "Games v1";
            config.Version = "v1";
        });
        
        builder.Services.AddDbContext<AppDbContext>();
        WebApplication app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi(config =>
            {
                config.DocumentTitle = "Games API";
                config.Path = "/swagger";
                config.DocumentPath = "/swagger/{documentName}/swagger.json";
                config.DocExpansion = "list";
            });
        }

        //Métodos

        app.MapGet("/api/games", (AppDbContext context) =>
            {
                var games = context.Games;

                return games != null ? Results.Ok(games) : Results.NotFound();
            }
        ).Produces<Game>();


        app.MapPost("/api/games", (AppDbContext context, string name, string plataforma, string genero) => 
            {
                var novoGame =  new Game(Guid.NewGuid(), name, plataforma, genero);

                context.Games.Add(novoGame);
                context.SaveChanges();

                return Results.Ok();
            }
        ).Produces<Game>();

        app.MapPut("/api/games", (AppDbContext context, Game gamebuscar) =>
            {
                var game = context.Games.Find(gamebuscar.Id);

                if (game == null) return Results.NotFound();

                var entry = context.Entry(game).CurrentValues;

                entry.SetValues(gamebuscar);
                context.SaveChanges();

                return Results.Ok(game);

            }
        ).Produces<Game>();

        app.MapDelete("/api/games", (AppDbContext context, Guid Id) =>
            {
                var game = context.Games.Find(Id);

                if (game == null) return Results.NotFound();

                context.Games.Remove(game);
                context.SaveChanges();

                return Results.Ok(game);
            }
        ).Produces<Game>();

        app.MapGet("/api/games/Id", (AppDbContext context, Guid id) =>
            {
                foreach (var game in context.Games)
                {
                    if (game.Id == id)
                    {
                         return Results.Ok(game);
                    }
                }
               return Results.NotFound();
            }
        ).Produces<Game>();

        app.MapPatch("/api/games", (AppDbContext context, Guid id, string genero) =>
            {
                var game = context.Games.FirstOrDefault(game => game.Id == id);
            if (game == null)
            {
                return Results.NotFound();
            }

            game.genero = genero;

            context.SaveChanges();

            return Results.Ok(game);

            }
        ).Produces<Game>();

        app.Run();
    }
}
