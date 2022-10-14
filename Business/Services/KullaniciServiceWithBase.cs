using AppCore.Business.Models.Results;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IKullaniciService : IService<KullaniciModel, Kullanici, EsyaSatisContext>
    {
        Result<List<KullaniciModel>> KullanicilariGetir();
        Result<KullaniciModel> KullaniciGetir(int id);
    }

    public class KullaniciService : IKullaniciService
    {
        public RepoBase<Kullanici, EsyaSatisContext> Repo { get; set; }

        private readonly RepoBase<KullaniciDetayi, EsyaSatisContext> _kullaniciDetayiRepo;
        private readonly RepoBase<Rol, EsyaSatisContext> _rolRepo;
        private readonly RepoBase<Ulke, EsyaSatisContext> _ulkeRepo;
        private readonly RepoBase<Sehir, EsyaSatisContext> _sehirRepo;

        private readonly RepoBase<Siparis, EsyaSatisContext> _siparisRepo;
        private readonly RepoBase<EsyaSiparis, EsyaSatisContext> _esyaSiparisRepo;

        public KullaniciService()
        {
            EsyaSatisContext EsyaSatisContext = new EsyaSatisContext();
            Repo = new Repo<Kullanici, EsyaSatisContext>(EsyaSatisContext);
            _kullaniciDetayiRepo = new Repo<KullaniciDetayi, EsyaSatisContext>(EsyaSatisContext);
            _rolRepo = new Repo<Rol, EsyaSatisContext>(EsyaSatisContext);
            _ulkeRepo = new Repo<Ulke, EsyaSatisContext>(EsyaSatisContext);
            _sehirRepo = new Repo<Sehir, EsyaSatisContext>(EsyaSatisContext);

            _siparisRepo = new Repo<Siparis, EsyaSatisContext>(EsyaSatisContext);
            _esyaSiparisRepo = new Repo<EsyaSiparis, EsyaSatisContext>(EsyaSatisContext);
        }

        public IQueryable<KullaniciModel> Query()
        {
            var kullaniciQuery = Repo.Query();
            var kullaniciDetayiQuery = _kullaniciDetayiRepo.Query();
            var rolQuery = _rolRepo.Query();
            var ulkeQuery = _ulkeRepo.Query();
            var sehirQuery = _sehirRepo.Query();

            var query = from kullanici in kullaniciQuery
                        join kullaniciDetayi in kullaniciDetayiQuery
                        on kullanici.Id equals kullaniciDetayi.KullaniciId
                        join rol in rolQuery
                        on kullanici.RolId equals rol.Id
                        join ulke in ulkeQuery
                        on kullaniciDetayi.UlkeId equals ulke.Id
                        join sehir in sehirQuery
                        on kullaniciDetayi.SehirId equals sehir.Id
                        orderby rol.Adi, kullanici.KullaniciAdi
                        select new KullaniciModel()
                        {
                            Id = kullanici.Id,
                            KullaniciAdi = kullanici.KullaniciAdi,
                            Sifre = kullanici.Sifre,
                            AktifMi = kullanici.AktifMi,
                            KullaniciDetayi = new KullaniciDetayiModel()
                            {
                                Cinsiyet = kullaniciDetayi.Cinsiyet,
                                Eposta = kullaniciDetayi.Eposta,
                                UlkeId = kullaniciDetayi.UlkeId,
                                UlkeAdiDisplay = ulke.Adi,
                                SehirId = kullaniciDetayi.SehirId,
                                SehirAdiDisplay = sehir.Adi,
                                Adres = kullaniciDetayi.Adres
                            },
                            RolId = kullanici.RolId,
                            RolAdiDisplay = rol.Adi,
                            AktifDisplay = kullanici.AktifMi ? "Evet" : "Hayır"
                        };
            return query;
        }

        public Result Add(KullaniciModel model)
        {
            if (Repo.Query().Any(k => k.KullaniciAdi.ToUpper() == model.KullaniciAdi.ToUpper().Trim()))
                return new ErrorResult("Girilen kullanıcı adına sahip kayıt bulunmaktadır!");
            if (Repo.Query("KullaniciDetayi").Any(k => k.KullaniciDetayi.Eposta.ToUpper() == model.KullaniciDetayi.Eposta.ToUpper().Trim()))
                return new ErrorResult("Girilen e-postaya sahip kayıt bulunmaktadır!");
            var entity = new Kullanici()
            {
                AktifMi = model.AktifMi,
                KullaniciAdi = model.KullaniciAdi,
                Sifre = model.Sifre,
                RolId = model.RolId.Value,
                KullaniciDetayi = new KullaniciDetayi()
                {
                    Adres = model.KullaniciDetayi.Adres.Trim(),
                    Cinsiyet = model.KullaniciDetayi.Cinsiyet,
                    Eposta = model.KullaniciDetayi.Eposta.Trim(),
                    SehirId = model.KullaniciDetayi.SehirId.Value,
                    UlkeId = model.KullaniciDetayi.UlkeId.Value
                }
            };
            Repo.Add(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            var entity = Repo.Query(k => k.Id == id, "Siparisler").SingleOrDefault();

            _kullaniciDetayiRepo.Delete(kd => kd.KullaniciId == entity.Id, false);

            List<int> siparisIdleri = entity.Siparisler.Select(s => s.Id).ToList();
            _esyaSiparisRepo.Delete(us => siparisIdleri.Contains(us.SiparisId), false);

            _siparisRepo.Delete(s => siparisIdleri.Contains(s.Id), false);

            Repo.Delete(entity);

            return new SuccessResult();
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public Result<KullaniciModel> KullaniciGetir(int id)
        {
            var kullanici = Query().SingleOrDefault(k => k.Id == id);
            if (kullanici == null)
                return new ErrorResult<KullaniciModel>("Kullanıcı bulunamadı!");
            return new SuccessResult<KullaniciModel>(kullanici);
        }

        public Result<List<KullaniciModel>> KullanicilariGetir()
        {
            var kullanicilar = Query().ToList();
            if (kullanicilar.Count == 0)
                return new ErrorResult<List<KullaniciModel>>("Kullanıcı bulunamadı!");
            return new SuccessResult<List<KullaniciModel>>(kullanicilar.Count + " kullanıcı bulundu.", kullanicilar);
        }

        public Result Update(KullaniciModel model)
        {
            if (Repo.Query().Any(k => k.KullaniciAdi.ToUpper() == model.KullaniciAdi.ToUpper().Trim() && k.Id != model.Id))
                return new ErrorResult("Girilen kullanıcı adına sahip kayıt bulunmaktadır!");
            if (Repo.Query("KullaniciDetayi").Any(k => k.KullaniciDetayi.Eposta.ToUpper() == model.KullaniciDetayi.Eposta.ToUpper().Trim() && k.Id != model.Id))
                return new ErrorResult("Girilen e-postaya sahip kayıt bulunmaktadır!");
            var entity = Repo.Query(k => k.Id == model.Id, "KullaniciDetayi").SingleOrDefault();
            entity.AktifMi = model.AktifMi;
            entity.KullaniciAdi = model.KullaniciAdi;
            entity.Sifre = model.Sifre;
            entity.RolId = model.RolId.Value;
            entity.KullaniciDetayi.Cinsiyet = model.KullaniciDetayi.Cinsiyet;
            entity.KullaniciDetayi.Adres = model.KullaniciDetayi.Adres.Trim();
            entity.KullaniciDetayi.Cinsiyet = model.KullaniciDetayi.Cinsiyet;
            entity.KullaniciDetayi.Eposta = model.KullaniciDetayi.Eposta.Trim();
            entity.KullaniciDetayi.SehirId = model.KullaniciDetayi.SehirId.Value;
            entity.KullaniciDetayi.UlkeId = model.KullaniciDetayi.UlkeId.Value;
            Repo.Update(entity);
            return new SuccessResult();
        }
    }
}
