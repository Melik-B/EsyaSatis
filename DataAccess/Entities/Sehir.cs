using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Sehir : RecordBase
    {
        [Required, StringLength(150)]
        public string Adi { get; set; }

        public int UlkeId { get; set; }
        public Ulke Ulke { get; set; }

        public List<KullaniciDetayi> KullaniciDetaylari { get; set; }
    }
}
