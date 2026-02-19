using Newtonsoft.Json;

namespace MvcCoreSession.Helpers
{
    public class HelperJsonSession
    {
        // Vamos a almacenar datos en session
        // mediante el metodo GetString, SetString
        public static string SerializeObject<T>(T data)
        {
            // Convertimos el objeto a string mediante newton
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        // Recibimos un string y devolver cualquier objeto
        public static T DeserializeObject<T>(string data)
        {
            // Mediante newton deserializamos el objeto
            T objeto = JsonConvert.DeserializeObject<T>(data);
            return objeto;
        }
    }
}
