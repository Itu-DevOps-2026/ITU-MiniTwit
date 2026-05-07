using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DotNetEnv;

namespace MiniTwit.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MiniTwitDBContext>
{
    public MiniTwitDBContext CreateDbContext(string[] args)
    {

        Env.Load();

        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");



        var optionsBuilder = new DbContextOptionsBuilder<MiniTwitDBContext>();
        optionsBuilder.UseMySql(connectionString!, new MySqlServerVersion(new Version(8, 0, 36)));

        return new MiniTwitDBContext(optionsBuilder.Options);
    }
}