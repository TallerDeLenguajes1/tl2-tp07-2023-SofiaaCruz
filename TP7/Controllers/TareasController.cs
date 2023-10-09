using Microsoft.AspNetCore.Mvc;
using EspacioManejoDeTareas;
using EspacioTarea;
using EspacioAccesoADatos;
namespace TP7;

[ApiController]
[Route("[controller]")]
public class TareasController : ControllerBase
{
    private ManejoDeTareas manejoDeTarea;

    private readonly ILogger<TareasController> _logger;

    public TareasController(ILogger<TareasController> logger)
    {
        _logger = logger;
        AccesosADatos accesosADatos = new AccesosADatos();
        manejoDeTarea = new ManejoDeTareas(accesosADatos);
    }

    [HttpPost("CrearTarea")]
    public ActionResult<string> CrearTarea(string titulo, string descripcion)
    {
        if(string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(descripcion))
        {
            return BadRequest("ERROR, Debe ingresar todos los valores requeridos");
        }
        if(manejoDeTarea.CrearTarea(titulo, descripcion))
        {
            return Ok("La tarea fue creada exitosamente");
        }
        else
        {
            return BadRequest("La tarea no fue creada con exito");
        }
    }

    [HttpGet("BuscarTarea/{id}")]
    public ActionResult<Tarea> BuscarTarea(int id)
    {
        Tarea? Encontrado = manejoDeTarea.BuscarTarea(id);
        if(Encontrado == null)
        {
            return BadRequest("La tarea solicitada no fue encontrada");
        }
        return Ok(Encontrado);
    }

    [HttpPut("ActualizarTarea")]
    public ActionResult<string> ActualizarTerea(int id, int estado)
    {
        if(estado != 1 && estado != 2){return BadRequest("ERROR, Debe ingresar valores correctos");}
        else
        {
            if(manejoDeTarea.ActualizarTarea(id,estado)){return Ok("El estado de la tarea fue actualizado correctamente");}
            else{return BadRequest("ERROE, El estado de la tarea no pudo ser actualizado o el ID ingresado no corresponde a una tarea");}
        }
    }

    [HttpDelete("EliminarTarea")]
    public ActionResult<string> EliminarTarea(int id)
    {
        if(manejoDeTarea.EliminarTarea(id)){
            return Ok("La Tarea fue eliminada con exito");
        }
        else
        {
            return BadRequest("La tarea solicitada no pudo ser eliminada o no existe");
        }
    }

    [HttpGet("MostrarTareas")]
    public ActionResult<List<Tarea>> MostrarTareas()
    {
        List<Tarea>? listaAux = manejoDeTarea.MostrarTareas();
        if(listaAux != null){return Ok(listaAux);}
        else{return BadRequest("La lista de tarea esta vacia");}
    }

    [HttpGet("MostrarTareasRealizadas")]
    public ActionResult<List<Tarea>> MostrarTareasCompletas()
    {
        List<Tarea>? listaAux = manejoDeTarea.MostrarTareasCompletas();
        return Ok(listaAux);
    }
}
