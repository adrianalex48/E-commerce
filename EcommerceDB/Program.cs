using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Windbrands.Data.ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexionNeon")));

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromHours(8);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Windbrands.Data.ApplicationDbContext>();
    await context.Database.ExecuteSqlRawAsync("ALTER TABLE clientes ADD COLUMN IF NOT EXISTS passwordhash text;");
    await context.Database.ExecuteSqlRawAsync("ALTER TABLE clientes ADD COLUMN IF NOT EXISTS passwordresettokenhash text;");
    await context.Database.ExecuteSqlRawAsync("ALTER TABLE clientes ADD COLUMN IF NOT EXISTS passwordresetexpiresat timestamp with time zone;");
    await context.Database.ExecuteSqlRawAsync("UPDATE clientes SET passwordhash = '' WHERE passwordhash IS NULL;");
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
