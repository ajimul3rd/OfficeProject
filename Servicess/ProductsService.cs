using System.Security.Claims;
using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using OfficeProject.Web.Pages.UserActivity;

namespace OfficeProject.Servicess
{
    public class ProductsService : IProductsService
    {


        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        private readonly IDataSerializer? DataSerializer;
        private readonly IService? Service;
        public ProductsService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper, IDataSerializer? dataSerializer, IService? Service)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;
            this.DataSerializer = dataSerializer;
            this.Service = Service;

        }

       
        public async Task AddOrUpdateProductAsync(ProductsDTO productsDTO)
        {
            using var context = dbContextFactory.CreateDbContext();

            if (productsDTO == null)
                throw new ArgumentNullException(nameof(productsDTO));

            // Get user ID from claims
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                throw new UnauthorizedAccessException("User not authenticated or invalid user ID");

            productsDTO.UserId = userId;

            // Uniqueness checks
            if (await context.Products.AnyAsync(p =>
                p.ProductsAlias == productsDTO.ProductsAlias &&
                p.ProductsId != productsDTO.ProductsId))
            {
                throw new InvalidOperationException("Product alias must be unique");
            }

            if (await context.Products.AnyAsync(p =>
                p.ProductsName == productsDTO.ProductsName &&
                p.ProductsId != productsDTO.ProductsId))
            {
                throw new InvalidOperationException("Product name must be unique");
            }

            Products productEntity;

            // Check if it's an update or insert
            var existingProduct = await context.Products
                .FirstOrDefaultAsync(p => p.ProductsId == productsDTO.ProductsId);

            if (existingProduct == null)
            {
                productEntity = new Products
                {
                    UserId = userId,
                    ProductsAlias = productsDTO.ProductsAlias,
                    ProductsName = productsDTO.ProductsName,
                    ProductsDescription = productsDTO.ProductsDescription,
                    ProductsSellingPrice = productsDTO.ProductsSellingPrice,
                    ProductsCostingPrice = productsDTO.ProductsCostingPrice,
                    IsBacklink = productsDTO.IsBacklink,
                    IsClasified = productsDTO.IsClasified,
                    IsSocialSharing = productsDTO.IsSocialSharing,
                    IsPost = productsDTO.IsPost,
                    IsReels = productsDTO.IsReels,
                    IsAdsBudget = productsDTO.IsAdsBudget,
                    ProductsStatus = productsDTO.ProductsStatus,
                    ProductsEntryDate = productsDTO.ProductsEntryDate,
                    ProductsModificationDate = productsDTO.ProductsModificationDate
                };

                context.Products.Add(productEntity);
                await context.SaveChangesAsync(); // Auto-generates ProductsId
            }
            else
            {
                existingProduct.UserId = userId;
                existingProduct.ProductsAlias = productsDTO.ProductsAlias;
                existingProduct.ProductsName = productsDTO.ProductsName;
                existingProduct.ProductsDescription = productsDTO.ProductsDescription;
                existingProduct.ProductsSellingPrice = productsDTO.ProductsSellingPrice;
                existingProduct.ProductsCostingPrice = productsDTO.ProductsCostingPrice;
                existingProduct.IsBacklink = productsDTO.IsBacklink;
                existingProduct.IsClasified = productsDTO.IsClasified;
                existingProduct.IsSocialSharing = productsDTO.IsSocialSharing;
                existingProduct.IsPost = productsDTO.IsPost;
                existingProduct.IsReels = productsDTO.IsReels;
                existingProduct.IsAdsBudget = productsDTO.IsAdsBudget;
                existingProduct.ProductsStatus = productsDTO.ProductsStatus;
                existingProduct.ProductsEntryDate = productsDTO.ProductsEntryDate;
                existingProduct.ProductsModificationDate = productsDTO.ProductsModificationDate;

                await Service.UpdateServiceFieldsByProductIdAsync((int)productsDTO.ProductsId!,
        productsDTO.ProductsName,
        productsDTO.IsBacklink,
        productsDTO.IsClasified,
        productsDTO.IsSocialSharing,
        productsDTO.IsPost,
        productsDTO.IsReels,
        productsDTO.IsAdsBudget);

                await context.SaveChangesAsync();

                productEntity = existingProduct;
            }

            // Handle child records: userWorkingActivity
            foreach (var activity in productsDTO.UserWorkingActivity ?? [])
            {
                try
                {
                    var existingActivity = await context.UserWorkingActivity
                        .FirstOrDefaultAsync(x => x.workingActivityId == activity.WorkingActivityId);

                    if (existingActivity == null)
                    {
                        var newActivity = new UserWorkingActivity
                        {
                            ProductsId = productEntity.ProductsId, // Now we have a valid ProductId
                            WorkingActivityName = activity.WorkingActivityName
                        };
                        context.UserWorkingActivity.Add(newActivity);
                    }
                    else
                    {
                        existingActivity.WorkingActivityName = activity.WorkingActivityName;
                        context.UserWorkingActivity.Update(existingActivity);
                    }

                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving UserWorkingActivity: {ex.Message}");
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


                using (var context = dbContextFactory.CreateDbContext())
                {
                    var products = await context.Products
                            .Include(p => p.UserWorkingActivity).ToListAsync();

                    return products.Select(p => new ProductsDTO
                    {
                        ProductsId = p.ProductsId,
                        UserId = p.UserId,
                        ProductsAlias = p.ProductsAlias,
                        ProductsName = p.ProductsName,
                        ProductsDescription = p.ProductsDescription,
                        ProductsSellingPrice = p.ProductsSellingPrice,
                        ProductsCostingPrice = p.ProductsCostingPrice,
                        IsBacklink = p.IsBacklink,
                        IsClasified = p.IsClasified,
                        IsSocialSharing = p.IsSocialSharing,
                        IsPost = p.IsPost,
                        IsReels = p.IsReels,
                        IsAdsBudget = p.IsAdsBudget,
                        ProductsStatus = p.ProductsStatus,
                        ProductsEntryDate = p.ProductsEntryDate,
                        ProductsModificationDate = p.ProductsModificationDate,

                        UserWorkingActivity = p.UserWorkingActivity?.Select(a => new UserWorkingActivityDto
                        {
                            WorkingActivityId = a.workingActivityId,
                            ProductsId = a.ProductsId,
                            WorkingActivityName = a.WorkingActivityName
                        }).ToList()
                    }).ToList();

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
