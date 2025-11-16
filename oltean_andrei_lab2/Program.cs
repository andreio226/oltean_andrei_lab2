using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<LibraryIdentityContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

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