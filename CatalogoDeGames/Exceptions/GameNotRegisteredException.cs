using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeGames.Exceptions
{
    public class GameNotRegisteredException: Exception
    {
        public GameNotRegisteredException()
            : base("Game Not Registered")
        { }
    }
}
