using EspacioTarea;
using EspacioAccesoADatos;

namespace EspacioManejoDeTareas;

public class ManejoDeTareas 
{
    private AccesosADatos accesosADatos;
    private List<Tarea>? listaTarea;

    public ManejoDeTareas(AccesosADatos accesosADatos)
    {
        this.accesosADatos = accesosADatos;
        listaTarea = accesosADatos.Obtener();
    }
    public bool CrearTarea(string titulo, string descripcion)
    {
        
        int id;
        if(listaTarea == null)
        {
            listaTarea = new List<Tarea>();
        }
        id = listaTarea.Count+1;
        Tarea? aux = BuscarTarea(id);
        if(aux != null)
        {
            aux = listaTarea[listaTarea.Count-1];
            if(aux!=null){int idAux = aux.Id+1; id = idAux;}
        }
        Tarea nuevaTarea = new Tarea(id,titulo,descripcion);
        listaTarea.Add(nuevaTarea);
        accesosADatos.Guardar(listaTarea);
        if(listaTarea != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Tarea? BuscarTarea(int id)
    {
        if(listaTarea == null)
        {
            return null;
        }
        Tarea? tareaEncontrada = listaTarea.FirstOrDefault(t => t.Id == id);
        return tareaEncontrada;
    }

    public bool ActualizarTarea(int id, int estado)
    {
        if(listaTarea == null)
        {
            return false;
        }
        Tarea? tareaElegida = listaTarea.FirstOrDefault(t => t.Id == id);
        if(tareaElegida!=null){
            if(estado == 1) {tareaElegida.Estado = Tarea.Estados.EnProgreso;}
            else{tareaElegida.Estado = Tarea.Estados.Completada; }
            accesosADatos.Guardar(listaTarea);
            return true;
        }
        return false;
    }

    public bool EliminarTarea(int id)
    {
        bool aux = false;
        if(listaTarea != null)
        {
            Tarea? tareaElegida = listaTarea.FirstOrDefault(t => t.Id == id);
            if(tareaElegida != null)
            {
                if(listaTarea.Remove(tareaElegida))
                {
                    aux = true;
                    accesosADatos.Guardar(listaTarea);
                }
                
            }
        }
        return aux;
    }

    public List<Tarea>? MostrarTareas()
    {
        return listaTarea;
    }
    public List<Tarea>? MostrarTareasCompletas()
    {
        List<Tarea> listaAux = new List<Tarea>();
        if(listaTarea != null){
            foreach(var t in listaTarea)
            {
                if(t.Estado == Tarea.Estados.Completada)
                {
                    listaAux.Add(t);
                }
            }
        }
        return listaAux;
    }
}