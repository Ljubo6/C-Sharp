using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        //public FootballBettingContext(DbContextOptions options) 
        //    : base(options)
        //{
        //}

        //protected FootballBettingContext()
        //{
        //}

        public DbSet<Bet> Bets { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurationOnUser(modelBuilder);
            ConfigurationOnTown(modelBuilder);
            ConfigurationOnTeam(modelBuilder);
            ConfigurationOnPosition(modelBuilder);
            ConfigurationOnPlayerStatistic(modelBuilder);
            ConfigurationOnPlayer(modelBuilder);
            ConfigurationOnGame(modelBuilder);
            ConfigurationOnCountry(modelBuilder);
            ConfigurationOnColor(modelBuilder);
            ConfigurationOnBet(modelBuilder);
        }

        private void ConfigurationOnBet(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Bet>(entity => 
                {
                    entity.HasKey(k => k.BetId);

                    entity.Property(p => p.Amount)
                    .IsRequired();

                    entity.Property(p => p.Prediction)
                    .IsRequired();

                    entity.Property(p => p.DateTime)
                    .IsRequired();

                    entity.HasOne(g => g.Game)
                    .WithMany(b => b.Bets)
                    .HasForeignKey(k => k.UserId);

                    entity.HasOne(u => u.User)
                    .WithMany(b => b.Bets)
                    .HasForeignKey(k => k.UserId);
                });
        }

        private void ConfigurationOnColor(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Color>(entity => 
                {
                    entity.HasKey(k => k.ColorId);

                    entity.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired()
                    .IsUnicode();
                });
        }

        private void ConfigurationOnCountry(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Country>(entity => 
                {
                    entity.HasKey(c => c.CountryId);

                    entity.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired()
                    .IsUnicode();
                });
        }

        private void ConfigurationOnGame(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity => 
            {
                entity.HasKey(g => g.GameId);

                entity.Property(p => p.AwayTeamBetRate)
                .IsRequired();

                entity.Property(p => p.AwayTeamGoals);

                entity.HasOne(t => t.AwayTeam)
                .WithMany(g => g.AwayGames)
                .HasForeignKey(k => k.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.DrawBetRate)
                .IsRequired();

                entity.Property(p => p.HomeTeamBetRate)
                .IsRequired();

                entity.Property(p => p.HomeTeamGoals)
                .IsRequired();

                entity.HasOne(t => t.HomeTeam)
                .WithMany(g => g.HomeGames)
                .HasForeignKey(k => k.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.Result)
                .IsRequired();

                entity.Property(p => p.DateTime)
                .IsRequired();
            });
        }

        private void ConfigurationOnPlayer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(k => k.PlayerId);

                entity.Property(p => p.IsInjured)
                .IsRequired();

                entity.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

                entity.HasOne(p => p.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(k => k.PositionId);

                entity.Property(p => p.SquadNumber)
                .IsRequired();

                entity.HasOne(t => t.Team)
                .WithMany(p => p.Players)
                .HasForeignKey(k => k.TeamId);
            });
        }

        private void ConfigurationOnPlayerStatistic(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerStatistic>(entity => 
            {
                entity.HasKey(ps => new { ps.GameId,ps.PlayerId});

                entity.HasOne(p => p.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(k => k.PlayerId);

                entity.HasOne(g => g.Game)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(k => k.GameId);

                entity.Property(p => p.Assists)
                .IsRequired();
                entity.Property(p => p.MinutesPlayed)
                .IsRequired();
                entity.Property(p => p.ScoredGoals)
                .IsRequired();

            });
        }

        private void ConfigurationOnPosition(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>(entity => 
            {
                entity.HasKey(k => k.PositionId);

                entity.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();
            });
        }

        private void ConfigurationOnTeam(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(entity => 
            {
                entity.HasKey(k => k.TeamId);

                entity.Property(p => p.Budget)
                .IsRequired();

                entity.Property(p => p.Initials)
                .IsRequired()
                .HasColumnType("CHAR(3)");

                entity.Property(p => p.LogoUrl)
                .HasMaxLength(30)
                .IsRequired();

                entity.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

                entity.HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(k => k.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(k => k.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Town)
                .WithMany(x => x.Teams)
                .HasForeignKey(k => k.TeamId);

            });
        }

        private void ConfigurationOnTown(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Town>(entity => 
            {
                entity.HasKey(k => k.TownId);

                entity.HasOne(t => t.Country)
                .WithMany(t => t.Towns)
                .HasForeignKey(k => k.CountryId);

                entity.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();
            });
        }

        private void ConfigurationOnUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => 
            {
                entity.HasKey(k => k.UserId);

                entity.Property(p => p.Balance);

                entity.Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode();

                entity.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

                entity.Property(u => u.Username)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

                entity.Property(u => u.Password)
                 .IsRequired()
                 .IsUnicode()
                 .HasMaxLength(25);
            });
        }
    }
}
