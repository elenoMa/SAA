namespace SAA.Services
{
    /// <summary>
    /// Interfaz para el servicio de persistencia.
    /// </summary>
    public interface IPersistenceService
    {
        /// <summary>
        /// Obtiene todos los registros de un recurso especificado.
        /// </summary>
        /// <typeparam name="T">El tipo de los registros a obtener.</typeparam>
        /// <param name="resourceName">El nombre del recurso de donde obtener los registros.</param>
        /// <returns>Una lista de registros del tipo especificado o nulo si no se encuentran registros.</returns>
        List<T>? GetAll<T>(string resourceName);

        /// <summary>
        /// Obtiene un registro por su ID de un recurso especificado.
        /// </summary>
        /// <typeparam name="T">El tipo del registro a obtener.</typeparam>
        /// <param name="id">El ID del registro a obtener.</param>
        /// <param name="resourceName">El nombre del recurso de donde obtener el registro.</param>
        /// <returns>El registro del tipo especificado o nulo si no se encuentra.</returns>
        T? GetById<T>(int id, string resourceName);

        /// <summary>
        /// Obtiene una lista de registros que coinciden con una propiedad específica de un recurso especificado.
        /// </summary>
        /// <typeparam name="T">El tipo de los registros a obtener.</typeparam>
        /// <param name="propertyName">El nombre de la propiedad por la cual filtrar.</param>
        /// <param name="propertyValue">El valor de la propiedad por la cual filtrar.</param>
        /// <param name="resourceName">El nombre del recurso de donde obtener los registros.</param>
        /// <returns>Una lista de registros que coinciden con la propiedad especificada o nulo si no se encuentran registros.</returns>
        List<T>? GetByProperty<T>(string propertyName, object propertyValue, string resourceName);

        /// <summary>
        /// Agrega o actualiza un registro en un recurso especificado.
        /// </summary>
        /// <typeparam name="T">El tipo del registro a agregar o actualizar.</typeparam>
        /// <param name="entity">El registro a agregar o actualizar.</param>
        /// <param name="resourceName">El nombre del recurso donde agregar o actualizar el registro.</param>
        void AddOrUpdate<T>(T entity, string resourceName);

        /// <summary>
        /// Elimina un registro por su ID de un recurso especificado.
        /// </summary>
        /// <typeparam name="T">El tipo del registro a eliminar.</typeparam>
        /// <param name="id">El ID del registro a eliminar.</param>
        /// <param name="resourceName">El nombre del recurso de donde eliminar el registro.</param>
        void Delete<T>(int id, string resourceName);
    }
}
