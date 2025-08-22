using BlogTalks.Application.Abstractions;
using BlogTalks.Domain.Repositories;
using BlogTalks.Infrastructure.Authentication;
using BlogTalks.Infrastructure.Messaging;
using BlogTalks.Infrastructure.Repositories;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.FeatureManagement;

namespace BlogTalks.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddDatabase(services, configuration);

        AddAuthentication(services, configuration);

        AddServices(services, configuration);

        return services;
    }

    private static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeyedTransient<IEmailService, EmailHttpService>("EmailHttpService");
        services.AddKeyedTransient<IEmailService, EmailBusEService>("EmailBusEService");

        services.AddFeatureManagement(configuration.GetSection("FeatureManagement"));
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!)),
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });
        services.AddHttpContextAccessor();
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddTransient<IAuthService, AuthService>();
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default)));

        //services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContext>();

        services.AddTransient<IBlogPostRepository, BlogPostRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}
