using FireForce.Shared.Enums;

namespace FireForce.Data.Models.Salidas.Planillas
{
    public class RescatePersona : Rescate
    {
        //CARCTERÍSTICAS DEL LUGAR

        //Tipo de lugar persona

        public TipoLugarRescatePersona LugarRescatePersona { get; set; }
    }
}
