using LigaPro_CalvaJ_RodriguezJ_RualesM.Models;

namespace LigaPro_CalvaJ_RodriguezJ_RualesM.Interfaces
{
    public interface IJugadorRepository
    {
        Task<bool> JugadorExiste(int id);
        Task<List<Jugador>> ObtenerJugadores();
        Task<Jugador?> ObtenerJugadorPorId(int id);
        Task CrearJugador(Jugador jugador);
        Task EditarJugador(Jugador jugador);
        Task EliminarJugador(int id);
        Task<List<Jugador>> ObtenerJugadoresPorEquipoId(int equipoId);
    }
}
