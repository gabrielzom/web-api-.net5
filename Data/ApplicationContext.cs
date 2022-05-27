using Microsoft.EntityFrameworkCore;
using System;

namespace MonitoriaWEBAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=MonitoriaApiWEB;User Id=sa;Password=Biels@@1");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(table => {

                table.ToTable("clients");
                table.Property("NameAndSurname").HasMaxLength(40).HasColumnName("name_and_surname");
                table.Property("RegisterOfPhysicalPerson").HasColumnType("CHAR(11)").HasColumnName("register_of_physical_person");
                table.Property("DateOfBorn").HasColumnType("DATE").HasColumnName("date_of_born");
                table.Property("Genre").HasMaxLength(11).HasColumnName("genre");

                table.Property("ClientId").HasColumnType("INT").HasColumnName("client_id");
            });   
        }
    }
}