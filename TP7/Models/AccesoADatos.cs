using System.Text.Json;
using EspacioTarea;
namespace EspacioAccesoADatos;

public class AccesosADatos
{
    public List<Tarea>? Obtener()
    {
        List<Tarea>? nuevaListaTarea = null;
        if(File.Exists("Tareas.json"))
        {
            string JsonTarea = File.ReadAllText("Tareas.json");
            List<Tarea>? ListAux = JsonSerializer.Deserialize<List<Tarea>>(JsonTarea);
            nuevaListaTarea = ListAux;
        }
        return nuevaListaTarea;
    }
    public void Guardar(List<Tarea> ListTarea)
    {
        string info = JsonSerializer.Serialize(ListTarea);
        File.WriteAllText("Tareas.json", info);
    }
}