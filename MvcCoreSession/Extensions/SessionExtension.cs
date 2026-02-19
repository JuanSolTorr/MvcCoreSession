using Microsoft.CodeAnalysis.CSharp.Syntax;
using MvcCoreSession.Helpers;

namespace MvcCoreSession.Extensions
{
    public static class SessionExtension
    {
        // Método para recuperar cualqui objeto de session
        public static T GetObject<T>(this ISession session, string key)

        {
            // Ahora mismpo ya tenemos dentro de la variable session
            // el objeto HttpContext.Session
            // Debemos recuperar el objeto json de session
            string json = session.GetString(key);
            // En session si algo no existe siempre devuelve null
            if(json == null)
            {
                return default(T);
            }
            else
            {
                // Recuperamos el objeto y lo convertimos con nuestro helper
                T data = HelperJsonSession.DeserializeObject<T>(json);
                return data;
            }
        }

        public static void SetObject<T>(this ISession session, string key, T value)
        {
            string data = HelperJsonSession.SerializeObject(value);
            session.SetString(key, data);
        }
    }
}
