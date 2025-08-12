namespace Vista.DTOs.Nominatim
{
    public class NominatimResponse
    {
        public List<Direccion> Direcciones { get; set; } = new();
    }

    public class Direccion
    {
        /// <summary>
        /// Representa la calle asociada con la dirección.
        /// Contiene información sobre el nombre de la calle o vía pública.
        /// </summary>
        public Calle Calle { get; set; } = null!;

        /// <summary>
        /// Representa la altura asociada con la dirección.
        /// Contiene información sobre el número o valor de la dirección en la calle.
        /// </summary>
        public Altura Altura { get; set; } = null!;

        /// <summary>
        /// Representa la ubicación geográfica de la dirección.
        /// Contiene las coordenadas de latitud y longitud de la dirección en formato geográfico.
        /// </summary>
        public Ubicacion Ubicacion { get; set; } = null!;

        /// <summary>
        /// Obtiene una representación combinada del nombre de la calle y la altura de la dirección.
        /// Por ejemplo, "Avenida Siempreviva 742".
        /// </summary>
        public string NombreYAltura => $"{Calle.Nombre} {Altura.Valor}";
    }

    public class Calle
    {
        /// <summary>
        /// Representa el nombre de la calle en una dirección.
        /// Puede ser cualquier nombre de vía pública, como "Avenida de la Paz", "Calle 5", etc.
        /// </summary>
        public string Nombre { get; set; } = null!;
    }

    public class Altura
    {
        /// <summary>
        /// Representa el número o valor de la altura en una dirección de calle.
        /// </summary>
        public int Valor { get; set; }
    }

    public class Ubicacion
    {
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