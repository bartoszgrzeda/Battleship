using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Enums;

namespace Battleship.Core.Domain
{
    public class Game
    {
        public Guid Id { get; protected set; }
        public GameState? State { get; protected set; }
        public Player? CurrentMovePlayer { get; protected set; }
        public Player? Winner { get; protected set; }
        public IEnumerable<Player> Players { get; protected set; }

        protected Game()
        {
            Players = new HashSet<Player>();
        }

        public Game(Guid id) : this()
        {
            Id = id;
        }

        public void SetState(GameState state)
        {
            if (!IsStateValid(state))
                throw new Exception($"State is not valid: {state}.");

            if (State == state)
                return;

            State = state;
        }

        private static bool IsStateValid(GameState state)
        {
            return Enum.GetValues(typeof(GameState))
                .Cast<GameState>()
                .Contains(state);
        }

        public void SetCurrentMovePlayer(Player player)
        {
            CurrentMovePlayer = player;
        }

        public void SetWinner(Player player)
        {
            Winner = player;
        }
    }
}
