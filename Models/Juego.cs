
   public static class Juego{
    private static string _username{get;set;}
    public static int _puntajeActual{get;set;}
    private static int  _cantidadPreguntasCorrectas{get;set;}
    private static List<Pregunta> _preguntas{get;set;}
    private static List<Respuesta> _respuestas{get;set;}
    private static bool _fin = false;
    private static int contador = 0;
    private static List<Pregunta> ListaPreguntasHechas = new List<Pregunta>(); 
   
    public static bool Fin{
        get{return _fin;}
        set{_fin = value;}
    }
    public static void InicializarJuego(){
        _username = "";
        _puntajeActual = 0;
        _cantidadPreguntasCorrectas = 0;
    }

    public static List<Categoria> ObtenerCategorias(){
        return BD.ObtenerCategorias();
    }

    public static List<Dificultad> ObtenerDificultades(){
        return BD.ObtenerDificultades();
    }

    public static void CargarPartida(string username, int dificultad, int categoria){
        _preguntas = BD.ObtenerPreguntas(dificultad, categoria);
        _respuestas = BD.ObtenerRespuestas(_preguntas);
        _username = username;
    }
    public static Pregunta ObtenerProximaPregunta(){ 
        
        Random random = new Random();
        Pregunta preguntaRandom;
        int indiceAleatorio;
        do{
            indiceAleatorio = random.Next(0, _preguntas.Count); 
            preguntaRandom = _preguntas[indiceAleatorio];
        }while(ListaPreguntasHechas.Contains(preguntaRandom) && ListaPreguntasHechas.Count < 2);
        ListaPreguntasHechas.Add(preguntaRandom);
        contador++;
        if(ListaPreguntasHechas.Count > 2){
            preguntaRandom = null; 
        }
        return preguntaRandom;
    }
    public static List<Respuesta> ObtenerProximasRespuestas(int idPregunta){ 
        List<Respuesta> ListaPosiblesRespuestas = new List<Respuesta>();
        Console.WriteLine(_respuestas[0].IdRespuesta);
        for(int i=0; i<_respuestas.Count; i++){ 
            System.Console.WriteLine(idPregunta);
            if(_respuestas[i].IdPregunta == idPregunta){
                ListaPosiblesRespuestas.Add(_respuestas[i]);
            }
        }
        return ListaPosiblesRespuestas;
    }

    public static bool VerificarRespuesta(int idPregunta, int idRespuesta){ 
        bool validacion = false; 
        Respuesta respuestaCorrecta = new Respuesta();
        

        foreach(Respuesta r in _respuestas){
            if(r.Correcta == true && r.IdPregunta == idPregunta){
                respuestaCorrecta = r;
            }
        }
        if(idPregunta == respuestaCorrecta.IdPregunta){
            if(idRespuesta == respuestaCorrecta.IdRespuesta){
                validacion = true;
            }
        }
        int i = 0; 

        while(i < _preguntas.Count){
            if(_preguntas[i].IdPregunta == idPregunta){
                _preguntas.Remove(_preguntas[i]);
            }
            i++;
        }
        return validacion;  
    }
}