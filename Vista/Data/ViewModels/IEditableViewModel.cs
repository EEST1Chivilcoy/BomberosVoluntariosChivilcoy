namespace Vista.Data.ViewModels
{
    /// <summary>
    /// Combina las capacidades de clonación y actualización para ViewModels editables.
    /// </summary>
    /// <typeparam name="T">Tipo del ViewModel</typeparam>
    public interface IEditableViewModel<T> : ICloneable<T>, IUpdatable<T> 
        where T : class
    {
    }
}