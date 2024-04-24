﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcCorePersonajesSeries.Models
{
    public class Personaje
    {
        public int IdPersonaje { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Serie { get; set; }
    }
}
