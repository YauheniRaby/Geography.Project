using FluentMigrator.Runner;

namespace Geography.Api.Migrations
{
    public static class MigrationExtension
    {
        public static IApplicationBuilder Migrate(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var runner = scope.ServiceProvider.GetService<IMigrationRunner>();

            runner.ListMigrations();
            
            runner.MigrateUp(20220620100000);
            runner.MigrateUp(20220620103000);
            runner.MigrateUp(20220620105000); 

            return app;
        }
    }
}
