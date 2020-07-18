using Models.EFrame;
using PagedList;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
namespace Models.DAO
{
    public class ProductDAO
    {
        public ComesticEntities dbContext = null;
        public ProductDAO()
        {
            dbContext = new ComesticEntities();
        }
        public IEnumerable<Product> PageList(int? currentPage, int pageSize)/*PageModel pageModel*/
        {
            #region Pagelist
            /*var listnew = new List<Product>();
            var sizePage = dbContext.Products.Count();
            var listPr = dbContext.Products.ToList();
            var shapeSize = sizePage % pageModel.pageSize;
            if(sizePage > pageModel.pageSize)
            {
                if (shapeSize != 0)
                {
                    pageModel.firstPage = (pageModel.pageSize * pageModel.currentPage + 1) - pageModel.pageSize;
                    pageModel.lastPage = pageModel.pageSize * pageModel.currentPage;
                    shapeSize += 1;
                    pageModel.numberPage = shapeSize;
                }
                for (int i = pageModel.firstPage; i <= pageModel.lastPage; i++)
                {
                    listnew += listPr[i - 1].ToString();
                }
                return listnew;
            }*/
            #endregion
            return dbContext.Products.OrderByDescending(x => x.dateUpdate).ToPagedList(currentPage ?? 1, pageSize);
        }
        public List<Product> ListTop(int popularPR)
        {
            var listTop = (from list in dbContext.Products
                           where list.status == 1
                           orderby list.prId descending
                           select list).Take(popularPR);
            return listTop.ToList();
        }
        public List<Product> ListPR()
        {
            return dbContext.Products.ToList();
        }
        public Product FindProduct(long prId)
        {
            return dbContext.Products.Find(prId);
        }
        public List<Product> ProductCategoryDifferent(long productId)
        {
            var Id = dbContext.Products.Find(productId);
            return dbContext.Products.Where(x => x.prId != productId && x.cateId == Id.cateId).ToList();
        }
        public List<Product> ProductCategory(long productId)
        {
            var Id = dbContext.Products.Find(productId);
            return dbContext.Products.Where(x => x.cateId == Id.cateId).ToList();
        }
        public Product AddPr(Product product)
        {
            var addpr = dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var pr = dbContext.Products.LastOrDefault();
            if (addpr != null)
                return pr;
            return null;
        }
        public Product UpdateProduct(Product product)
        {
            var updatepr = dbContext.Products.SingleOrDefault(x => x.prId == product.prId);
            if(updatepr != null)
            {
                try
                {
                    updatepr.prName = product.prName;
                    updatepr.cateId = product.cateId;
                    updatepr.images = product.images;
                    updatepr.description = product.description;
                    updatepr.price = product.price;
                    updatepr.amount = product.amount;
                    dbContext.SaveChanges();
                    return updatepr;
                }
                catch
                {
                }
            }
            return updatepr;
        }

        public IEnumerable<Product> Search(string keyword,int? currentPage, int pageSize = 5)
        {
            return dbContext.Products.OrderByDescending(x => x.dateUpdate).Where(x => x.prName.Contains(keyword)).ToPagedList(currentPage ?? 1, pageSize);
        }
    }
}
