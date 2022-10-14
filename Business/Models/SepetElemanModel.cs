using System.ComponentModel;

namespace Business.Models
{
    public class SepetElemanModel
    {
        public int EsyaId { get; set; }
        public int KullaniciId { get; set; }

        [DisplayName("Eşya Adı")]
        public string EsyaAdi { get; set; }

        [DisplayName("Eşya Birim Fiyatı")]
        public double EsyaBirimFiyati { get; set; }
    }
}
