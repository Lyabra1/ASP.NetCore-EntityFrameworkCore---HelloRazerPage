using HelloRazorPage.DbModels;
using Microsoft.EntityFrameworkCore;
using HelloRazorPage.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("Default")
    ));
builder.Services.AddScoped<UserService>();
builder.Services.AddAntiforgery(options => {
    options.HeaderName = "RequestVerificationToken";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.Use(
    async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/Users/UsersPage");
            return;
        }
        await next.Invoke();
    }
);

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
