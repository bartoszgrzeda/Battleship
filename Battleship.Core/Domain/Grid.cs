using Battleship.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Domain
{
    public class Grid
    {
        public Guid Id { get; protected set; }
        public GridType Type { get; protected set; }
        public Player Player { get; protected set; }
        public IEnumerable<Field> Fields { get; protected set; }

        protected Grid()
        {
            Fields = new HashSet<Field>();
        }

        public Grid(Guid id, Player player, GridType type) : this()
        {
            if (!IsTypeValid(type))
                throw new Exception($"Type is not valid: {type}.");

            Id = id;
            Player = player;
            Type = type;
        }

        private static bool IsTypeValid(GridType type)
        {
            return Enum.GetValues(typeof(GridType))
                .Cast<GridType>()
                .Contains(type);
        }
    }
}
