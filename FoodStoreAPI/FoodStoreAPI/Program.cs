using FoodStore.Repositories;
using FoodStoreAPI.Commons.Extensions;
using FoodStoreAPI.DbContext;
using FoodStoreAPI.Entities.Identity;
using FoodStoreAPI.Repositories;
using FoodStoreAPI.Repositories.Interfaces;
using FoodStoreAPI.Services;
using FoodStoreAPI.Services.Interfaces;
using FoodStoreAPI.ViewModel.Mail;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddControllers(options =>
    {
        options.EnableEndpointRouting = false;
    });
    ConfigurationManager configuration = builder.Configuration;
 
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureSwagger();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
    builder.Services.Configure<EmailSettingViewModel>(builder.Configuration.GetSection("EmailSettings"));
    builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    {
        options.TokenLifespan = TimeSpan.FromDays(1);
    });
    builder.Services.AddAuthentication();
    builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        //options.User.RequireUniqueEmail = true;
        //options.SignIn.RequireConfirmedEmail = false;
    }).AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
    //.AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultEmailProvider);
    // Adding Authentication
    builder.Services.DependencyInjection();
    builder.Services.ConfigureJWT(builder.Configuration);
    builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseStaticFiles();
    app.MapControllers();
    app.Run();
}


