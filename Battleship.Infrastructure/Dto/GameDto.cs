using Battleship.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Infrastructure.Dto
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public GameState? State { get; set; }
        public string CurrentMovePlayer { get; set; }
        public string Winner { get; set; }
        public IEnumerable<PlayerDto> Players { get; set; }
    }
}
