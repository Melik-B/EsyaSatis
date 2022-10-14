using AppCore.Business.Models.Results;
using Business.Enums;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IHesapService
    {
        Result<KullaniciModel> Giris(KullaniciGirisModel model);
        Result Kayit(KullaniciKayitModel model);
    }

    public class HesapService : IHesapService
    {
        private readonly IKullaniciServiceWithBase _kullaniciService;

        public HesapService(IKullaniciServiceWithBase kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        public Result<KullaniciModel> Giris(KullaniciGirisModel model)
        {
            KullaniciModel kullanici = _kullaniciService.Query().SingleOrDefault(k => k.KullaniciAdi == model.KullaniciAdi && k.Sifre == model.Sifre && k.AktifMi);
            if (kullanici == null)
                return new ErrorResult<KullaniciModel>("Geçersiz kullanıcı adı ve şifre!");
            return new SuccessResult<KullaniciModel>(kullanici);
        }

        public Result Kayit(KullaniciKayitModel model)
        {
            var kullanici = new KullaniciModel()
            {
                AktifMi = true,

                RolId = (int)Rol.Kullanıcı,

                KullaniciAdi = model.KullaniciAdi,
                Sifre = model.Sifre,
                KullaniciDetayi = new KullaniciDetayiModel()
                {
                    Adres = model.KullaniciDetayi.Adres.Trim(),
                    Cinsiyet = model.KullaniciDetayi.Cinsiyet,
                    Eposta = model.KullaniciDetayi.Eposta.Trim(),
                    SehirId = model.KullaniciDetayi.SehirId,
                    UlkeId = model.KullaniciDetayi.UlkeId
                }
            };
            return _kullaniciService.Add(kullanici);
        }
    }
}
