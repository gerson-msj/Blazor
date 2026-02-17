using Blazor.Core.Domain.Config;
using Blazor.Core.Domain.Dto;
using Blazor.Core.Domain.Entity;
using Blazor.Core.IoC;
using Blazor.Core.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<SqliteConfig>(builder.Configuration.GetSection(nameof(SqliteConfig)));

DependencyInjection.Config(builder.Services);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Livros";
    config.Title = "Livros v1";
    config.Version = "v1";
});

var app = builder.Build();

// Swagger
if(app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config=>
    {
        config.DocumentTitle = "LivrosAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

var livros = app.MapGroup("/livros");
livros.MapGet("/", GetAll);

livros.MapGet("/{id}", async (int id, LivroService service) =>
   await service.FindAsync(id) is LivroEntity livro
        ? Results.Ok(new LivroDto(livro))
        : Results.NotFound());

livros.MapPost("/", async (LivroDto dto, LivroService service) =>
{
    LivroEntity entity = dto.ToNewEntity();
    await service.Create(entity);
    return Results.Created($"/livros/{entity.Id}", new LivroDto(entity));
});

livros.MapPut("/{id}", async (int id, LivroDto dto, LivroService service) =>
{
    LivroEntity entity = dto.ToNewEntity();
    entity.Id = id;
    return await service.Update(entity)
        ? Results.NoContent()
        : Results.NotFound();
});

livros.MapDelete("/{id}", async (int id, LivroService service) =>
    await service.Delete(id)
        ? Results.NoContent()
        : Results.NotFound());

// Somente em testes, para garantir a criação do banco.
/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var fac = services.GetRequiredService<IDbContextFactory<DataContext>>();
    await using var db = await fac.CreateDbContextAsync();
    await db.Database.EnsureCreatedAsync();
}
*/

app.Run();

static async Task<IResult> GetAll(LivroService service)
{
    var entities = await service.ToListAsync();
    return TypedResults.Ok(entities.Select(e => new LivroDto(e)));
}
