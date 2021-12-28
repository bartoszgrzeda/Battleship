using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Domain;
using Battleship.Core.Enums;

namespace Battleship.Infrastructure.Services
{
    public interface IPlayerService : IService
    {
        Task CreateAsync(Guid id, Game game, string name);
        Task<bool> ArrangeShipsAsync(Player player);
        bool IsAlive(Player player);
        Task FireAsync(Player player);
        Grid GetGrid(Player player, GridType type);
        Player GetOpponent(Player player);
    }
}
