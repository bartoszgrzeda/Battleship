using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Domain
{
    public class Player
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public Field? LastFireField { get; protected set; }
        public Game Game { get; protected set; }
        public IEnumerable<Grid> Grids { get; protected set; }

        protected Player()
        {
            Grids = new HashSet<Grid>();
        }

        public Player(Guid id, Game game, string name) : this()
        {
            Id = id;
            Game = game;
            Name = name;
        }

        public void SetLastFireField(Field field)
        {
            LastFireField = field;
        }
    }
}
