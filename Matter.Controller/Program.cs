using Matter.Core;
using Matter.Core.Fabrics;
using Matter.Controller.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

IFabricStorageProvider storageProvider = new FabricDiskStorage("H:\\fabrics");
IMatterController matterController = new MatterController(storageProvider);

matterController.MatterNodeAddedToFabricEvent += MatterController_MatterNodeAddedToFabricEvent;

void MatterController_MatterNodeAddedToFabricEvent(object sender, Matter.Core.Events.MatterNodeAddedToFabricEventArgs e)
{
    Console.WriteLine($"Node added to fabric!");
}

builder.Services.AddSingleton<IMatterController>(matterController);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

await matterController.InitAsync();

app.Run();
