using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca_musical_v2_no_Asistant.Models
{
    /// <summary>
    /// Representa la relación entre una lista de reproducción y una canción en la tabla <c>lista_reproduccion_cancion</c>.
    /// </summary>
    [Table("lista_reproduccion_cancion")]
    public class ListaReproduccionCancion
    {
        /// <summary>
        /// Clave primaria: Identificador único de la relación.
        /// </summary>
        [Key]
        [Column("id_lista_rep_can")]
        public int IdListaRepCan { get; set; }

        /// <summary>
        /// Clave foránea a la lista de reproducción.
        /// </summary>
        [Column("id_lista")]
        public int? IdLista { get; set; }

        /// <summary>
        /// Navegación a la lista de reproducción.
        /// </summary>
        public ListaReproduccion? Lista { get; set; }

        /// <summary>
        /// Clave foránea a la canción.
        /// </summary>
        [Column("id_cancion")]
        public int? IdCancion { get; set; }

        /// <summary>
        /// Navegación a la canción.
        /// </summary>
        public Cancion? Cancion { get; set; }
    }
}
