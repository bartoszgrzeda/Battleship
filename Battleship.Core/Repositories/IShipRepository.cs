using Battleship.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Repositories
{
    public interface IShipRepository : IRepository
    {
        Task<IEnumerable<Ship>> GetAllAsync();
        Task AddAsync(Ship ship);
    }
}
