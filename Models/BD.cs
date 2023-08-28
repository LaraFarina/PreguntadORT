using System.Data.SqlClient; 
using Dapper; 

public class BD{
private static string _connectionString = @"Server=localhost;DataBase=PreguntadOrt;Trusted_Connection=True;";
 
    public static List<Categoria> ObtenerCategorias(){
        List<Categoria> listaCategorias;
        using(SqlConnection db = new SqlConnection(_connectionString)){
        string sql = "SELECT * FROM Categorias";
        listaCategorias = db.Query<Categoria>(sql).ToList();
        }
        return listaCategorias;
    }
    
    public static List<Dificultad> ObtenerDificultades(){
        List<Dificultad> listaDificultades;
        using(SqlConnection db = new SqlConnection(_connectionString)){
        string sql = "SELECT * FROM Dificultades"; 
        listaDificultades = db.Query<Dificultad>(sql).ToList();
        }
        return listaDificultades;
    }

    public static List<Pregunta> ObtenerPreguntas(int idDificultad, int idCategoria){ 
        List<Pregunta> listaPreguntas = new List <Pregunta>(); 
        using(SqlConnection db = new SqlConnection(_connectionString)){ 
            
            if(idDificultad == -1 && idCategoria == -1){
                string sql = "SELECT * FROM Preguntas"; 
                listaPreguntas = db.Query<Pregunta>(sql).ToList();
            }
            else if(idDificultad == -1 && idCategoria != -1){
                string sql = "SELECT * FROM Preguntas WHERE IdCategoria = @IdCategoria"; 
                listaPreguntas = db.Query<Pregunta>(sql, new{IdCategoria = idCategoria}).ToList();
            }
            else if(idCategoria == -1 && idDificultad != -1){
                string sql = "SELECT * FROM Preguntas WHERE IdDificultad = @IdDificultad"; 
                listaPreguntas = db.Query<Pregunta>(sql, new{IdDificultad = idDificultad}).ToList();
            }
            
            else{
                string sql =  "SELECT * FROM Preguntas WHERE IdDificultad = @IdDificultad AND IdCategoria = @IdCategoria"; 
                listaPreguntas = db.Query<Pregunta>(sql, new{IdDificultad = idDificultad, IdCategoria = idCategoria}).ToList();     
            }
    
        }
        return listaPreguntas;
    }

    public static List<Respuesta> ObtenerRespuestas(List<Pregunta> preguntas){
        List<Respuesta> listaRespuestas = new List<Respuesta>();
            
        foreach(Pregunta preg in preguntas){
            string SQL = "SELECT * FROM Respuestas WHERE IdPregunta = @pIdPregunta";
            using(SqlConnection db = new SqlConnection(_connectionString)){
                listaRespuestas.AddRange(db.Query<Respuesta>(SQL, new{pIdPregunta = preg.IdPregunta}));
            }
        }
        return listaRespuestas;
    }
}