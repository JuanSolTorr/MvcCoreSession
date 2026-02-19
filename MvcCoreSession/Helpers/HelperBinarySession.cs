using System.Runtime.Serialization.Formatters.Binary;

namespace MvcCoreSession.Helpers
{
    public class HelperBinarySession
    {
        // Vamos a crear los métodos de tipo static
        // porque para convertir no voy a utilizar nada de esta clase
        // solo la funcionalidad
        public static byte[] ObjectToByte(Object objeto)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, objeto);
                return stream.ToArray();
            }
        }
        //CONVERTIMOS DE BYTE[] A
        public static Object ByteToObject(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
                

            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Seek(0, SeekOrigin.Begin);
                Object objeto = (Object) formatter.Deserialize(stream);
                return objeto;
            }
        }

        public string Dato { get; set; }
        public void MetodoObjeto()
        {
            //utilizo elementos de la clase
            this.Dato = "Algo";
        }
        //HelperBinarySession helper = new HelperBinarySession()
        //helper.MetodoObjeto()

        public static DateTime DameFecha()
        {
            return DateTime.Now;
        }
        //fecha = HelperBinarySession.DameFecha()
    }
}
