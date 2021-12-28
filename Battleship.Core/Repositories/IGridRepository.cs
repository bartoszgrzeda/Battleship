using Battleship.Core.Domain;
using Battleship.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Repositories
{
    public interface IGridRepository : IRepository
    {
        Task AddAsync(Grid grid);
    }
}
