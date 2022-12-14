using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Business.Models
{
    public class UlkeModel : RecordBase
    {
        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(100, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Ülke Adı")]
        public string Adi { get; set; }
    }
}
