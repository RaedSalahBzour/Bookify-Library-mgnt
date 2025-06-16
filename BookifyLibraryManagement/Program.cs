using Bookify_Library_mgnt.Middlewares;
using Data.Entities;
using Data.Helpers;
using Microsoft.AspNetCore.Identity;
using Service;
using Service.Persistence.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCustomAuthorizationPolicies();
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
