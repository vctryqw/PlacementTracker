using Microsoft.EntityFrameworkCore;   // NEW
using PlacementTracker.Data;           // NEW

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// NEW: tell the app to use your AppDbContext with a SQLite file
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=placements.db"));

var app = builder.Build();

// NEW: create the database and table if they don't exist yet
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();