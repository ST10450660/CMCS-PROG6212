using CMCS.Web.Data;
using CMCS.Web.Hubs;
using CMCS.Web.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// EF Core (SQLite local file)
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// MVC + FluentValidation
builder.Services.AddControllersWithViews(); // This is a key change
builder.Services.AddFluentValidationAutoValidation() // This is new
    .AddFluentValidationClientsideAdapters(); // This is new
builder.Services.AddTransient<IValidator<CMCS.Web.Models.Claim>, ClaimValidator>();

// SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Apply migrations / create db automatically (dev convenience)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.MapHub<ClaimStatusHub>("/claimStatusHub");

app.Run();