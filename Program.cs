using WebApplication3.Models;

var builder = WebApplication.CreateBuilder(args);


// Registrar GoogleDriveFilesRepository como servicio
builder.Services.AddScoped<GoogleDriveFilesRepository>();


// Add services to the container.
builder.Services.AddControllersWithViews();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=GetGoogleDriveFiles}/{id?}");

app.Run();
