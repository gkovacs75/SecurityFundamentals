using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using SecurityFundamentals.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("GaborsAuthCookie").AddCookie("GaborsAuthCookie", options =>
{
    options.Cookie.Name = "GaborsAuthCookie";
    options.LoginPath = "/Account/Login"; // This is default, but can be changed
    options.AccessDeniedPath = "/Account/AccessDenied"; // This is default, but can be changed
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5); // Expire after 5 min.
}
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBelongToHRDepartment", policy => policy.RequireClaim("Department", "HR"));

    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));

    options.AddPolicy("HRManagerOnly", policy =>
    {
        policy.RequireClaim("Department", "HR");
        policy.RequireClaim("Manager");
        // Add custom requirement with 3 months probation period
        policy.Requirements.Add(new HRManagerProbationRequirement(3));
    });
});


builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// This adds the Authentication middleware
app.UseAuthentication();
// This adds the Authorization middleware
app.UseAuthorization();

app.MapRazorPages();

app.Run();
