﻿namespace BlazorApp1.Data.Models.Salidas
{
    public abstract class Salida
    {
        public DateTime Fecha { get; set; }
        public DateTime HoraSalida { get; set; }
        public DateTime HoraLlegada { get; set; }


        //public int PersonalSalidaId { get; set; }
        //public PersonalSalida Personalsalida { get; set; }

        public int NumeroParte { get; set; }

        public string Descripcion { get; set; }


        public int LocalizacionId { get; set; }
        public Localizacion Localizacion { get; set; }

        public int SolicitanteId { get; set; }
        public Solicitante Solicitante { get; set; }

        public int DamnificadoId { get; set; }
        public Damnificado Damnificado { get; set; }

        public int SeguroId { get; set; }
        public Seguro Seguro { get; set; }
    }
}