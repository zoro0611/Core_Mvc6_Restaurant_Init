using Microsoft.EntityFrameworkCore;
using Restaurant_Init.Models.DBModels;
using Restaurant_Init.Models.EmailModels;
using Restaurant_Init.Repos;
using Restaurant_Init.Services;
using Restaurant_Init.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration["ConnectionStrings:Restaurant"] = builder.Configuration["ConnectionStrings:Restaurant"];
builder.Services.AddDbContext<RestaurantContext>(
    options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Restaurant"))
);
builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
builder.Services.AddScoped<CommonService>();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("MyCookieAuthenticationScheme")
    .AddCookie("MyCookieAuthenticationScheme", options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(60);
        options.LoginPath = "/Account/Login";
    });

//EMail
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton<IMailService, MailService>();

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
app.UseAuthentication(); //·s¼W
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
