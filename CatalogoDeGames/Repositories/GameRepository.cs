using CatalogoDeGames.Entities;
using CatalogoDeGames.Repository;
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
            {Guid.Parse(""), new Game{Id = Guid.Parse(""), Name = "Game A", Produce = "Producer A",Price = 100} },
            {Guid.Parse(""), new Game{Id = Guid.Parse(""), Name = "Game B", Produce = "Producer B",Price = 100*2} },
            {Guid.Parse(""), new Game{Id = Guid.Parse(""), Name = "Game C", Produce = "Producer C",Price = 100*3} },
            {Guid.Parse(""), new Game{Id = Guid.Parse(""), Name = "Game D", Produce = "Producer D",Price = 100*4} },
            {Guid.Parse(""), new Game{Id = Guid.Parse(""), Name = "Game E", Produce = "Producer E",Price = 100*5} },
            {Guid.Parse(""), new Game{Id = Guid.Parse(""), Name = "Game F", Produce = "Producer F",Price = 100*6} }
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
