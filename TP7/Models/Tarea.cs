namespace EspacioTarea;

public class Tarea 
{
    private int id;
    private string titulo;
    private string descripcion;
    private Estados estado;

    public int Id { get => id; set => id = value; }
    public string Titulo { get => titulo; set => titulo = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public Estados Estado { get => estado; set => estado = value; }

    public Tarea(int id, string titulo, string descripcion)
    {
        this.id = id;
        this.titulo = titulo;
        this.descripcion = descripcion;
        estado = Estados.Pendiente;
    }

    public enum Estados{Pendiente, EnProgreso, Completada}
}