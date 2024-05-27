
using GalloFlix.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace GalloFlix.Data;

public class AppDbContext : DbContext
{
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
   {
   }
   public DbSet<AppUser> AppUsers { get; set; }
   public DbSet<Genre> Genres { get; set; }

   public DbSet<Movie> Movies { get; set; }

   public DbSet<MovieGenre> MovieGenres { get; set; }
   protected override void OnModelCreating(ModelBuilder builder)
   {
      base.OnModelCreating(builder);

      #region Configuração Do Muitos para Muitos do MovieGenre

      builder.Entity<MovieGenre>()
           .HasKey(mg => new { mg.MovieId, mg.GenreId });

      builder.Entity<MovieGenre>()
         .HasOne(mg => mg.Movie)
         .WithMany(m => m.Genres)
         .HasForeignKey(mg => mg.MovieId);

      builder.Entity<MovieGenre>()
         .HasOne(mg => mg.Genre)
         .WithMany(g => g.Movies)
         .HasForeignKey(mg => mg.GenreId);
      #endregion
   
       #region Popular os dados de Usuarios
      //Perfil - Identityrole
      List<IdentityRole> roles = new()
      {
         new IdentityRole()
         {
            Id = Guid.NewGuid().ToString(),
            Name = "Administrador",
            NormalizedName = "ADIMINISTRADOR" 
         },
         new IdentityRole()
         {
            Id = Guid.NewGuid().ToString(),
            Name = "Moderador",
            NormalizedName = "MODERADOR" 
         },
         new IdentityRole()
         {
            Id = Guid.NewGuid().ToString(),
            Name = "Usuário",
            NormalizedName = "USUÁRIO" 
         }
      };
      builder.Entity<IdentityRole>().HasData(roles);

      // Conta de Usuário - IdentityUser
      List<IdentityUser> users = new()
      { 
         new IdentityUser()
         {
            Id = Guid.NewGuid().ToString(),
            Email = "admi@galloflix.com",
            NormalizedEmail = "ADMIN@GALLOFLIX.COM",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            EmailConfirmed = true,
            LockoutEnabled = false,

         },
         new IdentityUser()
         {
            Id = Guid.NewGuid().ToString(),
            Email = "user@gmail.com",
            NormalizedEmail = "USER@GMAIL.COM",
            UserName = "user",
            NormalizedUserName = "USER",
            EmailConfirmed = true,
            LockoutEnabled = false,

         }

      };
      foreach (var user in users)
      {
         PasswordHasher<IdentityUser> pass = new();
         pass.HashPassword(user,"@Etec123");
      }
      builder.Entity<IdentityUser>().HasData(users);

      // Dados Pessoais
      List<AppUser> appUsers = new()
      {
         new AppUser()
         {
            AppUserId = users[0].Id,
            Name = "Eduardo Henrique Santos Silva",
            Birthday = DateTime.Parse("21/01/2008"),

         },
         new AppUser()
         {
            AppUserId = users[1].Id,
            Name = "Baiano",
            Birthday = DateTime.Parse("25/09/2000"),
         }
      };
      builder.Entity<AppUser>().HasData();

      List<IdentityUserRole> identityUserRoles = new()
      {
         new IdentityUserRole<string>;
         {
            UserId = Users,
            RoleId = roles,
         }
         new IdentityUserRole<string>;
         {
            UserId = Users,
            RoleId = roles,
         }
      }
       #endregion   
   }

}
