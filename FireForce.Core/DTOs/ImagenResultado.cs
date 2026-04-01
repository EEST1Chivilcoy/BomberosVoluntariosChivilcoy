namespace Vista.DTOs
{
    /// <summary>
    /// DTO para representar el resultado de una imagen.
    /// </summary>
    public class ImagenResultado
    {
        public byte[] Datos { get; set; } = null!;
        public string Formato { get; set; } = null!;
    }
}
