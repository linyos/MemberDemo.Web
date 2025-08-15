try
{
    // Log.Logger = new LoggerConfiguration()
    // .MinimumLevel.Debug()
    // .WriteTo.Console()
    // .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    // .CreateLogger();


    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build())
        .CreateLogger();

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();
    // Add services to the container.
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


    builder.Services.AddControllersWithViews();
    builder.Services.AddScoped<IPointService, PointService>();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles(); // 新增：啟用 wwwroot 靜態檔案服務
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

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}