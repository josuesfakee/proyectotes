using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsistenteExpress.Models
{
    public class Proceso
    {
        public List<SelectListItem> Campañas { get; set; }
        public List<SelectListItem> Perfiles { get; set; }
        public List<SelectListItem> Motivos { get; set; }
        public List<SelectListItem> SubMotivos { get; set; }


    }
}