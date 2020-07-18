using Models.EFrame;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Models.DAO
{
    public class CategoryDAO
    {
        public ComesticEntities dbContext = null;
        public CategoryDAO()
        {
            dbContext = new ComesticEntities();
        }
        public IEnumerable<Category> PageListCategory(int? currentPage, int pageSize)
        {
            return dbContext.Categories.OrderBy(x => x.cateId).ToPagedList(currentPage ?? 1, pageSize);
        }
        public List<Category> ListAll()
        {
            return dbContext.Categories.ToList();
        }
        public Category FindCategory(long cateid)
        {
            return dbContext.Categories.Find(cateid);
        }
        public Category UpdateLSP(Category category)
        {
            var Lsp = dbContext.Categories.SingleOrDefault(x => x.cateId == category.cateId);
            if (Lsp != null)
            {
                Lsp.cateImage = category.cateImage;
                Lsp.cateName = category.cateName;
                dbContext.SaveChanges();
            }
            return Lsp;
        }
        public int Delete(int id)
        {
            var del = dbContext.Categories.SingleOrDefault(x => x.cateId == id);
            if (del.status == 1)
            {
                del.status = 0;
            }
            else
            {
                del.status = 1;
            }
            dbContext.SaveChanges();
            return 1;
        }
    }
}
