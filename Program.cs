using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FridgeManagement.Data;
using FridgeManagement.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<FridgeManagementUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AuthenticationContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
