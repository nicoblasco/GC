namespace Guardia_Comunal.Migrations
{
    using GuardiaComunal.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Guardia_Comunal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Guardia_Comunal.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


           // context.Usuarios.AddOrUpdate(x => x.Id,
           // new Usuario() { Id = 1, Nombreusuario="administrador", Contraseña="1234", Role= new Rol() { Id = 1, Nombre = "Administrador", Descripcion = "Administrador" }, Enable=true }
           // );

        }



    }
}
