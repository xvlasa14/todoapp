using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using todo_app_UI;
using todo_app_UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped<TodoService>();
builder.Services.AddScoped<TodoStateService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5232/") });

await builder.Build().RunAsync();
