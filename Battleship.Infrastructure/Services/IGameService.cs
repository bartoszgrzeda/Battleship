using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Domain;
using Battleship.Core.Enums;
using Battleship.Infrastructure.Dto;

namespace Battleship.Infrastructure.Services
{
    public interface IGameService : IService
    {
        Task<GameDto> GetAsync(Guid id);
        Task<IEnumerable<GameDto>> GetAllAsync();
        Task CreateAsync(Guid id);
        Task NextMoveAsync(Guid id);
        Task SetStateAsync(Game game, GameState state);
        Task ArrangeShipsAsync(Guid id);
    }
}
