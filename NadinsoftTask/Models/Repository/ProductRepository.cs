using NadinsoftTask.Models.DataBase;

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
        public List<Product> GetAllProducts() 
        { 
            return _context.Products.ToList();
        }

        #endregion

        #region Get by Id

        public Product GetById(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            return product;
        }

        #endregion

    }
}
