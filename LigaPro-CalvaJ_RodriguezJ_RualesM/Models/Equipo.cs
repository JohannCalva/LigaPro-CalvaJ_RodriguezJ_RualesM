using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaPro_CalvaJ_RodriguezJ_RualesM.Models
{
    public class Equipo
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [DisplayName("Nombre del equipo")]
        [Required]
        public string Nombre { get; set; }
        [DisplayName("Logo")]
        public string? Logo { get; set; }   
        public string? Descripcion { get; set; }
        [NotMapped]
        [DisplayName("Partidos Jugados")]
        public int? PartidosJugados
        {
            get
            {
                int partidosJugados = PartidosGanados + PartidosEmpatados + PartidosPerdidos;
                return partidosJugados;
            }
        }
        //Para mayor practicidad y no tener que hacer diversas validaciones, los partidos jugados no podran ser ingresados por el usuario y serán la suma de los demás partidos
        [DisplayName("Partidos Ganados")]
        public int PartidosGanados { get; set; }
        [DisplayName("Partidos Empatados")]
        public int PartidosEmpatados { get; set; }
        [DisplayName("Partidos Perdidos")]
        public int PartidosPerdidos { get; set; }
        [DisplayName("Puntos")]
        public int? Puntos
        {
            get
            {
                int puntos = PartidosGanados * 3 + PartidosEmpatados;
                return puntos;
            }
        }
    }
}
