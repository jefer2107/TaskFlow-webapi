using Application.DTOs.Category;
using Application.DTOs.Chore;
using Application.DTOs.User;
using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Application.Validations.Categories;
using Application.Validations.Chores;
using Application.Validations.Users;
using Domain.Interface;
using FluentValidation;
using Infrastructure.Data.Context;
using Infrastructure.Data.Identity;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection InfrastrutureApi(
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        var postgresConnection = configuration.GetConnectionString("DatabasePostgres");

        services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(
            options => options.UseNpgsql(postgresConnection,
                npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            )
        );

        services.AddAutoMapper(typeof(DomainDtoToMappingProfile));

        services.AddIdentity<IdentityUser, IdentityRole>(
            option =>
            {
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
            }
        )
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        services.AddTransient<IValidator<UserInputCreateDTO>, UserCreateValidator>();
        services.AddTransient<IValidator<UserInputUpdateDTO>, UserUpdateValidator>();
        services.AddTransient<IValidator<UserInputChangePasswordDTO>, UserPasswordChangeValidator>();
        services.AddTransient<IValidator<ResetPasswordDTO>, UserResetPasswordValidator>();
        services.AddTransient<IValidator<ForgotPasswordDTO>, UserForgotPasswordValidator>();

        services.AddTransient<IValidator<ChoreInputCreateDTO>, ChoreCreateValidator>();
        services.AddTransient<IValidator<ChoreInputUpdateDTO>, ChoreUpdateValidator>();

        services.AddTransient<IValidator<CategoryInputCreateDTO>, CategoryCreateValidator>();
        services.AddTransient<IValidator<CategoryInputUpdateDTO>, CategoryUpdateValidator>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped<IAuthentication, Authentication>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();

        services.AddScoped<IChoreRepository, ChoreRepository>();
        services.AddScoped<IChoreService, ChoreService>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
