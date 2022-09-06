using FinFolio.Web.Infrastructure;
using FinFolio.Web.Models;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPortfolioFunctionAdapter, PortfolioFunctionAdapter>();
builder.Services.AddScoped<IUserService, UserService>();
// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
    options.Events = new OpenIdConnectEvents
    {
        OnTokenValidated = async ctx =>
        {
            if (!ctx.Principal.Claims.Any(cl => cl.Type == ClaimTypes.NameIdentifier))
            {
                throw new UnauthorizedAccessException("The Name Identifier attribute is not present in the token.");
            }
            var roleClaim = ctx.Principal.Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value);
            var objectidentifier = ctx.Principal.Claims
                            .Where(c => c.Type == ClaimTypes.NameIdentifier)
                            .FirstOrDefault().Value;
            IUserService userService = ctx.HttpContext.RequestServices.GetService<IUserService>();
            UserViewModel user = await userService.SaveUserDetails(new UserViewModel { ObjectIdentifier = objectidentifier });
            if (user != null && user.Id > 0)
            {
                ctx.Principal.AddIdentity(new ClaimsIdentity(new List<Claim> { new Claim(CustomClaimAttributes.CLAIM_TYPE, user.Id.ToString(), CustomClaimAttributes.CLAIM_VALUE_TYPE, CustomClaimAttributes.CLAIM_ISSUER) }));
            }

        }
    }
);
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration.GetValue<string>("SyncFusionLicenseKey"));
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});



builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapControllers();

app.Run();
