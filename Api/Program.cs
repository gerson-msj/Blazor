using Blazor.Core.DataAccess;
using Blazor.Core.Domain.Config;
using Blazor.Core.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<SqliteConfig>(builder.Configuration.GetSection(nameof(SqliteConfig)));

DependencyInjection.Config(builder.Services);

var app = builder.Build();

/*
Livros
Get
Get/{Id}
Post
Put
Delete
*/

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

app.MapGet("/", () => "Hello World!");

app.Run();
