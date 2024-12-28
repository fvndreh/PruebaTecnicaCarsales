using Microsoft.AspNetCore.Mvc;
using Carsales.Models;
using Carsales.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarSales.Controllers
{
    [ApiController]
    [Route("api/episodes")]
    public class RickAndMortyController : ControllerBase
    {
        private readonly RickAndMortyService _rickAndMortyService;

        public RickAndMortyController(RickAndMortyService rickAndMortyService)
        {
            _rickAndMortyService = rickAndMortyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEpisodes([BindRequired,FromQuery] string page, [BindRequired,FromQuery] string pageSize)
{

    if (!int.TryParse(page, out int pageInt) || pageInt < 0)
    {
        return BadRequest("Please enter a valid number for the page.");
    }

    if (!int.TryParse(pageSize, out int pageSizeInt) || pageSizeInt <= 0)
    {
        return BadRequest("Please enter a valid page size.");
    }

    var result = await _rickAndMortyService.GetEpisodesAsync(pageInt, pageSizeInt);
    return Ok(result);
}



    }
}
