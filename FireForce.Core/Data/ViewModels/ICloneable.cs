namespace Vista.Data.ViewModels
{
    /// <summary>
    /// Define la capacidad de clonar un objeto del mismo tipo.
    /// </summary>
    /// <typeparam name="T">Tipo del objeto a clonar</typeparam>
    public interface ICloneable<T> where T : class
    {
        /// <summary>
        /// Crea una copia independiente del objeto actual.
        /// </summary>
        T Clonar();
    }
}