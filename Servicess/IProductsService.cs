using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public interface IProductsService
    {
        Task<List<ProductsDTO>> GetAllProductsDTOAsync();
        Task AddOrUpdateProductAsync(ProductsDTO productsDTO);
        Task<ProductsDTO?> GetProductsDTOById(int id);
    }
}
