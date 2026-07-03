using LogicaNegocio.ClasesDominio;
using LogicaNegocio.Enums;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class StellarMindsContext : DbContext
    {        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Observacion> Observaciones { get; set; }
        public DbSet<ObjetoCeleste> ObjetosCelestes { get; set; }
        public DbSet<AuditoriaPrestamo> AuditoriasPrestamos { get; set; }

        public StellarMindsContext(DbContextOptions<StellarMindsContext> options) : base(options)
        {
        }

        // configuracion opcional (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // herencia en Equipo
            //table per hierarchy, una sola tabla para todos los tipos de equipo, con una columna discriminadora
            //los equipos comparten muchas propiedades
            modelBuilder.Entity<Equipo>()
                .UseTphMappingStrategy(); 

            // enums como string 
            modelBuilder.Entity<Prestamo>()
                .Property(p => p.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Telescopio)
                .WithMany()
                .HasForeignKey(p => p.TelescopioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Montura)
                .WithMany()
                .HasForeignKey(p => p.MonturaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Camara)
                .WithMany()
                .HasForeignKey(p => p.CamaraId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Ocular)
                .WithMany()
                .HasForeignKey(p => p.OcularId)
                .OnDelete(DeleteBehavior.Restrict); //no permite borrar si esta siendo usado en un prestamo

            modelBuilder.Entity<Observacion>()
                .Property(o => o.Indicador)
                .HasConversion<string>();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .OwnsOne(u => u.Email);

            modelBuilder.Entity<Usuario>()
                .OwnsOne(u => u.Password); //no crea tablas email ni password, solo columnas en Usuario

            modelBuilder.Entity<ObjetoCeleste>()
                .Property(o => o.MagnitudAparente)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ObjetoCeleste>()
            .HasData(

                new ObjetoCeleste
                {
                    Id = 1,
                    Nombre = "Saturno",
                    Tipo = TipoObjetoCeleste.PLANETA,
                    MagnitudAparente = 0.46m
                },

                new ObjetoCeleste
                {
                    Id = 2,
                    Nombre = "Júpiter",
                    Tipo = TipoObjetoCeleste.PLANETA,
                    MagnitudAparente = -2.20m
                },

                new ObjetoCeleste
                {
                    Id = 3,
                    Nombre = "Marte",
                    Tipo = TipoObjetoCeleste.PLANETA,
                    MagnitudAparente = -1.10m
                },

                new ObjetoCeleste
                {
                    Id = 4,
                    Nombre = "Venus",
                    Tipo = TipoObjetoCeleste.PLANETA,
                    MagnitudAparente = -4.40m
                },

                new ObjetoCeleste
                {
                    Id = 5,
                    Nombre = "M42",
                    Tipo = TipoObjetoCeleste.NEBULOSA,
                    MagnitudAparente = 4.00m
                },

                new ObjetoCeleste
                {
                    Id = 6,
                    Nombre = "Nebulosa Laguna",
                    Tipo = TipoObjetoCeleste.NEBULOSA,
                    MagnitudAparente = 6.00m
                },

                new ObjetoCeleste
                {
                    Id = 7,
                    Nombre = "Andrómeda",
                    Tipo = TipoObjetoCeleste.GALAXIA,
                    MagnitudAparente = 3.44m
                },

                new ObjetoCeleste
                {
                    Id = 8,
                    Nombre = "Galaxia del Remolino",
                    Tipo = TipoObjetoCeleste.GALAXIA,
                    MagnitudAparente = 8.40m
                },

                new ObjetoCeleste
                {
                    Id = 9,
                    Nombre = "Polaris",
                    Tipo = TipoObjetoCeleste.ESTRELLA,
                    MagnitudAparente = 1.98m
                },

                new ObjetoCeleste
                {
                    Id = 10,
                    Nombre = "Sirio",
                    Tipo = TipoObjetoCeleste.ESTRELLA,
                    MagnitudAparente = -1.46m
                },

                new ObjetoCeleste
                {
                    Id = 11,
                    Nombre = "Betelgeuse",
                    Tipo = TipoObjetoCeleste.ESTRELLA,
                    MagnitudAparente = 0.42m
                },

                new ObjetoCeleste
                {
                    Id = 12,
                    Nombre = "NGC 2392",
                    Tipo = TipoObjetoCeleste.NEBULOSA,
                    MagnitudAparente = 9.10m
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}