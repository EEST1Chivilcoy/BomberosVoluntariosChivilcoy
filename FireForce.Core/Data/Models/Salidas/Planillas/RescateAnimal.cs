using FireForce.Core.Data.Enums;

namespace FireForce.Core.Data.Models.Salidas.Planillas
{
    public class RescateAnimal : Rescate
    {
        //CARCTERÍSTICAS DEL LUGAR

        //Tipo de lugar animal

        public TipoLugarRescateAnimal LugarRescateAnimal { get; set; }
    }
}