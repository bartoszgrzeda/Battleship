using Battleship.Core.Domain;
using Battleship.Core.Enums;
using Battleship.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Infrastructure.Services
{
    public interface IShipService : IService
    {
        Task CreateAsync(ShipType type, int size, int count);
        Task<IEnumerable<Ship>> GetAllAsync();
    }
}
