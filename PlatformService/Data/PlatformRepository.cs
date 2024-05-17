using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformRepository : IPlatformRepository
{
    private readonly AppDbContext context;
    
    public PlatformRepository(AppDbContext context)
        => this.context = context;

    public void CreatePlatform(Platform platform)
    {
        context.Platforms.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        var platforms = context.Platforms.ToList();
        return platforms;
    }

    public Platform? GetPlatformById(int id)
    {
        var platform = context.Platforms.FirstOrDefault(p => p.Id == id);
        return platform;
    }

    public bool SaveChanges()
    {
        var result = context.SaveChanges();
        return result >= 0;
    }
}
