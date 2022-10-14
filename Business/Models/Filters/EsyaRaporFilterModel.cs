using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AppCore.Business.Validations;

namespace Business.Models.Filters
{
    public class EsyaRaporFilterModel
    {
        [DisplayName("Kategori")]
        public int? KategoriId { get; set; }

        [StringLength(200, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Eşya Adı")]
        public string EsyaAdi { get; set; }

        [DisplayName("Birim Fiyatı")]
        [StringDecimal(ErrorMessage = "{0} başlangıç değeri sayısal olmalıdır!")]

        public string BirimFiyatiBaslangic { get; set; }

        [DisplayName("Birim Fiyatı")]
        [StringDecimal(ErrorMessage = "{0} bitiş değeri sayısal olmalıdır!")]

        public string BirimFiyatiBitis { get; set; }

        [DisplayName("Stok Miktarı")]
        public int? StokMiktariBaslangic { get; set; }

        public int? StokMiktariBitis { get; set; }

        [DisplayName("Son Kullanma Tarihi")]
        public string SonKullanmaTarihiBaslangic { get; set; }

        public string SonKullanmaTarihiBitis { get; set; }

        [DisplayName("Mağaza")]
        public List<int> MagazaIdleri { get; set; }
    }
}
