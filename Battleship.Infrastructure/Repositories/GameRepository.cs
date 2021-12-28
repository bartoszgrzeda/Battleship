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
    public class GameRepository : IGameRepository
    {
        private readonly BattleshipContext _context;

        public GameRepository(BattleshipContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Game game)
        {
            await _context.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<Game> GetAsync(Guid id)
        {
            return await _context.Games.Include(x => x.Players)
                .ThenInclude(x => x.Grids)
                .ThenInclude(x => x.Fields)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }
    }
}
