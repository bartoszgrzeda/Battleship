using Battleship.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Infrastructure.Dto
{
    public class GridDto
    {
        public IEnumerable<FieldDto> Fields { get; set; }
    }
}
