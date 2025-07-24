using System.Text.Json;

namespace OfficeProject.Servicess
{
    public class DataSerializer : IDataSerializer
    {
        public void Serializer<T>(T dataSource, string componentName)
        {
            string json = JsonSerializer.Serialize(dataSource, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Console.WriteLine($"{componentName}: {json}");
        }
    }
}
