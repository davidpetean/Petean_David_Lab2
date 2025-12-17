using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Petean_David_Lab2.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Contextul pentru datele aplicației (Member)
builder.Services.AddDbContext<Petean_David_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Petean_David_Lab2Context")
        ?? throw new InvalidOperationException("Connection string 'Petean_David_Lab2Context' not found.")));

// Contextul pentru datele Identity (Useri, Role-uri, etc.)
builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Petean_David_Lab2Context")
        ?? throw new InvalidOperationException("Connection string 'Petean_David_Lab2Context' not found.")));

// Configurarea serviciului Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

// ... (Restul codului rămâne neschimbat)

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();