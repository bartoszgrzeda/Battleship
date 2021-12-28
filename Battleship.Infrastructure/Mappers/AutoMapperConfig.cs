using AutoMapper;
using Battleship.Core.Domain;
using Battleship.Core.Enums;
using Battleship.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<Game, GameDto>()
                    .ForMember(x => x.CurrentMovePlayer, x => x.MapFrom(source => source.CurrentMovePlayer == null ? null : source.CurrentMovePlayer.Name))
                    .ForMember(x => x.Winner, x => x.MapFrom(source => source.Winner == null ? null : source.Winner.Name));
                config.CreateMap<Player, PlayerDto>()
                    .ForMember(x => x.Grid, x => x.MapFrom(source => source.Grids.SingleOrDefault(x => x.Type == GridType.Game)));
                config.CreateMap<Grid, GridDto>();
                config.CreateMap<Field, FieldDto>();
            });

            return configuration.CreateMapper();
        }
    }
}
