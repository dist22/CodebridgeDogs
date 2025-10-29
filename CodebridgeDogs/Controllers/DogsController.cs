using CodebridgeDogs.Dto_s;
using CodebridgeDogs.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CodebridgeDogs.Controllers;

[ApiController]
[Route("[controller]")]

public class DogsController(IDogService dogService) : ControllerBase
{
    [HttpGet("ping")]
    public IActionResult Get()  => Ok("Dogshouseservice.Version1.0.1");

    [HttpGet("dogs")]
    public async Task<IActionResult> GetDogs([FromQuery] GetDogsDto dto) 
        => Ok(await dogService.GetDogsAsync(dto));
    
    [HttpPost("dog")]
    public async Task<IActionResult> PostDog([FromBody] CreateDogDto dto) 
        => Ok(await dogService.CreateDogAsync(dto));
}