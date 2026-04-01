namespace Vista.Helpers
{
    public static class Base64Helper
    {
        public static async Task<string> ConvertObjectURLToBase64(string objectURL)
        {
            using (var httpClient = new HttpClient())
            {
                var imageBytes = await httpClient.GetByteArrayAsync(objectURL);
                return Convert.ToBase64String(imageBytes);
            }
        }

        public static async Task<string> StreamToBase64(Stream stream)
        {
            //Para guardar imagenes las guardamos en base64 y para esto necesitamos un array para hacer la conversion
            return Convert.ToBase64String(await StreamToArrayAsync(stream));
        }

        private static async Task<byte[]> StreamToArrayAsync(Stream stream)
        {
            //Este metodo convierte un stream en un array para que despues se convierta en un base64
            byte[] bytes = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int leer;
                while ((leer = await stream.ReadAsync(bytes, 0, bytes.Length)) > 0)
                {
                    ms.Write(bytes, 0, leer);
                }
                return ms.ToArray();
            }
        }
    }
}