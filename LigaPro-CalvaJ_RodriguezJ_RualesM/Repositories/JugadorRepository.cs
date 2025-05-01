using LigaPro_CalvaJ_RodriguezJ_RualesM.Interfaces;
using LigaPro_CalvaJ_RodriguezJ_RualesM.Models;
using Microsoft.EntityFrameworkCore;

namespace LigaPro_CalvaJ_RodriguezJ_RualesM.Repositories
{
    public class JugadorRepository : IJugadorRepository
    {
        private readonly LigaProJJMContext _context;

        public JugadorRepository(LigaProJJMContext context)
        {
            _context = context;
        }
        public async Task<bool> JugadorExiste(int id)
        {
            return await _context.Jugador.AnyAsync(j => j.Id == id);
        }
        public async Task<List<Jugador>> ObtenerJugadores()
        {
            return await _context.Jugador.Include(j => j.Equipo).ToListAsync();
        }

        public async Task<Jugador?> ObtenerJugadorPorId(int id)
        {
            return await _context.Jugador.Include(j => j.Equipo).FirstOrDefaultAsync(j => j.Id == id);
        }
        public async Task CrearJugador(Jugador jugador)
        {
            _context.Jugador.Add(jugador);
            await _context.SaveChangesAsync();
        }
        public async Task EditarJugador(Jugador jugador)
        {
            _context.Jugador.Update(jugador);
            await _context.SaveChangesAsync();
        }
        public async Task EliminarJugador(int id)
        {
            var jugador = await _context.Jugador.FindAsync(id);
            if (jugador != null)
            {
                _context.Jugador.Remove(jugador);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Jugador>> ObtenerJugadoresPorEquipoId(int equipoId)
        {
            return await _context.Jugador.Include(j => j.Equipo).Where(j => j.EquipoId == equipoId).ToListAsync();
        }


    }
}
