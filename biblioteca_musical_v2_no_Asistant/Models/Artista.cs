using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca_musical_v2_no_Asistant.Models
{
    /// <summary>
    /// Representa un artista en la tabla <c>artista</c> de la base de datos.
    /// </summary>
    [Table("artista")]
    public class Artista
    {
        /// <summary>
        /// Clave primaria: Identificador único del artista.
        /// </summary>
        [Key]
        [Column("id_artista")]
        public int IdArtista { get; set; }

        /// <summary>
        /// Nombre de pila del artista.
        /// </summary>
        [Column("nombre"), StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Apellido del artista.
        /// </summary>
        [Column("apellido"), StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        /// <summary>
        /// Nombre artístico o de escena.
        /// </summary>
        [Column("nombre_artistico"), StringLength(100)]
        public string NombreArtistico { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de artista (por ejemplo, Solista, Banda).
        /// </summary>
        [Column("tipo_artista"), StringLength(50)]
        public string TipoArtista { get; set; } = string.Empty;

        /// <summary>
        /// País de origen.
        /// </summary>
        [Column("pais_origen"), StringLength(50)]
        public string PaisOrigen { get; set; } = string.Empty;

        /// <summary>
        /// Biografía o descripción extensa del artista.
        /// </summary>
        [Column("biografia")]
        public string Biografia { get; set; } = string.Empty;

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
