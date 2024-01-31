using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingListApp.Contracts;
using ShoppingListApp.Data;
using ShoppingListApp.Data.Models;
using ShoppingListApp.Models;

namespace ShoppingListApp.Services
{
    public class ProductService : IProductService
    {
        private readonly ShoppingListDbContext context;
        public ProductService(ShoppingListDbContext _context)
        {
            context = _context;
        }
        public async Task AddProductAsync(ProductViewModel model)
        {
            var entity = new Product { Name = model.Name };
            context.Products.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var entity = await context.Products.FindAsync(id);
            if (entity is null)
            {
                throw new ArgumentException("Invalid product");
            }
            context.Products.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var result = await context.Products
                .AsNoTracking()
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToListAsync();
            return result;

        }

        public async Task<ProductViewModel> TakeByIdAsync(int id)
        {
            var entity = await context.Products.FindAsync(id);
            if (entity is null)
            {
                throw new ArgumentException("Invalid product");
            }
            return new ProductViewModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task UpdateProductAsync(ProductViewModel model)
        {
            var entity = await context.Products.FindAsync(model.Id);
            if (entity is null)
            {
                throw new ArgumentException("Invalid product");
            }
            entity.Name = model.Name;
            await context.SaveChangesAsync();
        }
    }
}
