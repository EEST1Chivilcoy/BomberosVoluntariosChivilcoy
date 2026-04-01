namespace Vista.Data.ViewModels
{
    /// <summary>
    /// Define la capacidad de actualizar las propiedades desde otro objeto del mismo tipo.
    /// </summary>
    /// <typeparam name="T">Tipo del objeto fuente</typeparam>
    public interface IUpdatable<in T> where T : class
    {
        /// <summary>
        /// Actualiza las propiedades de este objeto con los valores del objeto fuente.
        /// </summary>
        /// <param name="source">Objeto fuente con los nuevos valores</param>
        void ActualizarDesde(T source);
    }
}