using GuardiaComunal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Guardia_Comunal.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext() :base("DefaultConnection")
        {
                
        }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Act> Acts { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Audit> Audits { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Contravention> Contraventions { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Domain> Domains { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Inspector> Inspectors { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Liberation> Liberations { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.LiberationPlace> LiberationPlaces { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Module> Modules { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Nighborhood> Nighborhoods { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Observation> Observations { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Permission> Permissions { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Person> People { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Police> Police { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.PoliceStation> PoliceStations { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Rol> Rols { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Street> Streets { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.VehicleBrand> VehicleBrands { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.VehicleModel> VehicleModels { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.VehicleType> VehicleTypes { get; set; }

        public System.Data.Entity.DbSet<GuardiaComunal.Models.Window> Windows { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Act>()
            //.HasMany(c => c.)
            //.WithOptional()
            //.Map(m => m.MapKey("ClaimId"));

        }
        

    }
}