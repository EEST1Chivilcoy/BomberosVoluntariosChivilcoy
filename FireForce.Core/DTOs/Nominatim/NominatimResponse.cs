namespace Vista.DTOs.Nominatim
{
    public class NominatimResponse
    {
        public List<Direccion> Direcciones { get; set; } = new();
    }

    public class Direccion
    {
        /// <summary>
        /// Descripción completa de la dirección proporcionada por Nominatim.
        /// (Display Name)
        /// </summary>
        public string Descripcion { get; set; } = null!;

        /// <summary>
        /// Representa la latitud de la ubicación geográfica en grados decimales.
        /// La latitud varía de -90 a 90, donde valores positivos indican el hemisferio norte
        /// y valores negativos el hemisferio sur.
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// Representa la longitud de la ubicación geográfica en grados decimales.
        /// La longitud varía de -180 a 180, donde valores positivos indican el este de Greenwich
        /// y valores negativos indican el oeste de Greenwich.
        /// </summary>
        public double Lon { get; set; }

    }
}