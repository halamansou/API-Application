
using Electronic_E_commerce_Website_API.Models;
using Electronic_E_commerce_Website_API.Repository;

namespace Electronic_E_commerce_Website_API.UnitOfWork
{
    public class UnitOfWork
    {
        private readonly ECommerceApiContext _context;
        private GenericRepository<Cart> _cartRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<User> _userRepository;

        public UnitOfWork(ECommerceApiContext context)
        {
            _context = context;
        }

        public GenericRepository<Cart> CartRepository
        {
            get
            {
                if (_cartRepository == null)
                {
                    _cartRepository = new GenericRepository<Cart>(_context);
                }
                return _cartRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new GenericRepository<Product>(_context);
                }
                return _productRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
