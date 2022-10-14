using System.ComponentModel;

namespace Business.Models
{
    public class SepetElemanGroupByModel
    {
        public int EsyaId { get; set; }
        public int KullaniciId { get; set; }

        [DisplayName("Eşya Adı")]
        public string EsyaAdi { get; set; }

        public double ToplamEsyaBirimFiyati { get; set; }

        [DisplayName("Toplam Eşya Birim Fiyatı")]
        public string ToplamEsyaBirimFiyatiDisplay { get; set; }

        [DisplayName("Toplam Eşya Adedi")]
        public int ToplamEsyaAdedi { get; set; }
    }
}
