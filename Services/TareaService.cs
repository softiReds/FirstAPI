using webapi.Models;

namespace webapi.Services;

public class TareaService : ITareaService
{
    TareasContext context;

    public TareaService(TareasContext context)
    {
        this.context = context;
    }

    public IEnumerable<Tarea> Get()
    {
        return context.Tareas;
    }

    public async Task Save(Tarea tarea)
    {
        context.Add(tarea);

        await context.SaveChangesAsync();
    }

    public async Task Update(Tarea tarea, Guid id)
    {
        Tarea tareaActualizar = context.Tareas.Find(id);

        if (tareaActualizar != null)
        {
            tareaActualizar.CategoriaId = tarea.CategoriaId;
            tareaActualizar.Titulo = tarea.Titulo;
            tareaActualizar.Descripcion = tarea.Descripcion;
            tareaActualizar.PrioridadTarea = tarea.PrioridadTarea;
            tareaActualizar.Categoria = tarea.Categoria;
            tareaActualizar.Resumen = tarea.Resumen;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id)
    {
        Tarea tareaEliminar = context.Tareas.Find(id);

        if (tareaEliminar != null)
        {
            context.Remove(tareaEliminar);

            await context.SaveChangesAsync();
        }
    }
}

public interface ITareaService
{
    IEnumerable<Tarea> Get();
    Task Save(Tarea tarea);
    Task Update(Tarea tarea, Guid id);
    Task Delete(Guid id);
}