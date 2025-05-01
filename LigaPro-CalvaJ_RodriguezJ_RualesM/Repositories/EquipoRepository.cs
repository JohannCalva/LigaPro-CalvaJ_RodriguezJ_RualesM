using LigaPro_CalvaJ_RodriguezJ_RualesM.Interfaces;
using LigaPro_CalvaJ_RodriguezJ_RualesM.Models;
using Microsoft.EntityFrameworkCore;

namespace LigaPro_CalvaJ_RodriguezJ_RualesM.Repositories
{
    public class EquipoRepository : IEquipoRepository
    {
        private readonly LigaProJJMContext _context;

        public EquipoRepository(LigaProJJMContext context)
        {
            _context = context;
        }
        public async Task<bool> EquipoExiste(int id)
        {
            return await _context.Equipo.AnyAsync(e => e.Id == id);
        }
        public async Task<List<Equipo>> ObtenerEquipos()
        {
            var lista = await _context.Equipo.ToListAsync();
            var listaEnOrden = lista.OrderByDescending(e => e.Puntos).ToList();
            return listaEnOrden;
        }

        public async Task<Equipo?> ObtenerEquipoPorId(int id)
        {
            return await _context.Equipo.FindAsync(id);
        }
        public async Task CrearEquipo(Equipo equipo)
        {
            _context.Equipo.Add(equipo);
            await _context.SaveChangesAsync();
        }
        public async Task EditarEquipo(Equipo equipo)
        {
            _context.Equipo.Update(equipo);
            await _context.SaveChangesAsync();
        }
        public async Task EliminarEquipo(int id)
        {
            var equipo = await _context.Equipo.FindAsync(id);
            if (equipo != null)
            {
                _context.Equipo.Remove(equipo);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Jugador>> ObtenerJugadoresPorEquipoId(int equipoId)
        {
            return await _context.Jugador.Where(j => j.EquipoId == equipoId).ToListAsync();
        }
    }
}
