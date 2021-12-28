using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Enums;

namespace Battleship.Core.Domain
{
    public class Field
    {
        public Guid Id { get; protected set; }
        public Coordinate Horizontal { get; protected set; }
        public Coordinate Vertical { get; protected set; }
        public FieldState State { get; protected set; }
        public Grid Grid { get; protected set; }

        protected Field()
        {
        }

        public Field(Guid id, Grid grid, Coordinate horizontal, Coordinate vertical)
        {
            if (!IsCoordinateValid(horizontal) || !IsCoordinateValid(vertical))
                throw new Exception($"Coordinates are not valid: {horizontal},{vertical}.");

            Id = id;
            Grid = grid;
            Horizontal = horizontal;
            Vertical = vertical;
            State = FieldState.Empty;
        }

        public static bool IsCoordinateValid(Coordinate coordinate)
        {
            return Enum.GetValues(typeof(Coordinate))
                .Cast<Coordinate>()
                .Contains(coordinate);
        }

        private static bool IsStateValid(FieldState state)
        {
            return Enum.GetValues(typeof(FieldState))
                .Cast<FieldState>()
                .Contains(state);
        }

        public void SetState(FieldState state)
        {
            if (!IsStateValid(state))
                throw new Exception($"State is not valid: {state}.");

            if (State == state)
                return;

            State = state;
        }
    }
}
