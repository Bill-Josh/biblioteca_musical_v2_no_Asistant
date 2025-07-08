using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca_musical_v2_no_Asistant.Models
{
    /// <summary>
    /// Representa una lista de reproducción en la tabla <c>lista_reproduccion</c>.
    /// </summary>
    [Table("lista_reproduccion")]
    public class ListaReproduccion
    {
        /// <summary>
        /// Clave primaria: Identificador único de la lista.
        /// </summary>
        [Key]
        [Column("id_lista")]
        public int IdLista { get; set; }

        /// <summary>
        /// Nombre de la lista de reproducción.
        /// </summary>
        [Column("nombre"), StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

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

        /// <summary>
        /// Descripción de la lista.
        /// </summary>
        [Column("descripcion"), StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;
    }
}
