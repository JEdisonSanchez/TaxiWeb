using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiWeb.Models
{
    public class ConductorAfiliacion
    {
        public Conductor Conductor { get; set; }
        public long Radicado { get; set; }
    }
}