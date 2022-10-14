using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AppCore.Records.Bases;

namespace Business.Models
{
    public class MagazaModel : RecordBase
    {
        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(300, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [DisplayName("Sanal")]
        public bool SanalMi { get; set; }

        [DisplayName("İmaj")]
        public byte[] Imaj { get; set; }

        [StringLength(5)]
        public string ImajDosyaUzantisi { get; set; }

        [DisplayName("Sanal")]
        public string SanalMiDisplay { get; set; }

        [DisplayName("İmaj")]
        public string ImajSrcDisplay { get; set; }
    }
}