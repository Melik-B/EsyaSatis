using AppCore.Business.Models.Results;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class KategoriService : IKategoriService
    {
        public RepoBase<Kategori, EsyaSatisContext> Repo { get; set; } = new Repo<Kategori, EsyaSatisContext>();

        public IQueryable<KategoriModel> Query()
        {
            IQueryable<KategoriModel> query = Repo.Query("Esyalar").OrderBy(kategori => kategori.Adi).Select(kategori => new KategoriModel()
            {
                Id = kategori.Id,
                Adi = kategori.Adi,
                Aciklamasi = kategori.Aciklamasi,
                EsyaSayisiDisplay = kategori.Esyalar.Count
            });
            return query;
        }

        public Result Add(KategoriModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Adi))
                return new ErrorResult("Kategori boş olamaz!");
            if (model.Adi.Length > 100)
                return new ErrorResult("Kategori en fazla 100 karakter olmalıdır!");
            if (!string.IsNullOrWhiteSpace(model.Aciklamasi) && model.Aciklamasi.Length > 1000)
                return new ErrorResult("Kategori açıklaması en fazla 1000 karakter olmalıdır!");

            if (Repo.Query().Any(k => k.Adi.ToUpper() == model.Adi.ToUpper().Trim()))
                return new ErrorResult("Girdiğiniz kategori adına sahip kayıt bulunmaktadır!");

            Kategori newEntity = new Kategori()
            {
                Adi = model.Adi.Trim(),

                Aciklamasi = model.Aciklamasi?.Trim()
            };
            Repo.Add(newEntity);

            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            Kategori entity = Repo.Query(k => k.Id == id, "Esyalar").SingleOrDefault();
            if (entity.Esyalar != null && entity.Esyalar.Count > 0)
            {
                return new ErrorResult("Silinmek istediğiniz kategoriye ait ürünler bulunmaktadır!");
            }
            Repo.Delete(k => k.Id == id);
            return new SuccessResult("Kategori başarıyla silindi.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public async Task<List<KategoriModel>> KategorileriGetirAsync()
        {
            List<KategoriModel> kategoriler;

            kategoriler = await Query().ToListAsync();

            return kategoriler;
        }

        public Result Update(KategoriModel model)
        {
            if (Repo.Query().Any(k => k.Adi.ToUpper() == model.Adi.ToUpper().Trim() && k.Id != model.Id))
                return new ErrorResult("Girdiğiniz kategori adına sahip kayıt bulunmaktadır!");
            Kategori entity = Repo.Query(k => k.Id == model.Id).SingleOrDefault();
            if (entity == null)
                return new ErrorResult("Kategori kaydı bulunamadı!");
            entity.Adi = model.Adi.Trim();
            entity.Aciklamasi = model.Aciklamasi?.Trim();
            Repo.Update(entity);
            return new SuccessResult("Kategori başarıyla güncellendi.");
        }
    }
}
