using AppCore.Records.Bases;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Siparis : RecordBase
    {
        public DateTime Tarih { get; set; }
        public SiparisDurum Durum { get; set; }
        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }
        public List<EsyaSiparis> EsyaSiparisler { get; set; }
    }
}
