using Matter.Core;
using Matter.Core.Fabrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

IFabricStorageProvider storageProvider = new FabricDiskStorage("H:\\fabrics");
IMatterController matterController = new MatterController(storageProvider);

matterController.MatterNodeAddedToFabricEvent += MatterController_MatterNodeAddedToFabricEvent;

void MatterController_MatterNodeAddedToFabricEvent(object sender, Matter.Core.Events.MatterNodeAddedToFabricEventArgs e)
{
    Console.WriteLine($"Node added to fabric!");
}

builder.Services.AddSingleton(matterController);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

matterController.InitAsync().Wait();

app.Run();
