using Battleship.Core.Domain;
using Battleship.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Repositories;

namespace Battleship.Infrastructure.Services
{
    public class FieldService : IFieldService
    {
        private readonly IFieldRepository _fieldRepository;

        public FieldService(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }

        public Field GetNextField(Field field, Direction direction)
        {
            var horizontal = field.Horizontal;
            var vertical = field.Vertical;

            switch (direction)
            {
                case Direction.North:
                    ++vertical;
                    break;

                case Direction.East:
                    ++horizontal;
                    break;

                case Direction.South:
                    --vertical;
                    break;

                case Direction.West:
                    --horizontal;
                    break;
            }

            return GetFieldByCoordinates(field.Grid, horizontal, vertical);
        }

        public async Task SetStateAsync(Field field, FieldState state)
        {
            field.SetState(state);
            await _fieldRepository.UpdateAsync(field);
        }

        public Field GetFieldByCoordinates(Grid grid, Coordinate horizontal, Coordinate vertical)
        {
            return grid.Fields.SingleOrDefault(x => x.Horizontal == horizontal && x.Vertical == vertical);
        }

        public async Task CreateAsync(Guid id, Grid grid, Coordinate horizontal, Coordinate vertical)
        {
            var field = new Field(id, grid, horizontal, vertical);
            await _fieldRepository.AddAsync(field);
        }

        public IEnumerable<Field> GetNextFields(Field field)
        {
            var directions = Enum.GetValues(typeof(Direction))
                .Cast<Direction>();

            var fields = directions.Select(x => GetNextField(field, x));

            return fields.Where(x => x != null);
        }
    }
}
