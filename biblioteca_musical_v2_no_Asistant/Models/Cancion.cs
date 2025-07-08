using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca_musical_v2_no_Asistant.Models
{
    /// <summary>
    /// Representa una canción en la tabla <c>cancion</c>.
    /// </summary>
    [Table("cancion")]
    public class Cancion
    {
        /// <summary>
        /// Clave primaria: Identificador único de la canción.
        /// </summary>
        [Key]
        [Column("id_cancion")]
        public int IdCancion { get; set; }

        /// <summary>
        /// Título de la canción.
        /// </summary>
        [Column("titulo"), StringLength(50)]
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Duración de la canción en segundos.
        /// </summary>
        [Column("duracion_cancion")]
        public int? DuracionCancion { get; set; }

        /// <summary>
        /// Clave foránea al género.
        /// </summary>
        [Column("id_genero")]
        public int? IdGenero { get; set; }

        /// <summary>
        /// Navegación al género.
        /// </summary>
        public Genero? IdGeneroNavigation { get; set; }

        /// <summary>
        /// Clave foránea al artista.
        /// </summary>
        [Column("id_artista")]
        public int? IdArtista { get; set; }

        /// <summary>
        /// Navegación al artista.
        /// </summary>
        public Artista? IdArtistaNavigation { get; set; }

        /// <summary>
        /// Año de lanzamiento de la canción.
        /// </summary>
        [Column("anio_lanzamiento")]
        public int? AnioLanzamiento { get; set; }

        /// <summary>
        /// Estado de la canción (por ejemplo, Publicada, Borrador).
        /// </summary>
        [Column("estado"), StringLength(50)]
        public string Estado { get; set; } = string.Empty;

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
