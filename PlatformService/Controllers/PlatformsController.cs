using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository repository;
    private readonly IMapper mapper;

    public PlatformsController(IPlatformRepository repository, IMapper mapper)
        => (this.repository, this.mapper) = (repository, mapper);

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
    public IActionResult CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platform = mapper.Map<Platform>(platformCreateDto);
        
        repository.CreatePlatform(platform);
        
        repository.SaveChanges();

        var platformReadDto = mapper.Map<PlatformReadDto>(platform);

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }
}
