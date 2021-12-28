using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Domain;
using Battleship.Core.Enums;

namespace Battleship.Infrastructure.Services
{
    public interface IGridService : IService
    {
        Task<bool> ArrangeShipsAsync(Grid grid);
        bool IsFieldWithStateExisting(Grid grid, FieldState state);
        Field GetRandomEmptyField(Grid grid);
        Task CreateAsync(Guid id, Player player, GridType type);
    }
}
