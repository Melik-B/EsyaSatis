using System.ComponentModel;

namespace Business.Models.Reports
{
    public class EsyaRaporModel
    {
        public int? KategoriId { get; set; }

        [DisplayName("Kategori")]
        public string KategoriAdi { get; set; }

        public string KategoriAciklamasi { get; set; }

        [DisplayName("Eşya")]
        public string EsyaAdi { get; set; }

        public string EsyaAciklamasi { get; set; }

        [DisplayName("Birim Fiyatı")]
        public string EsyaBirimFiyatiDisplay { get; set; }

        [DisplayName("Stok Miktarı")]
        public int EsyaStokMiktari { get; set; }

        [DisplayName("Son Kullanma Tarihi")]
        public string EsyaSonKullanmaTarihiDisplay { get; set; }

        public int? MagazaId { get; set; }

        [DisplayName("Mağaza")]
        public string MagazaAdi { get; set; }

        public double EsyaBirimFiyati { get; set; }

        public DateTime? EsyaSonKullanmaTarihi { get; set; }
    }
}
