using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Esya : RecordBase
    {
        [Required]
        [StringLength(200)]
        public string Adi { get; set; }

        [StringLength(500)]
        public string Aciklamasi { get; set; }

        public double BirimFiyati { get; set; }

        public int StokMiktari { get; set; }

        public DateTime? SonKullanmaTarihi { get; set; }

        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }

        [StringLength(5)]
        public string ImajDosyaUzantisi { get; set; }

        public List<EsyaMagaza> EsyaMagazalar { get; set; }

        public List<EsyaSiparis> EsyaSiparisler { get; set; }
    }
}
