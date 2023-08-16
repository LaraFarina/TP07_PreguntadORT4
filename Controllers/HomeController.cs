using Microsoft.AspNetCore.Mvc;

namespace TP07_PreguntadORT4.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
       public IActionResult ConfigurarJuego()
    {
        Juego.InicializarJuego();
        ViewBag.Categoria = Juego.ObtenerCategorias();
        ViewBag.Dificultad = Juego.ObtenerDificultades(); 
        return View();
    }
       public IActionResult Comenzar(string username, int dificultad, int categoria){
        ViewBag.Username = username;
        Juego.CargarPartida(username, dificultad, categoria);
        if(username != "" || dificultad > 0 && dificultad <= 3 || categoria > 0 && categoria<= 3){
            return RedirectToAction("Jugar");
        }else{
            return RedirectToAction("ConfigurarJuego");
        }
    }
        public IActionResult Jugar(){
        ViewBag.Pregunta = Juego.ObtenerProximaPregunta(); 
        if(ViewBag.Pregunta != null){ 
            ViewBag.RespuestasPreguntas = Juego.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta);
            Console.WriteLine(ViewBag.RespuestasPreguntas[0].IdRespuesta);
            return View("Juego");
        }else{ 
            return View("Fin");
        }
    }
        [HttpPost]public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta){
        ViewBag.Respuesta = Juego.VerificarRespuesta(idPregunta,idRespuesta);
       // ViewBag.PuntajeFinal = Juego._puntajeActual;  // Esto es para e puntaje Actual, puede no funcionar :)
        return View("Respuesta");
    }
}
