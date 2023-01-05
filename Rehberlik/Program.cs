using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
// Buras� Sisteme Authentike ��leminin yap�ld�g� yer
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

// Buras� Login i�in Yani Cookie kaydetme
// Yani Eger Kullan�c� giri� yapmad�ysa Giri� Sayfas�na Y�nlendiriyor
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(k =>
    {
        k.LoginPath = "/Organization/OrganiztionLogin";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(locOptions.Value);

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization();
// Giri� Yapabilmek ��in
app.UseAuthentication();

// Sessionu Kullan
//app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}");

app.Run();
