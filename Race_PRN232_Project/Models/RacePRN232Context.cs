using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Race_PRN232_Project.Models
{
    public partial class RacePRN232Context : DbContext
    {
        public RacePRN232Context()
        {
        }

        public RacePRN232Context(DbContextOptions<RacePRN232Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Race> Races { get; set; } = null!;
        public virtual DbSet<RaceDistance> RaceDistances { get; set; } = null!;
        public virtual DbSet<RaceParticipant> RaceParticipants { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SupportTeam> SupportTeams { get; set; } = null!;
        public virtual DbSet<SupportTeamMember> SupportTeamMembers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:MyCnn");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.Caption).HasMaxLength(255);

                entity.Property(e => e.UploadedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Url).HasMaxLength(500);

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.RaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Image__RaceId__38996AB5");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.LocationName).HasMaxLength(200);
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.ToTable("Race");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.RaceName).HasMaxLength(150);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Race__LocationId__31EC6D26");
            });

            modelBuilder.Entity<RaceDistance>(entity =>
            {
                entity.ToTable("RaceDistance");

                entity.Property(e => e.CutoffTimeHours).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.DistanceKm).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.RaceDistances)
                    .HasForeignKey(d => d.RaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RaceDista__RaceI__34C8D9D1");
            });

            modelBuilder.Entity<RaceParticipant>(entity =>
            {
                entity.HasKey(e => new { e.RaceId, e.UserId })
                    .HasName("PK__RacePart__D4835A70A3903942");

                entity.ToTable("RaceParticipant");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Distance)
                    .WithMany(p => p.RaceParticipants)
                    .HasForeignKey(d => d.DistanceId)
                    .HasConstraintName("FK__RaceParti__Dista__47DBAE45");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.RaceParticipants)
                    .HasForeignKey(d => d.RaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RaceParti__RaceI__45F365D3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RaceParticipants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RaceParti__UserI__46E78A0C");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.RoleName).HasMaxLength(100);
            });

            modelBuilder.Entity<SupportTeam>(entity =>
            {
                entity.ToTable("SupportTeam");

                entity.Property(e => e.ContactPhone).HasMaxLength(20);

                entity.Property(e => e.TeamName).HasMaxLength(150);

                entity.HasOne(d => d.Leader)
                    .WithMany(p => p.SupportTeams)
                    .HasForeignKey(d => d.LeaderId)
                    .HasConstraintName("FK__SupportTe__Leade__3C69FB99");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.SupportTeams)
                    .HasForeignKey(d => d.RaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SupportTe__RaceI__3B75D760");
            });

            modelBuilder.Entity<SupportTeamMember>(entity =>
            {
                entity.ToTable("SupportTeamMember");

                entity.Property(e => e.IsLeader).HasDefaultValueSql("((0))");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleInTeam).HasMaxLength(100);

                entity.HasOne(d => d.SupportTeam)
                    .WithMany(p => p.SupportTeamMembers)
                    .HasForeignKey(d => d.SupportTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SupportTe__Suppo__412EB0B6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SupportTeamMembers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SupportTe__UserI__4222D4EF");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ__User__A9D105344A7A851D")
                    .IsUnique();

                entity.Property(e => e.AccessFailedCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Avatar).HasMaxLength(500);

                entity.Property(e => e.ConcurrencyStamp).HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.EmailConfirmed).HasDefaultValueSql("((0))");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.FullName)
                    .HasMaxLength(201)
                    .HasComputedColumnSql("(([FirstName]+' ')+[LastName])", true);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.LockoutEnabled).HasDefaultValueSql("((1))");

                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.PhoneNumberConfirmed).HasDefaultValueSql("((0))");

                entity.Property(e => e.SecurityStamp).HasMaxLength(255);

                entity.Property(e => e.TwoFactorEnabled).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__RoleId__2D27B809");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
