using Battleship.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Infrastructure.Dto
{
    public class FieldDto
    {
        public Coordinate Horizontal { get; set; }
        public Coordinate Vertical { get; set; }
        public FieldState State { get; set; }
    }
}
