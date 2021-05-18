using CatalogoDeGames.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoDeGames.InputModel.Services
{
    public interface IGameService: IDisposable
    {
        Task<List<GameViewModel>> Obtain(int page, int amount);
        Task<GameViewModel> Obtain(Guid id);
        Task<GameViewModel> InsertGame(GameInputModel game);
        Task UpdateGame(Guid idGame, GameInputModel game);
        Task UpdateGame(Guid idGame, double price);
        Task DeleteGame(Guid idGame);

    }
}
