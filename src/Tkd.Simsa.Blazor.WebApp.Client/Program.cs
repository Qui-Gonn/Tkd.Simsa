using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Tkd.Simsa.Blazor.Ui.Extensions;
using Tkd.Simsa.Blazor.WebApp.Client.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSimsaUiServices();
builder.Services.AddSimsaWasmServices(builder.Configuration, builder.HostEnvironment);

await builder.Build().RunAsync();