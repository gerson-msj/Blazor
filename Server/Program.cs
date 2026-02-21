using Blazor.Core.DataAccess;
using Blazor.Core.Domain.Config;
using Blazor.Core.IoC;
using Blazor.Server.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .Configure<SqliteConfig>(builder.Configuration.GetSection(nameof(SqliteConfig)));

DependencyInjection.Config(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


// Somente em testes, para garantir a criação do banco.

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var fac = services.GetRequiredService<IDbContextFactory<DataContext>>();
    await using var db = await fac.CreateDbContextAsync();
    await db.Database.EnsureCreatedAsync();
}

app.Run();
