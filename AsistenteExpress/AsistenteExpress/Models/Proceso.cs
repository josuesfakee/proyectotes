using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsistenteExpress.Models
{
    public class Proceso
    {
        public int IdProceso { get; set; }
        public string Asunto { get; set; }
        public List<SelectListItem> Campañas { get; set; }
        public int IdCampania { get; set; }
        public List<SelectListItem> Perfiles { get; set; }
        public int? IdPerfil { get; set; }
        public List<SelectListItem> Tipos { get; set; }
        public int? IdTipo { get; set; }
        public List<SelectListItem> Motivos { get; set; }
        public int? IdMotivo { get; set; }
        public List<SelectListItem> SubMotivos { get; set; }
        public int? IdSubMotivo { get; set; }
    }
}