using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Business.Models
{
    public class KategoriModel : RecordBase
    {
        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(100, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [DisplayName("Açıklaması")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [MaxLength(4000, ErrorMessage = "{0} en çok {1} karakter olmalıdır!")]
        public string Aciklamasi { get; set; }

        [DisplayName("Eşya Sayısı")]
        public int EsyaSayisiDisplay { get; set; }
    }
}
