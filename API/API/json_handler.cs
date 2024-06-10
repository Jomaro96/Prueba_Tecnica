using System.IO;
using System.Text.Json;
using API.Models;
namespace API
{
    /// <summary>
    /// clase que se encarga de la deserializacion del archivo json
    /// </summary>
    public class json_handler
    {
        private static readonly string fileJson = "data.json";
        public static List<Data> ReadJson()
        {
            try
            {
                if (!File.Exists(fileJson))
                {
                    throw new FileNotFoundException("No se encontro el archivo JSON.");
                }
                var jsonData = File.ReadAllText(fileJson);
                var dataList = JsonSerializer.Deserialize<List<Data>>(jsonData);

                return dataList ?? new List<Data>();
            }
            catch (Exception e)
            {
                throw new Exception("Error intentando leer el archivo JSON", e);
               
            }
            
        }
    }
}
