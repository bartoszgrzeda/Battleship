using Battleship.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Domain
{
    public class Ship
    {
        public ShipType Type { get; protected set; }
        public int Size { get; protected set; }
        public int Count { get; protected set; }

        protected Ship()
        {
        }

        public Ship(ShipType type, int size, int count)
        {
            if (!IsTypeValid(type))
                throw new Exception($"Type is not valid: {type}.");

            if (!IsSizeValid(size))
                throw new Exception($"Size is not valid: {type}.");

            if (!IsCountValid(count))
                throw new Exception($"Count is not valid: {type}.");

            Type = type;
            Size = size;
            Count = count;
        }

        private static bool IsTypeValid(ShipType type)
        {
            return Enum.GetValues(typeof(ShipType))
                .Cast<ShipType>()
                .Contains(type);
        }

        private static bool IsSizeValid(int size)
        {
            return size > 0;
        }

        private static bool IsCountValid(int count)
        {
            return count >= 0;
        }
    }
}
