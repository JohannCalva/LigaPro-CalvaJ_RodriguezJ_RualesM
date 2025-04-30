using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaPro_CalvaJ_RodriguezJ_RualesM.Models
{
    public class Jugador
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [Range(16, 40, ErrorMessage ="La edad del jugador debe estar entre 16 y 40.")]
        public int Edad { get; set; }
        [Required]
        [Range(1, 99, ErrorMessage ="El numero de camiseta debe estar entre 1 y 99.")]
        public int NumeroCamiseta { get; set; }
        [Required]
        [MaxLength(20)]
        public string Posicion { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Los goles deben ser igual o mayor a 0.")]
        public int Goles { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Las asistencias deben ser igual o mayor a 0.")]
        public int Asistencias { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Sueldo { get; set; }
        [Required]
        public int EquipoId { get; set; }
        [ForeignKey("EquipoId")]
        public Equipo? Equipo { get; set; }
    }
}
