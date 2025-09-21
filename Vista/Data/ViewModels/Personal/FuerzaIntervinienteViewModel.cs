namespace Vista.Data.ViewModels.Personal
{
    public class FuerzaIntervinienteViewModel
    {
        public int Id { get; set; }
        public string NumeroUnidad { get; set; } = null!;
        public string EncargadoApellidoyNombre { get; set; } = null!;
        public SimpleFuerzaViewModel FuerzaViewModel { get; set; } = null!;
    }
}
