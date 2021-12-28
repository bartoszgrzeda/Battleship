using Battleship.Core.Domain;
using Battleship.Core.Enums;
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
    public class FieldRepository : IFieldRepository
    {
        private readonly BattleshipContext _context;

        public FieldRepository(BattleshipContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Field field)
        {
            await _context.Fields.AddAsync(field);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Field field)
        {
            _context.Update(field);
            await _context.SaveChangesAsync();
        }
    }
}
