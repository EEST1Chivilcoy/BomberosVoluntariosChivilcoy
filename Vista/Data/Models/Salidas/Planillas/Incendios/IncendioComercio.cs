﻿using Vista.Data.Enums;

namespace Vista.Data.Models.Salidas.Planillas.Incendios
{
    public class IncendioComercio : Incendio
    {
        public TipoLugarComercioIncendio TipoLugar { get; set; }
    }
}