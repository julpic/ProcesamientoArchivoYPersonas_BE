using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using System;
using System.IO;
using System.Linq;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {

        private static IWebHostEnvironment _webHostEnvironment;

        public PersonasController(IWebHostEnvironment enviroment)
        {
            _webHostEnvironment = enviroment;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<string> cargaDeArchivo()
        {
            //Request.File
            var files = Request.Form.Files;

            var files2=HttpContext.Request.Form.Files;
            HttpContext.Session.SetString("session name", "session string");
            /*var priceDetails = HttpContext.Session.ser;

                Request.HttpContext.re*/
            foreach (var file2 in files)
            {
                if (file2.Length > 0)
                {
                    try
                    {
                        if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\UserFiles\\"))
                        {
                            Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\UserFiles\\");
                        }
                        string filePath = _webHostEnvironment.WebRootPath + "\\UserFiles\\" + file2.FileName;
                        using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\UserFiles\\" + file2.FileName))
                        {
                            file2.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                        if (this.processFile(filePath))
                        {
                            return "El archivo ha sido  procesado correctamente";
                        }
                        else
                        {
                            return "No se pudo procesar el archivo ";
                        }
                        

                    }
                    catch (Exception e)
                    {
                        return "No se pudo procesar el archivo "+e.Message;

                    }
                }
                else
                {
                    return " La carga de archivo falló";
                   
                }
            }
            return "Seleccione algún archivo para subir";

            ///////////

            /*if (file.file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\UserFiles\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\UserFiles\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\UserFiles\\" + file.FileName))
                    {
                        file.file.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    this.processFile(filePath);
                    return this.averageMaxMinPerClub();

                }
                catch (Exception e)
                {
                    return null;
                    
                }
            }
            else
            {
                // return " Upload failed";
                return null;
            }*/
        }

       

            private bool processFile(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            using (var context = new Recursiva_DBContext())
            {
                foreach (var line in lines)
                {
                    var values = line.Split(";");

                    var persona = new Persona();
                    persona.Nombre = values[0];
                    persona.Edad = Int32.Parse(values[1]);
                    persona.ClubDeportivo = values[2];
                    persona.EstadoCivil = values[3];
                    persona.NivelEstudios = values[4];

                    context.Personas.Add(persona);
                    context.SaveChanges();
                }

            }
            return true;
        }

        [HttpGet]
        [Route("opcion1")]
        public int totalRecords()
        {
            using (var context = new Recursiva_DBContext())
            {
                return context.Personas.Count();
            }
        }

        [HttpGet]
        [Route("opcion2")]
        public double averageAgeRacing()
        {
            using (var context = new Recursiva_DBContext())
            {
                return (double)context.Personas.Average(persona => persona.Edad);
            }
        }

        [HttpGet]
        [Route("opcion3")]
        public IEnumerable<object> marriedAndUniversity()
        {
            IEnumerable<object> persons = null;
            using (var context = new Recursiva_DBContext())
            {
                persons = context.Personas
                                   .Where(s => s.EstadoCivil == "Casado" && s.NivelEstudios == "Universitario")
                                   .OrderBy(s => s.Edad)
                                   .Select(s => new
                                   {
                                       s.Nombre,
                                       s.Edad,
                                       s.ClubDeportivo
                                   }).ToList();
            }
            return persons;
        }

        [HttpGet]
        [Route("opcion4")]
        public IEnumerable<object> mostCommonRiverNames()
        {
            IEnumerable<object> persons = null;
            using (var context = new Recursiva_DBContext())
            {
                persons = from s in context.Personas
                          group s by s.Nombre into g
                          let namesCount = g.Count()
                          orderby namesCount descending
                          select new
                          {
                              Nombre = g.Key
                          };
                persons.Take(5);
            }
            return persons;
        }

        [HttpGet]
        [Route("opcion5")]
        public IEnumerable<object> averageMaxMinPerClub()
        {
            IEnumerable<object> persons = null;
            using (var context = new Recursiva_DBContext())
            {
                persons = context
    .Personas
    .GroupBy(l => l.ClubDeportivo, l => l.Edad)
    .Select(group => new
    {
        ClubDeportivo = group.Key,
        Average = group.Average(),
        Minimum = group.Min(),
        Maximum = group.Max()
    })
    .ToList();
            }

            return persons;
        }
    }
}
