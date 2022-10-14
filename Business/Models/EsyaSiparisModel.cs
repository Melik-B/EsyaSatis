using AppCore.Records.Bases;
using System.ComponentModel;

namespace Business.Models
{
    public class EsyaSiparisModel
    {
        public int EsyaId { get; set; }

        public EsyaModel Esya { get; set; }

        public int SiparisId { get; set; }

        public SiparisModel Siparis { get; set; }

        [DisplayName("Adet")]
        public int EsyaAdedi { get; set; }
    }
}
