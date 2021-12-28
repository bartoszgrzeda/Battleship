using Battleship.Core.Domain;
using Battleship.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Repositories;
using Battleship.Infrastructure.Settings;

namespace Battleship.Infrastructure.Services
{
    public class GridService : IGridService
    {
        private readonly IGridRepository _gridRepository;
        private readonly IShipService _shipService;
        private readonly IFieldService _fieldService;
        private readonly BattleshipSettings _battleshipSettings;

        public GridService(IGridRepository gridRepository, IShipService shipService, IFieldService fieldService, BattleshipSettings battleshipSettings)
        {
            _gridRepository = gridRepository;
            _shipService = shipService;
            _fieldService = fieldService;
            _battleshipSettings = battleshipSettings;
        }

        public async Task<bool> ArrangeShipsAsync(Grid grid)
        {
            var ships = await _shipService.GetAllAsync();

            if (!ships.Any(x => x.Count > 0))
                return false;

            foreach (var ship in ships.OrderByDescending(x => x.Size))
            {
                for (int i = 0; i < ship.Count; ++i)
                {
                    var result = await TryArrangeShipAsync(grid, ship);

                    if (!result)
                        return false;
                }
            }

            return true;
        }

        public bool IsFieldWithStateExisting(Grid grid, FieldState state)
        {
            return GetFieldsWithState(grid, state).Any();
        }

        private async Task<bool> TryArrangeShipAsync(Grid grid, Ship ship)
        {
            var tries = _battleshipSettings.ArrangeShipTries;
            
            for (int i = 0; i < tries; ++i)
            {
                var field = GetRandomEmptyField(grid);
                var direction = GetRandomDirection();
                var estimatedFields = GetEstimatedShipFields(ship, field, direction);

                if (estimatedFields.Any(x => x == null))
                    continue;

                if (estimatedFields.Any(x => x.State != FieldState.Empty))
                    continue;

                var tasks = estimatedFields.Select(x => _fieldService.SetStateAsync(x, FieldState.Ship));
                await Task.WhenAll(tasks);
                return true;
            }

            return false;
        }

        public Field GetRandomEmptyField(Grid grid)
        {
            var emptyFields = GetFieldsWithState(grid, FieldState.Empty);
            var random = new Random();

            return emptyFields.ElementAt(random.Next(emptyFields.Count()));
        }

        private static IEnumerable<Field> GetFieldsWithState(Grid grid, FieldState state)
        {
            return grid.Fields.Where(x => x.State == state);
        }

        private IEnumerable<Field> GetEstimatedShipFields(Ship ship, Field startingField, Direction direction)
        {
            var field = startingField;
            var fields = new List<Field>();

            for (int i = 0; i < ship.Size; ++i)
            {
                if (field == null)
                    break;

                field = _fieldService.GetNextField(field, direction);
                fields.Add(field);
            }

            return fields;
        }

        private static Direction GetRandomDirection()
        {
            var random = new Random();
            var values = Enum.GetValues(typeof(Direction))
                .Cast<Direction>();

            return values.ElementAt(random.Next(values.Count()));
        }

        public async Task CreateAsync(Guid id, Player player, GridType type)
        {
            var grid = new Grid(id, player, type);
            await _gridRepository.AddAsync(grid);

            await CreateFieldsAsync(grid);
        }

        private async Task CreateFieldsAsync(Grid grid)
        {
            var coordinates = Enum.GetValues(typeof(Coordinate))
                .Cast<Coordinate>();

            foreach (var horizontal in coordinates)
            {
                foreach (var vertical in coordinates)
                {
                    await _fieldService.CreateAsync(Guid.NewGuid(), grid, horizontal, vertical);
                }
            }
        }
    }
}
