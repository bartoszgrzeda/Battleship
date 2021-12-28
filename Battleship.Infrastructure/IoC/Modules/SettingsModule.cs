using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Battleship.Infrastructure.Extensions;
using Battleship.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Battleship.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<BattleshipSettings>())
                .SingleInstance();
        }
    }
}
