using Microsoft.EntityFrameworkCore;
using oltean_andrei_lab2.Data;
using Microsoft.AspNetCore.Identity;
using oltean_andrei_lab2.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("oltean_andrei_lab2Context") ?? throw new InvalidOperationException("Connection string 'oltean_andrei_lab2Context' not found.");

builder.Services.AddDbContext<oltean_andrei_lab2Context>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Books");
    options.Conventions.AuthorizePage("/Borrowings/Create");
    options.Conventions.AllowAnonymousToPage("/Books/Index");
    options.Conventions.AllowAnonymousToFolder("/Identity/Account");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();