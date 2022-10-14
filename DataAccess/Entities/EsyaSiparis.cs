using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class EsyaSiparis
    {
        [Key]
        [Column(Order = 0)]
        public int EsyaId { get; set; }

        public Esya Esya { get; set; }

        [Key]
        [Column(Order = 1)]
        public int SiparisId { get; set; }

        public Siparis Siparis { get; set; }

        public int EsyaAdedi { get; set; }
    }
}
