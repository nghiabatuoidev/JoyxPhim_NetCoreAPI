using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class JoyxphimContext : DbContext
{
    public JoyxphimContext()
    {
    }

    public JoyxphimContext(DbContextOptions<JoyxphimContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<EpisodeServer> EpisodeServers { get; set; }

    public virtual DbSet<Follow> Follows { get; set; }

    public virtual DbSet<Lang> Langs { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieCategory> MovieCategories { get; set; }

    public virtual DbSet<MovieCountry> MovieCountries { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<YearRelease> YearReleases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=JOYXPHIM;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK_User");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__D54EE9B4FDA18784");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Country__7E8CD05564E45A89");
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => e.EpisodeId).HasName("PK__Episode__8478035523D8D3A8");

            entity.HasOne(d => d.Movie).WithMany(p => p.Episodes).HasConstraintName("FK__Episode__movie_i__4BAC3F29");
        });

        modelBuilder.Entity<EpisodeServer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EpisodeSevers");

            entity.HasOne(d => d.Episode).WithMany(p => p.EpisodeServers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EpisodeSevers_Episodes_episode_id");

            entity.HasOne(d => d.Server).WithMany(p => p.EpisodeServers).HasConstraintName("FK_EpisodeSevers_Servers_server_id");
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.HasKey(e => e.FollowId).HasName("PK_FollowMovie");

            entity.Property(e => e.FollowId).ValueGeneratedNever();

            entity.HasOne(d => d.Movie).WithMany(p => p.Follows).HasConstraintName("FK_FollowMovie_Movie");

            entity.HasOne(d => d.User).WithMany(p => p.Follows).HasConstraintName("FK_follow_UserId_User");
        });

        modelBuilder.Entity<Lang>(entity =>
        {
            entity.HasKey(e => e.LangId).HasName("PK_Lang");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__83CDF749D31BB344");

            entity.Property(e => e.IsChieurap).HasDefaultValue(false);
            entity.Property(e => e.IsSubDocquyen).HasDefaultValue(false);
            entity.Property(e => e.IsTrending).HasDefaultValue(false);

            entity.HasOne(d => d.Lang).WithMany(p => p.Movies).HasConstraintName("FK_LangId");

            entity.HasOne(d => d.Status).WithMany(p => p.Movies).HasConstraintName("FK_MovieStatusId");

            entity.HasOne(d => d.Type).WithMany(p => p.Movies).HasConstraintName("FK_MovieTypeId");

            entity.HasOne(d => d.YearRelease).WithMany(p => p.Movies).HasConstraintName("FK_Movie_YearRelease_Id");
        });

        modelBuilder.Entity<MovieCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MovieCategory");

            entity.HasOne(d => d.Category).WithMany(p => p.MovieCategories)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__MovieCate__categ__5165187F");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieCategories)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__MovieCate__movie__5070F446");
        });

        modelBuilder.Entity<MovieCountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MovieCou__F6B00F2125DDFCF7");

            entity.HasOne(d => d.Country).WithMany(p => p.MovieCountries)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MovieCountry_Country_country_id");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieCountries)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MovieCountry_Movie_movie_id");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK_MovieStatus");

            entity.Property(e => e.Value).IsFixedLength();
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK_MovieType");

            entity.Property(e => e.Value).IsFixedLength();
        });

        modelBuilder.Entity<YearRelease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_YearRelease");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
