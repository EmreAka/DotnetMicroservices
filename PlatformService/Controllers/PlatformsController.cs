using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository repository;
    private readonly IMapper mapper;
    private readonly ICommandDataClient commandDataClient;

    public PlatformsController(IPlatformRepository repository,
        IMapper mapper,
        ICommandDataClient commandDataClient)
            => (this.repository, this.mapper, this.commandDataClient)
            = (repository, mapper, commandDataClient);

    [HttpGet]
    public IActionResult GetPlatforms()
    {
        var platforms = repository.GetAllPlatforms();
        var platformDtos = mapper.Map<IEnumerable<PlatformReadDto>>(platforms);

        return Ok(platformDtos);
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public IActionResult GetPlatformById(int id)
    {
        var platform = repository.GetPlatformById(id);

        if (platform is null) return NotFound();

        var platformDto = mapper.Map<PlatformReadDto>(platform);

        return Ok(platformDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platform = mapper.Map<Platform>(platformCreateDto);

        repository.CreatePlatform(platform);

        repository.SaveChanges();

        var platformReadDto = mapper.Map<PlatformReadDto>(platform);

        try
        {
            await commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
        }

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }
}
