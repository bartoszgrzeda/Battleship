using Battleship.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Enums;

namespace Battleship.Core.Repositories
{
    public interface IFieldRepository : IRepository
    {
        Task AddAsync(Field field);
        Task UpdateAsync(Field field);
    }
}
