using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca_musical_v2_no_Asistant.Models
{
    /// <summary>
    /// Representa un género en la tabla <c>genero</c>.
    /// </summary>
    [Table("genero")]
    public class Genero
    {
        /// <summary>
        /// Clave primaria: Identificador único del género.
        /// </summary>
        [Key]
        [Column("id_genero")]
        public int IdGenero { get; set; }

        /// <summary>
        /// Nombre del género.
        /// </summary>
        [Column("nombre"), StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Descripción del género.
        /// </summary>
        [Column("descripcion"), StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de creación del registro.
        /// </summary>
        [Column("fecha_creacion")]
        public DateTime? FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de la última modificación.
        /// </summary>
        [Column("fecha_modificacion")]
        public DateTime? FechaModificacion { get; set; }
    }
}
