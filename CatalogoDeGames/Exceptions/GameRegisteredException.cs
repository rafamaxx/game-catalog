using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeGames.Exceptions
{
    public class GameRegisteredException : Exception
    {
        public GameRegisteredException()
            : base (" Game Alread Registered")
        { }
    }
}
