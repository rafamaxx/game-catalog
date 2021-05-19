using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoDeGames.Controllers.version1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifeCycleController : ControllerBase
    {
        public readonly IExSingleton _exSingleton1;
        public readonly IExSingleton _exSingleton2;

        public readonly IExScoped _exScoped1;
        public readonly IExScoped _exScoped2;

        public readonly IExTransient _exTransient1;
        public readonly IExTransient _exTransient2;

        public LifeCycleController(IExSingleton exSingleton1,
                                   IExSingleton exSingleton2,
                                   IExScoped exScoped1,
                                   IExScoped exScoped2,
                                   IExTransient exTransient1,
                                   IExTransient exTransient2)
        {
            _exScoped1 = exScoped1;
            _exScoped2 = exScoped2;
            _exSingleton1 = exSingleton1;
            _exSingleton2 = exSingleton2;
            _exTransient1 = exTransient1;
            _exTransient2 = exTransient2;
        }

    [HttpGet]
    public Task<string> GET()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Singleton 1: {_exSingleton1}");
            stringBuilder.AppendLine($"Singleton 2: {_exSingleton2}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Scoped 1: {_exScoped1}");
            stringBuilder.AppendLine($"Scoped 2: {_exScoped2}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Transient 1: {_exTransient1}");
            stringBuilder.AppendLine($"Transient 2: {_exTransient2}");

            return Task.FromResult(stringBuilder.ToString());
        }

        public interface Iex
        {
            public Guid Id { get; }
        }

        public interface IExSingleton: Iex
        { }

        public interface IExScoped : Iex
        { }
        public interface IExTransient : Iex
        { }


    }
}
