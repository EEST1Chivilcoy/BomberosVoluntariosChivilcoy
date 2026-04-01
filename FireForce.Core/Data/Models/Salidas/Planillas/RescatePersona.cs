using Vista.Data.Enums;

namespace Vista.Data.Models.Salidas.Planillas
{
    public class RescatePersona : Rescate
    {
        //CARCTERÍSTICAS DEL LUGAR

        //Tipo de lugar persona

        public TipoLugarRescatePersona LugarRescatePersona { get; set; }
    }
}
