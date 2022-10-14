using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class EsyaMagaza
    {
        [Key]
        [Column(Order = 0)]
        public int EsyaId { get; set; }

        public Esya Esya { get; set; }

        [Key]
        [Column(Order = 1)]
        public int MagazaId { get; set; }

        public Magaza Magaza { get; set; }
    }
}
