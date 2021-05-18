using CatalogoDeGames.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoDeGames.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Obtain(int page, int amount);
        Task<Game> Obtain(Guid id);
        Task<List<Game>> Obtain(string name, string producer);
        Task Insert(Game game);
        Task Update(Game game);
        Task Remove(Guid id);
        
    }
}
