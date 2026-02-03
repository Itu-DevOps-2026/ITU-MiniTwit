namespace MiniTwit.Core.Interfaces;

public interface IDbInitializer
{
    public Task SeedDatabase();
}