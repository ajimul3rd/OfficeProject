using System.Security.Claims;
using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public class ProductsService : IProductsService
    {


        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        public ProductsService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;

        }
        //public async Task AddOrUpdateProductAsync(ProductsDTO productsDTO)
        //{
        //    using (var context = dbContextFactory.CreateDbContext())
        //    {
        //        if (productsDTO != null)
        //        {
        //            if (productsDTO.ProductsId != 0)
        //            {
        //                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

        //                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        //                {
        //                    throw new UnauthorizedAccessException("User not authenticated or invalid user ID");
        //                }
        //                productsDTO.UserId = userId;
        //                context.Products.Update(Mapper.Map<Products>(productsDTO));
        //            }
        //            else
        //            {
        //                await context.Products.AddAsync(Mapper.Map<Products>(productsDTO));
        //            }

        //            await context.SaveChangesAsync();
        //        }
        //    }
        //}


        public async Task AddOrUpdateProductAsync(ProductsDTO productsDTO)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                if (productsDTO != null)
                {
                    // Check for uniqueness
                    var aliasExists = await context.Products
                        .AnyAsync(p => p.ProductsAlias == productsDTO.ProductsAlias &&
                                      p.ProductsId != productsDTO.ProductsId);

                    if (aliasExists)
                        throw new InvalidOperationException("Product alias must be unique");

                    var nameExists = await context.Products
                        .AnyAsync(p => p.ProductsName == productsDTO.ProductsName &&
                                      p.ProductsId != productsDTO.ProductsId);

                    if (nameExists)
                        throw new InvalidOperationException("Product name must be unique");

                    var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                    if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                        throw new UnauthorizedAccessException("User not authenticated or invalid user ID");

                    productsDTO.UserId = userId;

                    if (productsDTO.ProductsId != 0)
                    {
                        context.Products.Update(Mapper.Map<Products>(productsDTO));
                    }
                    else
                    {
                        await context.Products.AddAsync(Mapper.Map<Products>(productsDTO));
                    }

                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<ProductsDTO?> GetProductsDTOById(int id)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                var products = await context.Products.FindAsync(id);
                return Mapper.Map<ProductsDTO>(products);
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                var products = await context.Products.FindAsync(id);
                if (products == null)
                {
                    // User not found
                    return false;
                }

                context.Products.Remove(products);
                await context.SaveChangesAsync();
                return true;
            }
        }


        public async Task<List<ProductsDTO>> GetAllProductsDTOAsync()
        {
            try
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    throw new UnauthorizedAccessException("User not authenticated or invalid user ID");
                }

                using (var context = dbContextFactory.CreateDbContext())
                {
                    var products = await context.Products.ToListAsync();

                   

                    return Mapper.Map<List<ProductsDTO>>(products);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving clients: {ex.Message}");
                throw;
            }
        }

    }
}
