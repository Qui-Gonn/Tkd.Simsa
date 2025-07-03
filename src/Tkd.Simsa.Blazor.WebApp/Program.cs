using Tkd.Simsa.Blazor.Ui.Extensions;
using Tkd.Simsa.Blazor.WebApp.Components;
using Tkd.Simsa.Blazor.WebApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents()
       .AddInteractiveWebAssemblyComponents();

builder.AddSimsaWebAppServices();
builder.Services.AddSimsaUiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(Tkd.Simsa.Blazor.Ui._Imports).Assembly);

app.MapSimsaApiEndpoints();

await app.MigrateDatabaseAsync();

await app.RunAsync();