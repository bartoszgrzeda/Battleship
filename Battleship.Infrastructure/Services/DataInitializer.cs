using Battleship.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IShipService _shipService;

        public DataInitializer(IShipService shipService)
        {
            _shipService = shipService;
        }

        public async Task SeedAsync()
        {
            await InitializeShips();
        }

        private async Task InitializeShips()
        {
            var tasks = new List<Task>
            {
                _shipService.CreateAsync(ShipType.Carrier, 5, 1),
                _shipService.CreateAsync(ShipType.Battleship, 4, 1),
                _shipService.CreateAsync(ShipType.Cruiser, 3, 1),
                _shipService.CreateAsync(ShipType.Destroyer, 2, 2),
                _shipService.CreateAsync(ShipType.Submarine, 1, 2)
            };

            await Task.WhenAll(tasks);
        }
    }
}
