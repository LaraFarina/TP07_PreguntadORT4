public static class Juego{
    private static string _username{get;set;}
    public static int _puntajeActual{get;set;} //tendria que ser private pero para que funcione esta asi. 
    private static int  _cantidadPreguntasCorrectas{get;set;}
    private static List<Pregunta> _preguntas{get;set;}
    private static List<Respuesta> _respuestas{get;set;}
    private static bool _fin = false;

    public static bool Fin{
        get{return _fin;}
        set{_fin = value;}
    }
    public static void InicializarJuego(){
        _username = ""; 
        _puntajeActual = 0;
        _cantidadPreguntasCorrectas = 0;
        List<Pregunta> _preguntas= new List<Pregunta>();
        List<Respuesta> _respuestas = new List<Respuesta>();
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
        InicializarJuego();
    }
    public static Pregunta ObtenerProximaPregunta(){ //agregar una lista que guarde todas las preguntas ya hechas para evitar que se repitan y para que cuando todas hayan sido respondidas se pueda terminar el juego. fz lo hace.
        List<Pregunta> ListaPreguntasHechas = new List<Pregunta>(); //guarda las preguntas ya hechas para evitar que se repitan jaja. 
        Random random = new Random();
        Pregunta preguntaRandom;
        do{
            int indiceAleatorio = random.Next(0, _preguntas.Count); //cambiar para que solo elija preguntas de la categoria elegida por el usuario. 
            preguntaRandom = _preguntas[indiceAleatorio];

        } while(ListaPreguntasHechas.Contains(preguntaRandom));
        
        ListaPreguntasHechas.Add(preguntaRandom);

        return preguntaRandom;
    }

    public static List<Respuesta> ObtenerProximasRespuestas(int idPregunta){ //cada pregunta tiene 3 opciones de respuesta. solo 1 opcion es correcta. tiene que mostrar esas 3 opciones. 
        List<Respuesta> ListaPosiblesRespuestas = new List<Respuesta>();
        Console.WriteLine(_respuestas[0].IdRespuesta);
        for(int i=0; i<_respuestas.Count; i++){ //se podria tambien hacer un foreach pero es lo mismo. aunque el foreach es mas limpio. 
            System.Console.WriteLine(idPregunta);
            if(_respuestas[i].IdPregunta == idPregunta){
                ListaPosiblesRespuestas.Add(_respuestas[i]);
            }
        }
        // int j = 0;
        // foreach(Respuesta respuesta in _respuestas){
        //     if(respuesta.IdPregunta == idPregunta){
        //         ListaPosiblesRespuestas.Add(_respuestas[j]);
        //     }
        //     j++;
        // }
        
            // if(ListaPosiblesRespuestas != null){
            //     return ListaPosiblesRespuestas;
            // }else{
            //     List<Respuesta> ListaError = new List<Respuesta>();
                
            //     return ListaError;
            // }
        
        return ListaPosiblesRespuestas;
    }

    public static bool VerificarRespuesta(int idPregunta, int idRespuesta){ //ver si funciona. 
        bool validacion = false; 

        foreach(Respuesta respuesta in _respuestas){
            if(respuesta.IdPregunta == idPregunta){
                if(respuesta.Correcta == true){
                    validacion = true; 
                    _puntajeActual++;
                    _cantidadPreguntasCorrectas++;
                }
            }
        }
        return validacion; 
    }
}