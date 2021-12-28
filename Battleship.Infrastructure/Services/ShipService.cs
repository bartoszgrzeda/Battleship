using AutoMapper;
using Battleship.Core.Domain;
using Battleship.Core.Enums;
using Battleship.Core.Repositories;
using Battleship.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Infrastructure.Services
{
    public class ShipService : IShipService
    {
        private readonly IShipRepository _shipRepository;

        public ShipService(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public async Task CreateAsync(ShipType type, int size, int count)
        {
            var ship = new Ship(type, size, count);
            await _shipRepository.AddAsync(ship);
        }

        public async Task<IEnumerable<Ship>> GetAllAsync()
        {
            return await _shipRepository.GetAllAsync();
        }
    }
}
