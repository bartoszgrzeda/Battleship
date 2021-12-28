using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Domain;

namespace Battleship.Core.Repositories
{
    public interface IPlayerRepository : IRepository
    {
        Task AddAsync(Player player);
        Task UpdateAsync(Player player);
    }
}
