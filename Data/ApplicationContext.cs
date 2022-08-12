using Microsoft.EntityFrameworkCore;
using System;

namespace MonitoriaWEBAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseMySql("Server=localhost;Database=web_api_dotnet_600;Uid=root;Pwd=Biels@@1;", new MySqlServerVersion(new Version(8,0,29)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(table =>
            {
                table.ToTable("Tab_Clients");

                table.HasKey(client => client.ClientId).HasName("id");
                
                table.Property("NameAndSurname").HasMaxLength(40).HasColumnName("name_and_surname");
                table.Property("RegisterOfPhysicalPerson").HasColumnType("CHAR(11)").HasColumnName("register_of_physical_person");
                table.Property("DateOfBorn").HasColumnType("DATE").HasColumnName("date_of_born");
                table.Property("Genre").HasMaxLength(11).HasColumnName("genre");
            });   
        }
    }
}