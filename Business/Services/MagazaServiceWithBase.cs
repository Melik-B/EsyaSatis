using AppCore.Business.Models.Results;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IMagazaService : IService<MagazaModel, Magaza, EsyaSatisContext>
    {
        Result DeleteImage(int id);
    }

    public class MagazaService : IMagazaService
    {
        public RepoBase<Magaza, EsyaSatisContext> Repo { get; set; }

        private readonly RepoBase<EsyaMagaza, EsyaSatisContext> _esyaMagazaRepo;

        public MagazaService()
        {
            EsyaSatisContext EsyaSatisContext = new EsyaSatisContext();
            Repo = new Repo<Magaza, EsyaSatisContext>(EsyaSatisContext);
            _esyaMagazaRepo = new Repo<EsyaMagaza, EsyaSatisContext>(EsyaSatisContext);
        }

        public IQueryable<MagazaModel> Query()
        {
            return Repo.Query().OrderBy(m => m.Adi).Select(m => new MagazaModel()
            {
                Id = m.Id,
                Adi = m.Adi,
                SanalMi = m.SanalMi,
                SanalMiDisplay = m.SanalMi ? "Evet" : "Hayır",

                Imaj = m.Imaj,
                ImajSrcDisplay = m.Imaj != null ? (m.ImajDosyaUzantisi == ".jpg" || m.ImajDosyaUzantisi == ".jpeg" ? "data:image/jpeg;base64," : "data:image/png;base64,") + Convert.ToBase64String(m.Imaj) : null,
                ImajDosyaUzantisi = m.ImajDosyaUzantisi
            });
        }

        public Result Add(MagazaModel model)
        {
            if (Repo.Query().Any(m => m.Adi.ToUpper() == model.Adi.ToUpper().Trim()))
                return new ErrorResult("Girdiğiniz mağaza adına sahip kayıt bulunmaktadır!");
            Magaza entity = new Magaza()
            {
                Adi = model.Adi.Trim(),
                SanalMi = model.SanalMi,

                Imaj = model.Imaj,
                ImajDosyaUzantisi = model.ImajDosyaUzantisi?.ToLower()
            };
            Repo.Add(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            _esyaMagazaRepo.Delete(em => em.MagazaId == id, false);
            Repo.Delete(m => m.Id == id);
            return new SuccessResult();
        }

        public Result DeleteImage(int id)
        {
            Magaza entity = Repo.Query(m => m.Id == id).SingleOrDefault();
            entity.Imaj = null;
            entity.ImajDosyaUzantisi = null;
            Repo.Update(entity);
            return new SuccessResult();
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public Result Update(MagazaModel model)
        {
            if (Repo.Query().Any(m => m.Adi.ToUpper() == model.Adi.ToUpper().Trim() && m.Id != model.Id))
                return new ErrorResult("Girdiğiniz mağaza adına sahip kayıt bulunmaktadır!");
            Magaza entity = Repo.Query(m => m.Id == model.Id).SingleOrDefault();
            entity.Adi = model.Adi.Trim();
            entity.SanalMi = model.SanalMi;

            if (model.Imaj != null && model.Imaj.Length > 0)
            {
                entity.Imaj = model.Imaj;
                entity.ImajDosyaUzantisi = model.ImajDosyaUzantisi.ToLower();
            }

            Repo.Update(entity);
            return new SuccessResult();
        }
    }
}
