using api.DTOs;
using api.DTOs.ProductR;

namespace api.Service.ProductS
{
    public interface IProductService
    {
        Task<List<Product>> GetProductAsync();
        Task<Object> CreateAndUpdateAsync(ProductRequest productRequest);
        Task<Object> RemoveAsync(int id);
        Task<(string errorMessge, List<string> imageNames)> UploadImageAsync(IFormFileCollection formfiles);
    }
}
