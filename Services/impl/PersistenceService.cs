using System.Text.Json;
using SAA.Services;

public class PersistenceService : IPersistenceService
{
    private readonly string _basePath;

    public PersistenceService()
    {
        // Obtener la ruta del directorio base del proyecto
        _basePath = GetBasePath();
    }

    public List<T> GetAll<T>(string resourceName)
    {
        var filePath = GetFilePath(resourceName);
        var entities = new List<T>();

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
            // Puedes agregar más lógica de manejo de excepciones aquí, como registrar el error o lanzar una excepción personalizada.
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error de E/S al intentar leer el archivo: {ex.Message}");
            // Manejo de excepciones específicas de E/S
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
            // Otros tipos de excepciones no controladas
        }

        return entities;
    }

    public T GetById<T>(int id, string resourceName)
    {
        var filePath = GetFilePath(resourceName);
        var entities = GetAll<T>(resourceName);
        return entities.Find(e => GetIdFromEntity(e).Equals(id));
    }

    public void AddOrUpdate<T>(T entity, string resourceName)
    {
        var filePath = GetFilePath(resourceName);
        var entities = GetAll<T>(resourceName);

        var entityId = Convert.ToInt32(entity.GetType().GetProperty("Id").GetValue(entity));

        if (entityId == 0)
        {
            // Assign new Id
            var maxId = entities.Count > 0 ? entities.Max(e => GetIdFromEntity(e)) : 0;
            entityId = maxId + 1;
            entity.GetType().GetProperty("Id").SetValue(entity, entityId);
            entities.Add(entity);
        }
        else
        {
            // Update existing entity
            var index = entities.FindIndex(e => GetIdFromEntity(e).Equals(entityId));
            if (index != -1) entities[index] = entity;
        }

        SaveEntities(entities, filePath);
    }

    public void Delete<T>(int id, string resourceName)
    {
        var filePath = GetFilePath(resourceName);
        var entities = GetAll<T>(resourceName);
        entities.RemoveAll(e => GetIdFromEntity(e).Equals(id));
        SaveEntities(entities, filePath);
    }

    private string GetBasePath()
    {
        // Obtener el directorio actual de ejecución del programa
        var currentDirectory = Directory.GetCurrentDirectory();

        // Subir tres niveles para llegar a la raíz del proyecto
        var basePath = Directory.GetParent(currentDirectory).Parent.Parent.FullName;

        // Asegurarse de que la ruta use el separador correcto para el sistema operativo
        basePath = Path.Combine(basePath, "Resources");

        return basePath;
    }

    // Helper method to get the file path for a specific resource
    private string GetFilePath(string resourceName)
    {
        return Path.Combine(_basePath, $"{resourceName.ToLower()}.json");
    }

    // Helper method to save entities back to JSON file using StreamWriter
    private void SaveEntities<T>(List<T> entities, string filePath)
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
            // Manejar la excepción de E/S específicamente
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado al intentar escribir en el archivo: {ex.Message}");
            // Otros tipos de excepciones no controladas
        }
    }

    // Helper method to get the Id from an entity (assuming Id property is "Id")
    private int GetIdFromEntity<T>(T entity)
    {
        return Convert.ToInt32(entity.GetType().GetProperty("Id").GetValue(entity));
    }
}