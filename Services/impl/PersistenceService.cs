using System.Text.Json;

namespace SAA.Services.impl
{
    public class PersistenceService : IPersistenceService
    {
        private readonly string _basePath;
        private static readonly Lazy<PersistenceService> instance = new Lazy<PersistenceService>(() => new PersistenceService());

        public static PersistenceService Instance => instance.Value;

        public PersistenceService()
        {
            _basePath = GetBasePath();
        }

        public List<T>? GetAll<T>(string resourceName)
        {
            var filePath = GetFilePath(resourceName);
            List<T>? entities = new List<T>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    var json = reader.ReadToEnd();
                    entities = JsonSerializer.Deserialize<List<T>>(json);
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al deserializar el archivo JSON: {ex.Message}");
                throw;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error de E/S al intentar leer el archivo: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw;
            }

            return entities;
        }

        public T? GetById<T>(int id, string resourceName)
        {
            var entities = GetAll<T>(resourceName);
            return (entities ?? throw new InvalidOperationException()).FirstOrDefault(e => GetIdFromEntity(e).Equals(id));
        }

        public List<T>? GetByProperty<T>(string propertyName, object propertyValue, string resourceName)
        {
            var entities = GetAll<T>(resourceName);
            if (entities == null)
            {
                return null;
            }

            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"La propiedad '{propertyName}' no existe en el tipo '{typeof(T).Name}'.");
            }

            return entities.Where(e => propertyInfo.GetValue(e)?.Equals(propertyValue) == true).ToList();
        }

        public void AddOrUpdate<T>(T entity, string resourceName)
        {
            var filePath = GetFilePath(resourceName);
            var entities = GetAll<T>(resourceName);

            var entityId = GetIdFromEntity(entity);

            if (entityId == 0)
            {
                entityId = entities != null && entities.Count > 0 ? entities.Max(e => GetIdFromEntity(e)) + 1 : 1;
                SetIdForEntity(entity, entityId);
                entities?.Add(entity);
            }
            else
            {
                var existingEntity = (entities ?? throw new InvalidOperationException()).FirstOrDefault(e => GetIdFromEntity(e).Equals(entityId));
                if (existingEntity != null)
                {
                    entities[entities.IndexOf(existingEntity)] = entity;
                }
            }

            SaveEntities(entities, filePath);
        }

        public void Delete<T>(int id, string resourceName)
        {
            var filePath = GetFilePath(resourceName);
            var entities = GetAll<T>(resourceName);
            if (entities != null)
            {
                entities.RemoveAll(e => GetIdFromEntity(e).Equals(id));
                SaveEntities(entities, filePath);
            }
        }

        private string GetBasePath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var basePath = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;
            if (basePath != null)
            {
                basePath = Path.Combine(basePath, "Resources");
                return basePath;
            }

            throw new InvalidOperationException();
        }

        private string GetFilePath(string resourceName)
        {
            return Path.Combine(_basePath, $"{resourceName.ToLower()}.json");
        }

        private void SaveEntities<T>(List<T>? entities, string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath, false))
                {
                    var json = JsonSerializer.Serialize(entities, new JsonSerializerOptions { WriteIndented = true });
                    writer.Write(json);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error de E/S al intentar escribir en el archivo: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado al intentar escribir en el archivo: {ex.Message}");
                throw;
            }
        }

        private int GetIdFromEntity<T>(T entity)
        {
            if (entity != null)
            {
                var idProp = entity.GetType().GetProperty("Id");
                if (idProp != null && idProp.PropertyType == typeof(int))
                {
                    return (int)(idProp.GetValue(entity) ?? throw new InvalidOperationException());
                }
            }

            throw new InvalidOperationException("La entidad no tiene una propiedad 'Id' válida o de tipo correcto.");
        }

        private void SetIdForEntity<T>(T entity, int id)
        {
            var idProp = entity?.GetType().GetProperty("Id");
            if (idProp != null && idProp.PropertyType == typeof(int))
            {
                idProp.SetValue(entity, id);
            }
            else
            {
                throw new InvalidOperationException("La entidad no tiene una propiedad 'Id' válida o de tipo correcto.");
            }
        }
    }
}
