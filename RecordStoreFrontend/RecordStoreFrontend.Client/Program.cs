using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient("RecordStoreAPI", client =>
    client.BaseAddress = new Uri("https://localhost:7186"));

await builder.Build().RunAsync();
