using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("GaborsAuthCookie").AddCookie("GaborsAuthCookie", options =>
{
    options.Cookie.Name = "GaborsAuthCookie";
    options.LoginPath = "/Account/Login"; // This is default, but can be changed
    options.AccessDeniedPath = "/Account/AccessDenied"; // This is default, but can be changed
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
    });
});

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
