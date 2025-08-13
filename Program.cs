using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MemberDemo.Web.Data;
using MemberDemo.Web.Models;
using MemberDemo.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>

{
    options.SignIn.RequireConfirmedAccount = true;
}
)
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPointService, PointService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
   
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();  
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();
// 執行種子資料 - 放在 app.Run() 之前
// 只在開發環境執行種子資料
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        await SeedData.RunAsync(scope.ServiceProvider);
    }
}
app.Run();
