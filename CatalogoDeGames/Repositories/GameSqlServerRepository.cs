using CatalogoDeGames.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeGames.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }
        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

        public async Task Insert(Game game)
        {
            var command = $"insert Games (Id,Name,Produce,Price) values ('{game.Id}','{game.Name}','{game.Produce}',{game.Price})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task<List<Game>> Obtain(int page, int amount)
        {
            var games = new List<Game>();

            var command = $"select * from Games order by id offset {((page - 1) * amount)} rows fetch next {amount} rows fetch next {amount} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Produce = (string)sqlDataReader["Produce"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();
            return games;

        }

        public Task<Game> Obtain(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Game>> Obtain(string name, string producer)
        {
            var games = new List<Game>();

            var command = $"select * from Games where Name = '{name}' and Produce = '{producer}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Produce = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();
            return games;
        }

        public async Task Remove(Guid id)
        {
            var command = $"delete from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Update(Game game)
        {
            var command = $"update Games set Name = '{game.Name}',Produce = '{game.Produce}',Price = {game.Price.ToString()}";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
    }
}
