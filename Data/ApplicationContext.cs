using Microsoft.EntityFrameworkCore;
using System;

namespace MonitoriaWEBAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DEVELOPMENT;Initial Catalog=MonitoriaApiWEB;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(table => {

                table.ToTable("Clientes");
                table.Property("Nome").HasMaxLength(40);
                table.Property("CPF").HasColumnType("CHAR(11)");
                table.Property("Nascimento").HasColumnType("DATE");

                table.HasKey("Id");
            });   
        }
    }
}