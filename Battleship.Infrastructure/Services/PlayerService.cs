using Battleship.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Repositories;
using Battleship.Core.Enums;

namespace Battleship.Infrastructure.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IGridService _gridService;
        private readonly IFieldService _fieldService;

        public PlayerService(IPlayerRepository playerRepository, IGridService gridService, IFieldService fieldService)
        {
            _playerRepository = playerRepository;
            _gridService = gridService;
            _fieldService = fieldService;
        }

        public async Task<bool> ArrangeShipsAsync(Player player)
        {
            var gameGrid = GetGrid(player, GridType.Game);
            return await _gridService.ArrangeShipsAsync(gameGrid);
        }

        public async Task CreateAsync(Guid id, Game game, string name)
        {
            var player = new Player(id, game, name);
            await _playerRepository.AddAsync(player);

            await _gridService.CreateAsync(Guid.NewGuid(), player, GridType.Game);
            await _gridService.CreateAsync(Guid.NewGuid(), player, GridType.Fire);
        }

        public async Task FireAsync(Player player)
        {
            var field = GetFireField(player);
            var opponentField = GetOpponentFireField(player, field);
            var state = GetFireFieldState(opponentField);            

            await _fieldService.SetStateAsync(field, state);
            await _fieldService.SetStateAsync(opponentField, state);

            if (state == FieldState.Hit)
                await SetLastFireField(player, field);
        }

        private Field GetFireField(Player player)
        {
            if (player.LastFireField != null && player.LastFireField.State == FieldState.Hit)
            {
                var field = GetRandomFieldNextToLastFireField(player.LastFireField);
                if (field != null)
                    return field;
            }

            var emptyFieldsNextToHit = GetEmptyFieldsNextToHitFields(player);
            if (emptyFieldsNextToHit.Any())
            {
                var random = new Random();
                return emptyFieldsNextToHit.ElementAt(random.Next(emptyFieldsNextToHit.Count()));
            }

            var fireGrid = GetGrid(player, GridType.Fire);
            return _gridService.GetRandomEmptyField(fireGrid);
        }

        private Field GetOpponentFireField(Player player, Field field)
        {
            var opponent = GetOpponent(player);
            var opponentGameGrid = GetGrid(opponent, GridType.Game);
            return _fieldService.GetFieldByCoordinates(opponentGameGrid, field.Horizontal, field.Vertical);
        }

        private static FieldState GetFireFieldState(Field field)
        {
            if (field.State == FieldState.Ship)
                return FieldState.Hit;

            else
                return FieldState.Miss;
        }

        public bool IsAlive(Player player)
        {
            var grid = GetGrid(player, GridType.Game);
            return _gridService.IsFieldWithStateExisting(grid, FieldState.Ship);
        }

        public Player GetOpponent(Player player)
        {
            var players = player.Game.Players;
            return players.SingleOrDefault(x => x.Id != player.Id);
        }

        public Grid GetGrid(Player player, GridType type)
        {
            return player.Grids.SingleOrDefault(x => x.Type == type);
        }

        private async Task SetLastFireField(Player player, Field field)
        {
            player.SetLastFireField(field);
            await _playerRepository.UpdateAsync(player);
        }

        private Field GetRandomFieldNextToLastFireField(Field field)
        {
            var directions = Enum.GetValues(typeof(Direction))
                .Cast<Direction>()
                .OrderBy(x => Guid.NewGuid());

            foreach (var direction in directions)
            {
                var nextField = _fieldService.GetNextField(field, direction);

                if (nextField != null && nextField.State == FieldState.Empty)
                    return nextField;
            }

            return null;
        }

        private IEnumerable<Field> GetHitFields(Player player)
        {
            var fireGrid = GetGrid(player, GridType.Fire);
            return fireGrid.Fields.Where(x => x.State == FieldState.Hit);
        }

        private IEnumerable<Field> GetEmptyFieldsNextToHitFields(Player player)
        {
            var hitFields = GetHitFields(player);

            var fields = new List<Field>();
            foreach (var field in hitFields)
            {
                var nextFields = _fieldService.GetNextFields(field);
                fields.AddRange(nextFields);
            }

            return fields.Where(x => x.State == FieldState.Empty);            
        }
    }
}
