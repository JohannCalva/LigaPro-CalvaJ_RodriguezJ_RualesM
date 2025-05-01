using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LigaPro_CalvaJ_RodriguezJ_RualesM.Models;
using LigaPro_CalvaJ_RodriguezJ_RualesM.Repositories;

namespace LigaPro_CalvaJ_RodriguezJ_RualesM.Controllers
{
    public class EquiposController : Controller
    {
        private readonly EquipoRepository _repository;
        private readonly LigaProJJMContext _context;

        public EquiposController(EquipoRepository repository, LigaProJJMContext context)
        {
            _repository = repository;
            _context = context;
        }

        // GET: Equipos
        public async Task<IActionResult> Index()
        {
            var equipos = await _repository.ObtenerEquipos();
            return View(equipos);
        }

        // GET: Equipos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var equipo = await _repository.ObtenerEquipoPorId(id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // GET: Equipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Logo,Descripcion,PartidosGanados,PartidosEmpatados,PartidosPerdidos")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                await _repository.CrearEquipo(equipo);
                return RedirectToAction(nameof(Index));
            }
            return View(equipo);
        }

        // GET: Equipos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var equipo = await _repository.ObtenerEquipoPorId(id);
            if (equipo == null)
            {
                return NotFound();
            }
            return View(equipo);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Logo,Descripcion,PartidosGanados,PartidosEmpatados,PartidosPerdidos")] Equipo equipo)
        {
            if (id != equipo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    await _repository.EditarEquipo(equipo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.EquipoExiste(equipo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(equipo);
        }

        // GET: Equipos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var equipo = await _repository.ObtenerEquipoPorId(id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.EliminarEquipo(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Gastos()
        {
            var equipos = await _context.Equipo.ToListAsync();

            foreach (var equipo in equipos)
            {
                equipo.Gastos = await _context.Jugador.Where(j => j.EquipoId == equipo.Id).SumAsync(j => j.Sueldo);
            }
            var gastos = equipos
                .OrderByDescending(e => e.Gastos).Take(5).ToList();
            return View(gastos); 
        }
    }
}
