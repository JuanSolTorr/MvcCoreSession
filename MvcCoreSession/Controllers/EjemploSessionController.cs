using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Extensions;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        HelperSessionContextAccesor helper;
        public EjemploSessionController(HelperSessionContextAccesor helper)
        {
            this.helper = helper;
        }

        public IActionResult Index()
        {
            List<Mascota> mascotas = this.helper.GetMascotasSession();
            return View(mascotas);   
        }

        public IActionResult SessionSimple(string accion)
        {
            if(accion != null)
            {
                if(accion.ToLower() == "almacenar")
                {
                    // Guardamos datos en session
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora", DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"] = "Datos almacenados en Session";
                } 
                else if(accion.ToLower() == "mostrar")
                {
                    // Recuperamos los datos de session
                    ViewData["NOMBRE"] = HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] = HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }

        public IActionResult SessionMascotaBytes(string accion)
        {
            if(accion != null)
            {
                if(accion.ToLower() == "almacenar")
                {
                    // Guardamos datos en session
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Wall-E";
                    mascota.Raza = "Cleaner";
                    mascota.Edad = 10;
                    // Para almacenar la mascota en Session, debemos convertirlo a byte[]
                    byte[] data = HelperBinarySession.ObjectToByte(mascota);
                    // Almacenamos el objeto en session
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"] = "Mensaje almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    // Recuperamos los datos de session
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    Mascota mascota = (Mascota)HelperBinarySession.ByteToObject(data);

                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionMascotaCollectionBytes(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    // Guardamos datos en session
                    List<Mascota> mascotasList = new List<Mascota>
                    {
                        new Mascota{Nombre = "Nala", Raza = "Leona", Edad = 21},
                        new Mascota{Nombre = "Sebastian", Raza = "Cangrejo", Edad = 24},
                        new Mascota{Nombre = "Rafiki", Raza = "Brujo", Edad = 23},
                        new Mascota{Nombre = "Olaf", Raza = "Muñeco", Edad = 14}
                    };
                    byte[] data = HelperBinarySession.ObjectToByte(mascotasList);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["MENSAJE"] = "Coleccion almacenada correctamento";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotas = (List<Mascota>)HelperBinarySession.ByteToObject(data);
                    return View(mascotas);

                }  
            }
            return View();
        }

        public IActionResult SessionMascotaJson(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    // Guardamos datos en session
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Eva";
                    mascota.Raza = "Exploradora";
                    mascota.Edad = 18;
                    // Queremos guardar el objeto mascota como string en session
                    string mascotaJson = HelperJsonSession.SerializeObject<Mascota>(mascota);
                    HttpContext.Session.SetString("MASCOTAJSON", mascotaJson);
                    ViewData["MENSAJE"] = "Mensaje almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    string jsonMascota = HttpContext.Session.GetString("MASCOTAJSON");
                    Mascota mascota = HelperJsonSession.DeserializeObject<Mascota>(jsonMascota);

                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionMascotaGeneric(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    // Guardamos datos en session
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Fujur";
                    mascota.Raza = "Dragón";
                    mascota.Edad = 18;
                    // Queremos guardar el objeto mascota como string en session
                    HttpContext.Session.SetObject("MASCOTAGENERIC", mascota);
                    ViewData["MENSAJE"] = "Mensaje almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    Mascota mascota = HttpContext.Session.GetObject<Mascota>("MASCOTAGENERIC");

                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionMascotaCollectionGeneric(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    // Guardamos datos en session
                    List<Mascota> mascotasList = new List<Mascota>
                    {
                        new Mascota{Nombre = "Nala", Raza = "Leona", Edad = 21},
                        new Mascota{Nombre = "Sebastian", Raza = "Cangrejo", Edad = 24},
                        new Mascota{Nombre = "Rafiki", Raza = "Brujo", Edad = 23},
                        new Mascota{Nombre = "Olaf", Raza = "Muñeco", Edad = 14}
                    };
                    HttpContext.Session.SetObject("MASCOTAGENERIC", mascotasList);
                    ViewData["MENSAJE"] = "Coleccion almacenada correctamento";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    List<Mascota> mascotas = HttpContext.Session.GetObject<List<Mascota>>("MASCOTAGENERIC");
                    return View(mascotas);

                }
            }
            return View();
        }
    }
}
