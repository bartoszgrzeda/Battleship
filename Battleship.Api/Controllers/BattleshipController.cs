using Battleship.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Api.Controllers
{
    [Route("[controller]")]
    public class BattleshipController : Controller
    {
        private readonly IGameService _gameService;

        public BattleshipController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var game = await _gameService.GetAsync(id);
            return Json(game);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var games = await _gameService.GetAllAsync();
            return Json(games);
        }

        [HttpPost("CreateGame/{id}")]
        public async Task<IActionResult> CreateGame(Guid id)
        {
            await _gameService.CreateAsync(id);
            return Ok();
        }

        [HttpPost("ArrangeShips/{id}")]
        public async Task<IActionResult> ArrangeShips(Guid id)
        {
            await _gameService.ArrangeShipsAsync(id);
            return Ok();
        }

        [HttpPost("NextMove/{id}")]
        public async Task<IActionResult> NextMove(Guid id)
        {
            await _gameService.NextMoveAsync(id);
            return Ok();
        }
    }
}
