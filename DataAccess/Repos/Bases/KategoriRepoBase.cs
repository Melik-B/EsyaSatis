using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Repos.Bases
{
    public class KategoriRepoBase : RepoBase<Kategori, EsyaSatisContext>
    {
        protected KategoriRepoBase() : base()
        {

        }

        protected KategoriRepoBase(EsyaSatisContext EsyaSatisContext) : base(EsyaSatisContext)
        {

        }
    }
}
