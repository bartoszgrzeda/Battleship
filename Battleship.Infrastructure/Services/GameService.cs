using Battleship.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Repositories;
using Battleship.Core.Domain;
using AutoMapper;
using Battleship.Core.Enums;

namespace Battleship.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IPlayerService _playerService;

        public GameService(IGameRepository gameRepository, IMapper mapper, IPlayerService playerService)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _playerService = playerService;
        }

        public async Task ArrangeShipsAsync(Guid id)
        {
            var game = await _gameRepository.GetAsync(id);

            if (game == null)
                throw new Exception($"Game with Id {id} does not exists.");

            if (game.State != null)
                throw new Exception($"Game with Id {id} has already started.");

            var tasks = game.Players.Select(x => _playerService.ArrangeShipsAsync(x));
            var results = await Task.WhenAll(tasks);

            if (results.Any(x => !x))
            {
                await SetStateAsync(game, GameState.Finished);
                throw new Exception($"Error occured while arranging ships for game with Id {id}.");
            }

            await SetStateAsync(game, GameState.Started);
        }

        public async Task CreateAsync(Guid id)
        {
            var game = await _gameRepository.GetAsync(id);

            if (game != null)
                throw new Exception($"Game with Id {id} already exists.");

            game = new Game(id);
            await _gameRepository.AddAsync(game);

            await CreatePlayersAsync(game);
        }

        private async Task CreatePlayersAsync(Game game)
        {
            await _playerService.CreateAsync(Guid.NewGuid(), game, "Player 1");
            await _playerService.CreateAsync(Guid.NewGuid(), game, "Player 2");
        }

        public async Task<IEnumerable<GameDto>> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return games.Select(x => _mapper.Map<Game, GameDto>(x));
        }

        public async Task<GameDto> GetAsync(Guid id)
        {
            var game = await _gameRepository.GetAsync(id);
            return _mapper.Map<Game, GameDto>(game);
        }

        public async Task NextMoveAsync(Guid id)
        {
            var game = await _gameRepository.GetAsync(id);

            if (game == null)
                throw new Exception($"Game with Id {id} does not exists.");

            if (game.State != GameState.Started)
                throw new Exception($"Game with Id {id} has wrong state.");

            if (game.CurrentMovePlayer == null)
            {
                var player = GetRandomPlayer(game);
                await SetCurrentMovePlayerAsync(game, player);
            }

            await _playerService.FireAsync(game.CurrentMovePlayer);
            var opponent = _playerService.GetOpponent(game.CurrentMovePlayer);

            if (!_playerService.IsAlive(opponent))
            {
                await SetWinnerAsync(game, game.CurrentMovePlayer);
                await SetStateAsync(game, GameState.Finished);
            }

            await SetCurrentMovePlayerAsync(game, opponent);
        }

        public async Task SetStateAsync(Game game, GameState state)
        {
            game.SetState(state);
            await _gameRepository.UpdateAsync(game);
        }

        private static Player GetRandomPlayer(Game game)
        {
            var random = new Random();
            return game.Players.ElementAt(random.Next(game.Players.Count()));
        }

        private async Task SetWinnerAsync(Game game, Player winner)
        {
            game.SetWinner(winner);
            await _gameRepository.UpdateAsync(game);
        }

        private async Task SetCurrentMovePlayerAsync(Game game, Player player)
        {
            game.SetCurrentMovePlayer(player);
            await _gameRepository.UpdateAsync(game);
        }
    }
}
