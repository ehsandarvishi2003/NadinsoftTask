using NadinsoftTask.Models.DataBase;
using NadinsoftTask.Models.Entity;
using NadinsoftTask.Models.ViewModels;

namespace NadinsoftTask.Models.Repository
{
    public class ProductRepository
    {
        #region ctor

        private readonly DatabaseContext _context;
        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        #endregion

        #region Get All
        /// <summary>
        /// دریافت تمامی محصولات
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProducts() 
        { 
            return _context.Products.ToList();
        }

        #endregion

        #region Get by Id
        /// <summary>
        /// دریافت محصول به کمک آدی
        /// </summary>
        /// <param name="Id">آیدی محصول</param>
        /// <returns></returns>
        public Product GetById(int Id)
        {
            Product product = _context.Products.SingleOrDefault(p => p.Id == Id);
            return product;
        }

        #endregion

        #region Add{Post}
        /// <summary>
        /// ایجاد محصول
        /// </summary>
        /// <param name="product">اطلاعات محصول</param>
        /// <returns></returns>
        public Product Add(ProductViewModel product)
        {
            var model = new Product()
            {
                Name = product.Name,
                ProductDate = DateTime.Now,
                ManufacturePhone =product.ManufacturePhone,
                ManufactureEmail=product.ManufactureEmail,
                IsAvailable=product.IsAvailable
            };
            var Result = _context.Products.Add(model);
            _context.SaveChanges();
            return model;
        }

        #endregion

        #region Edite{Pot}
        /// <summary>
        /// ویرایش اطلاعات محصول
        /// </summary>
        /// <param name="product">آیدی محصول</param>
        /// <returns></returns>
        public bool Edit(Product product)
        {
            if(product != null)
            {
                if (product.Id != null)
                {
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;

            //میشه با استفاده از متد get چک کرد که اگه همچنین entity و جود نداشت اون رو ایجاد کرد
        }

        #endregion

        #region Delete
        /// <summary>
        /// حذف محصول
        /// </summary>
        /// <param name="id">آیدی محصول</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            if(id != null)
            {
                var result = GetById(id);
                if (result != null)
                {
                    _context.Products.Remove(result);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        #endregion

    }
}
