using Microsoft.AspNetCore.Mvc;

namespace TP07_PreguntadORT.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult ConfigurarJuego()
    {
        Juego.InicializarJuego();
        ViewBag.ConfigurarJuego = Juego.ObtenerCategorias();
        ViewBag.ConfigurarJuego = Juego.ObtenerDificultades(); 
        return View();
    }
     public IActionResult Comenzar(string username, int dificultad, int categoria){
        ViewBag.Username = username;
        Juego.CargarPartida(username, dificultad, categoria);
        if(username != "" || dificultad > 0 && dificultad <= 3 || categoria > 0 && categoria <= 3){
            return RedirectToAction("Jugar");
        }else{
            return RedirectToAction("ConfigurarJuego");
        }
    }

   public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta){
        ViewBag.Respuesta = Juego.VerificarRespuesta(idPregunta,idRespuesta);
        return View("Respuesta");
    }
         public IActionResult Jugar(){
        ViewBag.Pregunta = Juego.ObtenerProximaPregunta(); 
        if(ViewBag.Pregunta != null){ 
            ViewBag.RespuestasAPregunta = Juego.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta);
            return View("Juego");
        }else{ 
            return View("Fin");
        }
    }
}