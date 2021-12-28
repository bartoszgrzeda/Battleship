using Battleship.Core.Domain;
using Battleship.Core.Repositories;
using Battleship.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Infrastructure.Repositories
{
    public class ShipRepository : IShipRepository
    {
        private readonly BattleshipContext _context;

        public ShipRepository(BattleshipContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Ship ship)
        {
            await _context.Ships.AddAsync(ship);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ship>> GetAllAsync()
        {
            return await _context.Ships.ToListAsync();
        }
    }
}
