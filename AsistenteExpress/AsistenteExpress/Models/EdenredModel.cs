using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AsistenteExpress.Models
{
    //==================================================================================================================
    public class EdenredContext : DbContext
    {
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public EdenredContext()
            : base("name=ConnEdenred")
        {
            Database.SetInitializer((IDatabaseInitializer<EdenredContext>)null);
        }

        public DbSet<Archivos> Archivos { get; set; }
        public DbSet<Campanias> Campanias { get; set; }
        public DbSet<Motivos> Motivos { get; set; }
        public DbSet<Perfiles> Perfiles { get; set; }
        public DbSet<Submotivos> Submotivos { get; set; }
        public DbSet<Procesos> Procesos { get; set; }
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public class Archivos
    {
        [Key] public int Id { get; set; }
        public string Usuario { get; set; }
        public string NameFile { get; set; }
        public string ruta { get; set; }
        public string typeFile { get; set; }
        public DateTime? FechaCarga { get; set; }
        public bool Estatus { get; set; }
    }
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public class Campanias
    {
        [Key] public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
    }
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public class Motivos
    {
        [Key] public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
    }
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public class Perfiles
    {
        [Key] public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
    }
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public class Procesos
    {
        [Key] public int Id { get; set; }
        public string Asunto { get; set; }
        public int IdCampaña { get; set; }
        public int? IdPerfil { get; set; }
        public int? IdMotivo { get; set; }
        public int? IdSubMotivo { get; set; }
        public bool Estatus { get; set; }
    }
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public class Submotivos
    {
        [Key] public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
    }
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
}
    //==================================================================================================================