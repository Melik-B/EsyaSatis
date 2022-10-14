using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Magaza : RecordBase
    {
        [Required]
        [StringLength(300)]
        public string Adi { get; set; }

        public bool SanalMi { get; set; }

        [Column(TypeName = "image")]
        public byte[] Imaj { get; set; }

        [StringLength(5)]
        public string ImajDosyaUzantisi { get; set; }

        public List<EsyaMagaza> EsyaMagazalar { get; set; }
    }
}
