using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Domain;
using Battleship.Core.Enums;

namespace Battleship.Infrastructure.Services
{
    public interface IFieldService : IService
    {
        Task SetStateAsync(Field field, FieldState state);
        Field GetNextField(Field field, Direction direction);
        Field GetFieldByCoordinates(Grid grid, Coordinate horizontal, Coordinate vertical);
        Task CreateAsync(Guid id, Grid grid, Coordinate horizontal, Coordinate vertical);
        IEnumerable<Field> GetNextFields(Field field);
    }
}
