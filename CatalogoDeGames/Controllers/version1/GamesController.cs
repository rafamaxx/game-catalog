using CatalogoDeGames.InputModel;
using CatalogoDeGames.InputModel.Services;
using CatalogoDeGames.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeGames.Controllers.version1
{
    [Route("api/version1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameservice;
        public GamesController(IGameService gameService)
        {
            _gameservice = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> Obtain([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int amount = 5)
        {
            var games = await _gameservice.Obtain(page, amount);
            if (games.Count() == 0)
                return NoContent();
            return Ok(games);
        }

        [HttpGet("{gameID:guid}")]
        public async Task<ActionResult<List<GameViewModel>>> Obtain([FromRoute] Guid gameID)
        {
            var games = await _gameservice.Obtain(gameID);
            if (games == null)
                return NoContent();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel insertGame)
        {
            try{
                var game = await _gameservice.InsertGame(insertGame);
            }
            catch(Exception ex)
            {
                return UnprocessableEntity("Insert fail");
            }
            return Ok();
        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame,[FromBody] GameInputModel game)
        {
            try
            {
                await _gameservice.UpdateGame(idGame, game);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound("Update fail. Game not exist!");
            }
        }

        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame,[FromRoute] double price)
        {
            try
            {
                await  _gameservice.UpdateGame(idGame, price);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound("Update fail. Game not exist!");
            }
        }

        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idGame)
        {
            try
            {
                await _gameservice.DeleteGame(idGame);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound("No");
            }
        }


    }
}
