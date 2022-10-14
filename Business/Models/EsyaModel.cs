using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Business.Models
{
    public class EsyaModel : RecordBase
    {
        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(200, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [StringLength(500, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Açıklaması")]
        public string Aciklamasi { get; set; }

        [Range(0, 1000000000, ErrorMessage = "{0} {1} ile {2} aralığında olmalıdır!")]
        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Birim Fiyatı")]
        public double? BirimFiyati { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} {1} ile {2} aralığında olmalıdır!")]
        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Stok Miktarı")]
        public int? StokMiktari { get; set; }

        [DisplayName("Son Kullanma Tarihi")]
        public DateTime? SonKullanmaTarihi { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} gereklidir!")]
        public int? KategoriId { get; set; }

        [StringLength(5)]
        public string ImajDosyaUzantisi { get; set; }

        [DisplayName("Kategori")]
        public string KategoriAdiDisplay { get; set; }

        [DisplayName("Birim Fiyatı")]
        public string BirimFiyatiDisplay { get; set; }

        [DisplayName("Son Kullanma Tarihi")]
        public string SonKullanmaTarihiDisplay { get; set; }

        public bool SepeteEklendiMi { get; set; } = false;

        [DisplayName("Mağaza")]
        public List<int> MagazaIdleri { get; set; }

        [DisplayName("Mağaza")]
        public List<string> MagazalarDisplay { get; set; }

        [DisplayName("İmaj")]
        public string ImajDosyaYoluDisplay { get; set; }
    }
}
