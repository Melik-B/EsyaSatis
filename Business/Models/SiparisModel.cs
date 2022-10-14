using AppCore.Records.Bases;
using DataAccess.Enums;
using System.ComponentModel;

namespace Business.Models
{
    public class SiparisModel : RecordBase
    {
        public DateTime Tarih { get; set; }

        public SiparisDurum Durum { get; set; }

        public int KullaniciId { get; set; }
        public KullaniciModel Kullanici { get; set; }

        public List<EsyaSiparisModel> EsyaSiparisler { get; set; }

        [DisplayName("Sipariş No")]
        public string SiparisNo { get; set; }

        [DisplayName("Sipariş Tarihi")]
        public string TarihDisplay { get; set; }

        public EsyaSiparisModel EsyaSiparisJoin { get; set; }

        public string SiparisColor { get; set; }

        public double ToplamEsyaBirimFiyati { get; set; }

        [DisplayName("Toplam Eşya Birim Fiyatı")]
        public string ToplamEsyaBirimFiyatiDisplay { get; set; }
    }
}
