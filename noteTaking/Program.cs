using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using noteTaking.Data;
using Microsoft.AspNetCore.Identity;
using noteTaking.Areas.Identity.Data;
using System.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using NETCore.MailKit.Infrastructure.Internal;
using noteTaking.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<noteTakingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("noteTakingContext") ?? throw new InvalidOperationException("Connection string 'noteTakingContext' not found.")));

builder.Services.AddDefaultIdentity<noteTakingUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>();
builder.Services.AddDbContext<IdentityDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDbContextConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Authorization
AddAuthorizationPolicies(builder.Services);
#endregion

builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();
builder.Services.Configure<MailKitOptions>(options =>
{
    options.Server = builder.Configuration["ExternalProviders:MailKit:SMTP:Address"];
    options.Port = Convert.ToInt32(builder.Configuration["ExternalProviders:MailKit:SMTP:Port"]);
    options.Account = builder.Configuration["ExternalProviders:MailKit:SMTP:Account"];
    options.Password = builder.Configuration["ExternalProviders:MailKit:SMTP:Password"];
    options.SenderEmail = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
    options.SenderName = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderName"];

    // Set it to TRUE to enable ssl or tls, FALSE otherwise
    options.Security = false;  // true zet ssl or tls aan
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapRazorPages();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedDataContext.Initialize(services);
}

app.Run();

void AddAuthorizationPolicies(IServiceCollection services)
{
    services.AddAuthorization(options =>
    {
        options.AddPolicy("User Only", policy => policy.RequireClaim("USER_NR"));
    });
}
