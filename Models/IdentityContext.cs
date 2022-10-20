using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StrangerRecord.Models.Entity;

namespace StrangerRecord.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant d'autres propriétés à votre classe ApplicationUser. Pour en savoir plus, consultez https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
        public virtual ICollection<Carte> Cartes { get; set; }
        public virtual ICollection<Sejour> Sejours { get; set; }

        public virtual ICollection<Identification> Identifications { get; set; }
        public int centreId { get; set; }
        public string FullName { get; set; }

    
    }




    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Commune> Communes { get; set; }
        public DbSet<Ville> Villes { get; set; }
        public DbSet<Sejour> Sejours { get; set; }
        public DbSet<Carte> Cartes { get; set; }
        public DbSet<TypePasseport> TypePasseports { get; set; }
        public DbSet<TypeVisa> TypeVisas { get; set; }
        public DbSet<CentreEnregistrement> CentreEnregistrements { get; set; }
        public DbSet<Identification> Identifications { get; set; }

        public IdentityContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Ville>()
                     .HasMany<CentreEnregistrement>(e => e.Centres)
                     .WithRequired(e => e.ville)
                     .HasForeignKey<int>(e => e.ville_id);

            modelBuilder.Entity<Ville>()
                .HasMany<Commune>(e => e.Communes)
                .WithRequired(e => e.ville)
                .HasForeignKey<int>(e => e.ville_id);

            modelBuilder.Entity<CentreEnregistrement>()
                .HasMany<Identification>(e => e.Identifications)
                .WithRequired(e => e.Centre)
                .HasForeignKey<int>(e => e.centre_id);

            modelBuilder.Entity<ApplicationUser>()
                       .HasMany<Identification>(e => e.Identifications)
                       .WithRequired(e => e.Encodeur)
                       .HasForeignKey<string>(e => e.encodeur_id);



            //carte 
            modelBuilder.Entity<Commune>()
                               .HasMany<Carte>(e => e.Cartes)
                               .WithRequired(e => e.Commune)
                               .HasForeignKey<int>(e => e.adresse_commune_id);

            modelBuilder.Entity<Identification>()
                                  .HasMany<Carte>(e => e.Cartes)
                                  .WithRequired(e => e.Identification)
                                  .HasForeignKey<string>(e => e.identification_id);

            modelBuilder.Entity<ApplicationUser>()
                         .HasMany<Carte>(e => e.Cartes)
                         .WithRequired(e => e.Encodeur)
                         .HasForeignKey<string>(e => e.encodeur_id);

            modelBuilder.Entity<CentreEnregistrement>()
                             .HasMany<Carte>(e => e.Cartes)
                             .WithRequired(e => e.Centre)
                             .HasForeignKey<int>(e => e.centre_id);


            // sejour
            modelBuilder.Entity<Carte>()
                                      .HasMany<Sejour>(e => e.Sejours)
                                      .WithRequired(e => e.Carte)
                                      .HasForeignKey<string>(e => e.carte_id);


            modelBuilder.Entity<ApplicationUser>()
                                      .HasMany<Sejour>(e => e.Sejours)
                                      .WithRequired(e => e.Encodeur)
                                      .HasForeignKey<string>(e => e.encodeur_id);

            modelBuilder.Entity<CentreEnregistrement>()
                                             .HasMany<Sejour>(e => e.Sejours)
                                             .WithRequired(e => e.Centre)
                                             .HasForeignKey<int>(e => e.centre_id);


            modelBuilder.Entity<TypePasseport>()
                .HasMany<Carte>(e => e.Cartes)
                .WithRequired(e => e.TypePasseport)
                .HasForeignKey<int>(e => e.type_passeport_id);

            modelBuilder.Entity<TypeVisa>()
                .HasMany<Carte>(e => e.Cartes)
                .WithRequired(e => e.TypeVisa)
                .HasForeignKey<int>(e => e.type_visa_id);

        } 
    }


}