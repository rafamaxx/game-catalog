using CatalogoDeGames.Entities;
using CatalogoDeGames.Exceptions;
using CatalogoDeGames.InputModel;
using CatalogoDeGames.InputModel.Services;
using CatalogoDeGames.Repositories;
using CatalogoDeGames.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeGames.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task DeleteGame(Guid idGame)
        {
            var game = _gameRepository.Obtain(idGame);
            if (game == null)
                throw new GameNotRegisteredException();

            await _gameRepository.Remove(idGame);
        }

        public async Task<GameViewModel> InsertGame(GameInputModel game)
        {
            var GameEntitie = await _gameRepository.Obtain(game.Name, game.Producer);
            if (GameEntitie.Count > 0)
                throw new GameRegisteredException();

            var GameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Produce = game.Producer,
                Price = game.Price
            };

            await _gameRepository.Insert(GameInsert);
            return new GameViewModel
            {
                Id = GameInsert.Id,
                Name = GameInsert.Name,
                Producer = GameInsert.Produce,
                Price = GameInsert.Price
            };
        }

        public async Task<List<GameViewModel>> Obtain(int page, int amount)
        {
            var games = await _gameRepository.Obtain(page, amount);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Produce,
                Price = game.Price
            }).ToList();                
        }

        public async Task<GameViewModel> Obtain(Guid id)
        {
            var game = await _gameRepository.Obtain(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Produce,
                Price = game.Price
            };
        }

        public async Task UpdateGame(Guid idGame, GameInputModel game)
        {
            var GameEntitie = await _gameRepository.Obtain(idGame);
            if (GameEntitie == null)
                throw new GameNotRegisteredException();

            GameEntitie.Name = game.Name;
            GameEntitie.Produce = game.Producer;
            GameEntitie.Price = game.Price;

            await _gameRepository.Update(GameEntitie);

        }

        public async Task UpdateGame(Guid idGame, double price)
        {
            var GameEntitie = await _gameRepository.Obtain(idGame);
            if (GameEntitie == null)
                throw new GameNotRegisteredException();
            GameEntitie.Price = price;
            await _gameRepository.Update(GameEntitie);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
