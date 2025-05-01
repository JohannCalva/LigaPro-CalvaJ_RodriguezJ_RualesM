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
    public class JugadoresController : Controller
    {
        private readonly JugadorRepository _jugadorRepository;
        private readonly EquipoRepository _equipoRepository;
        private readonly LigaProJJMContext _context;

        public JugadoresController(JugadorRepository jugadorRepository, EquipoRepository equipoRepository, LigaProJJMContext context)
        {
            _jugadorRepository = jugadorRepository;
            _equipoRepository = equipoRepository;
            _context = context;
        }

        // GET: Jugadores
        //Dios es grande ;-;
        public async Task<IActionResult> Index(int id)
        {
            var equipos = await _context.Equipo.ToListAsync();
            equipos.Insert(0, new Equipo { Id = 0, Nombre = "Todos" });

            var jugadores = await _context.Jugador.ToListAsync();
            if (id != 0)
            {
                jugadores = jugadores.Where(j => j.EquipoId == id).ToList();
            }

            ViewBag.EquipoId = new SelectList(equipos, "Id", "Nombre");
            return View(jugadores);
        }
        
        // GET: Jugadores/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var jugador = await _jugadorRepository.ObtenerJugadorPorId(id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // GET: Jugadores/Create
        public async Task<IActionResult> Create()
        {
            var equipos = await _equipoRepository.ObtenerEquipos();
            ViewData["EquipoId"] = new SelectList(equipos, "Id", "Nombre");
            return View();
        }
        // POST: Jugadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Edad,NumeroCamiseta,Posicion,Goles,Asistencias,Sueldo,EquipoId")] Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                await _jugadorRepository.CrearJugador(jugador);
                return RedirectToAction(nameof(Index));
            }
            var equipos = await _equipoRepository.ObtenerEquipos();
            ViewData["EquipoId"] = new SelectList(equipos, "Id", "Nombre", jugador.EquipoId);
            return View(jugador);
        }

        // GET: Jugadores/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var jugador = await _jugadorRepository.ObtenerJugadorPorId(id);
            if (jugador == null)
            {
                return NotFound();
            }
            var equipos = await _equipoRepository.ObtenerEquipos();
            ViewData["EquipoId"] = new SelectList(equipos, "Id", "Nombre", jugador.EquipoId);
            return View(jugador);
        }

        // POST: Jugadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Edad,NumeroCamiseta,Posicion,Goles,Asistencias,Sueldo,EquipoId")] Jugador jugador)
        {
            if (id != jugador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _jugadorRepository.EditarJugador(jugador);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _jugadorRepository.JugadorExiste(jugador.Id))
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
            var equipos = await _equipoRepository.ObtenerEquipos();
            ViewData["EquipoId"] = new SelectList(equipos, "Id", "Nombre", jugador.EquipoId);
            return View(jugador);
        }

        // GET: Jugadores/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var jugador = await _jugadorRepository.ObtenerJugadorPorId(id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // POST: Jugadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _jugadorRepository.EliminarJugador(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
