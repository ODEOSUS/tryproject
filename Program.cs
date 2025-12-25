using Microsoft.EntityFrameworkCore;
using odshop.Data;
using odshop.Models;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    )
);

// MVC
builder.Services.AddControllersWithViews();

// ✅ SESSION BURADA
builder.Services.AddSession();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// ✅ SESSION MIDDLEWARE
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Users.Any(u => u.Username == "admin"))
    {
        context.Users.Add(new User
        {
            Username = "admin",
            Password = "1234",
            IsAdmin = true
        });

        context.SaveChanges();
    }
}

app.Run();

