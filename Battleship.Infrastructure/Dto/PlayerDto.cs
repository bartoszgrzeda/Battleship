using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Infrastructure.Dto
{
    public class PlayerDto
    {
        public string Name { get; set; }
        public GridDto Grid { get; set; }
    }
}
