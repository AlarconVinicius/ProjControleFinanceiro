﻿using System.Runtime.CompilerServices;

using Microsoft.EntityFrameworkCore;

using ProjControleFinanceiro.Data.Configuracao;

namespace ProjControleFinanceiro.Api.IoC;

public static class ApiConfig
{

    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        return services;
    }

    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwaggerConfiguration();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });


        return app;
    }
    public static void MigrationInitialisation(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContextBase = scope.ServiceProvider.GetRequiredService<ContextoBase>();
        var dbContextIdentity = scope.ServiceProvider.GetRequiredService<ContextoBase>();
        if (dbContextBase.Database.GetPendingMigrations().Any())
        {
            dbContextBase.Database.Migrate();
        }
        if (dbContextIdentity.Database.GetPendingMigrations().Any())
        {
            dbContextIdentity.Database.Migrate();
        }
    }

}