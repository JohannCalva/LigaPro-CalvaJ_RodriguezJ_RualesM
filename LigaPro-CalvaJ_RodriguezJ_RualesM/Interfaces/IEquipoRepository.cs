using LigaPro_CalvaJ_RodriguezJ_RualesM.Models;

namespace LigaPro_CalvaJ_RodriguezJ_RualesM.Interfaces
{
    public interface IEquipoRepository
    {
        Task<List<Equipo>> ObtenerEquipos();
        Task<Equipo?> ObtenerEquipoPorId(int id);
        Task CrearEquipo(Equipo equipo);
        Task EditarEquipo(Equipo equipo);
        Task EliminarEquipo(int id);
    }
}
