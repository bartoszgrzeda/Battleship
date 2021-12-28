using Battleship.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Infrastructure.EF
{
    public class BattleshipContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Grid> Grids { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Battleship");
            //optionsBuilder.UseSqlServer("connectionString");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Game>()
                .HasOne(x => x.CurrentMovePlayer);
            modelBuilder.Entity<Game>()
                .HasOne(x => x.Winner);
            modelBuilder.Entity<Game>()
                .HasMany(x => x.Players)
                .WithOne(x => x.Game);

            modelBuilder.Entity<Player>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Player>()
                .HasMany(x => x.Grids)
                .WithOne(x => x.Player);

            modelBuilder.Entity<Grid>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Grid>()
                .HasMany(x => x.Fields)
                .WithOne(x => x.Grid);

            modelBuilder.Entity<Field>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Ship>()
                .HasKey(x => x.Type);
        }
    }
}
