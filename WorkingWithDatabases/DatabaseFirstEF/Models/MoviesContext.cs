using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFMoviesDatabaseFirst.Models
{
    public partial class MoviesContext : DbContext
    {
        public MoviesContext()
        {
        }

        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieCast> MovieCasts { get; set; } = null!;
        public virtual DbSet<MovieCrew> MovieCrews { get; set; } = null!;
        public virtual DbSet<MovieGenre> MovieGenres { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<ProductionCompany> ProductionCompanies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=Movies;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(200)
                    .HasColumnName("department_name");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("gender");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedNever()
                    .HasColumnName("gender_id");

                entity.Property(e => e.Gender1)
                    .HasMaxLength(20)
                    .HasColumnName("gender");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.Property(e => e.GenreId)
                    .ValueGeneratedNever()
                    .HasColumnName("genre_id");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(100)
                    .HasColumnName("genre_name");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movie");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Budget).HasColumnName("budget");

                entity.Property(e => e.Homepage)
                    .HasMaxLength(1000)
                    .HasColumnName("homepage");

                entity.Property(e => e.MovieStatus)
                    .HasMaxLength(50)
                    .HasColumnName("movie_status");

                entity.Property(e => e.Overview)
                    .HasMaxLength(1000)
                    .HasColumnName("overview");

                entity.Property(e => e.Popularity)
                    .HasColumnType("decimal(12, 6)")
                    .HasColumnName("popularity");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("release_date");

                entity.Property(e => e.Revenue).HasColumnName("revenue");

                entity.Property(e => e.Runtime).HasColumnName("runtime");

                entity.Property(e => e.Tagline)
                    .HasMaxLength(1000)
                    .HasColumnName("tagline");

                entity.Property(e => e.Title)
                    .HasMaxLength(1000)
                    .HasColumnName("title");

                entity.Property(e => e.VoteAverage)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("vote_average");

                entity.Property(e => e.VoteCount).HasColumnName("vote_count");

                entity.HasMany(d => d.Companies)
                    .WithMany(p => p.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "MovieCompany",
                        l => l.HasOne<ProductionCompany>().WithMany().HasForeignKey("CompanyId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_movie_company_production_company"),
                        r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_movie_company_movie1"),
                        j =>
                        {
                            j.HasKey("MovieId", "CompanyId").HasName("PK_movie_company_movie_id");

                            j.ToTable("movie_company");

                            j.HasIndex(new[] { "CompanyId" }, "fk_mc_comp");

                            j.HasIndex(new[] { "MovieId" }, "fk_mc_movie");

                            j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");

                            j.IndexerProperty<int>("CompanyId").HasColumnName("company_id");
                        });
            });

            modelBuilder.Entity<MovieCast>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("movie_cast");

                entity.HasIndex(e => e.GenderId, "fk_gender_idx");

                entity.HasIndex(e => e.MovieId, "fk_movie_idx");

                entity.HasIndex(e => e.PersonId, "fk_person_idx");

                entity.Property(e => e.CastOrder).HasColumnName("cast_order");

                entity.Property(e => e.CharacterName)
                    .HasMaxLength(400)
                    .HasColumnName("character_name");

                entity.Property(e => e.GenderId).HasColumnName("gender_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.HasOne(d => d.Gender)
                    .WithMany()
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("movie_cast$fk_gender");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_movie_cast_movie");

                entity.HasOne(d => d.Person)
                    .WithMany()
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("movie_cast$fk_person_2");
            });

            modelBuilder.Entity<MovieCrew>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("movie_crew");

                entity.HasIndex(e => e.DepartmentId, "fk_department_idx");

                entity.HasIndex(e => e.MovieId, "fk_movie_idx");

                entity.HasIndex(e => e.PersonId, "fk_person_idx");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Job)
                    .HasMaxLength(200)
                    .HasColumnName("job");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.HasOne(d => d.Department)
                    .WithMany()
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("movie_crew$fk_department");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_movie_crew_movie");

                entity.HasOne(d => d.Person)
                    .WithMany()
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("movie_crew$fk_person");
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.ToTable("movie_genres");

                entity.HasIndex(e => e.GenreId, "fk_mg_genre");

                entity.HasIndex(e => e.MovieId, "fk_mg_movie");

                entity.Property(e => e.MovieGenreId).HasColumnName("movie_genre_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.MovieGenres)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movie_genres$fk_mg_genre");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieGenres)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movie_genres_movie");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.PersonId)
                    .ValueGeneratedNever()
                    .HasColumnName("person_id");

                entity.Property(e => e.PersonName)
                    .HasMaxLength(500)
                    .HasColumnName("person_name");
            });

            modelBuilder.Entity<ProductionCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PK_production_company_company_id");

                entity.ToTable("production_company");

                entity.Property(e => e.CompanyId)
                    .ValueGeneratedNever()
                    .HasColumnName("company_id");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(200)
                    .HasColumnName("company_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
