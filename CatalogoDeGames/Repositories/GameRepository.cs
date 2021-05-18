using CatalogoDeGames.Entities;
using CatalogoDeGames.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeGames.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("6d78f893-c2df-4b87-9ab5-e0ef880dc0b9"), new Game{Id = Guid.Parse("6d78f893-c2df-4b87-9ab5-e0ef880dc0b9"), Name = "Game A", Produce = "Producer A",Price = 100} },
            {Guid.Parse("a6d0ff5d-06a6-40ae-9d68-685abb7cd503"), new Game{Id = Guid.Parse("a6d0ff5d-06a6-40ae-9d68-685abb7cd503"), Name = "Game B", Produce = "Producer B",Price = 100*2} },
            {Guid.Parse("ce4f7a65-8cc4-491b-86d7-389b83e55e54"), new Game{Id = Guid.Parse("ce4f7a65-8cc4-491b-86d7-389b83e55e54"), Name = "Game C", Produce = "Producer C",Price = 100*3} },
            {Guid.Parse("edd3bb4a-5b67-4f13-be68-bfbe8e66153d"), new Game{Id = Guid.Parse("edd3bb4a-5b67-4f13-be68-bfbe8e66153d"), Name = "Game D", Produce = "Producer D",Price = 100*4} },
            {Guid.Parse("dabd124c-56a4-41f9-96c5-19f4135f9eb3"), new Game{Id = Guid.Parse("dabd124c-56a4-41f9-96c5-19f4135f9eb3"), Name = "Game E", Produce = "Producer E",Price = 100*5} },
            {Guid.Parse("be63c783-c783-4e4e-9968-69d1a171ffab"), new Game{Id = Guid.Parse("be63c783-c783-4e4e-9968-69d1a171ffab"), Name = "Game F", Produce = "Producer F",Price = 100*6} }
        };

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task Insert(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task<List<Game>> Obtain(int page, int amount)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * amount).Take(amount).ToList());
        }

        public Task<Game> Obtain(Guid id)
        {
            if (!games.ContainsKey(id))
                return null;

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Obtain(string name, string producer)
        {
            return Task.FromResult(games.Values.Where(games => games.Name.Equals(name) && games.Produce.Equals(producer)).ToList());
        }

        public Task Remove(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public Task Update(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }
    }
}
