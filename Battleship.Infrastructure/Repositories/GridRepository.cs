using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Domain;
using Battleship.Core.Enums;
using Battleship.Core.Repositories;
using Battleship.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Infrastructure.Repositories
{
    public class GridRepository : IGridRepository
    {
        private readonly BattleshipContext _context;

        public GridRepository(BattleshipContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Grid grid)
        {
            await _context.Grids.AddAsync(grid);
            await _context.SaveChangesAsync();
        }
    }
}
