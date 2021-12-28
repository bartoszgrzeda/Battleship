using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Domain;

namespace Battleship.Core.Repositories
{
    public interface IGameRepository : IRepository
    {
        Task<Game> GetAsync(Guid id);
        Task<IEnumerable<Game>> GetAllAsync();
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
    }
}
