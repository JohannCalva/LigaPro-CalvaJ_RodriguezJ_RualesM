using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LigaPro_CalvaJ_RodriguezJ_RualesM.Models;

    public class LigaProJJMContext : DbContext
    {
        public LigaProJJMContext (DbContextOptions<LigaProJJMContext> options)
            : base(options)
        {
        }

        public DbSet<LigaPro_CalvaJ_RodriguezJ_RualesM.Models.Equipo> Equipo { get; set; } = default!;

public DbSet<LigaPro_CalvaJ_RodriguezJ_RualesM.Models.Jugador> Jugador { get; set; } = default!;
    }
