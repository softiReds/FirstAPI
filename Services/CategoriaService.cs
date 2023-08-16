using webapi;
using webapi.Models;

namespace weapi.Services;

public class CategoriaService : ICategoriaService
{
    TareasContext context;

    public CategoriaService(TareasContext context)
    {
        this.context = context;
    }

    public IEnumerable<Categoria> Get()
    {
        return context.Categorias;
    }

    public async Task Save(Categoria categoria)
    {
        context.Categorias.Add(categoria);
        await context.SaveChangesAsync();
    }

    public async Task Update(Categoria categoria, Guid id)
    {
        Categoria categoriaActualizar = context.Categorias.Find(id);

        if (categoriaActualizar != null)
        {
            categoriaActualizar.Nombre = categoria.Nombre;
            categoriaActualizar.Descripcion = categoria.Descripcion;
            categoriaActualizar.Peso = categoria.Peso;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id)
    {
        Categoria categoriaEliminar = context.Categorias.Find(id);

        if (categoriaEliminar != null)
        {
            context.Categorias.Remove(categoriaEliminar);

            await context.SaveChangesAsync();
        }
    }
}

public interface ICategoriaService
{
    IEnumerable<Categoria> Get();
    Task Save(Categoria categoria);
    Task Update(Categoria categoria, Guid id);
    Task Delete(Guid id);
}