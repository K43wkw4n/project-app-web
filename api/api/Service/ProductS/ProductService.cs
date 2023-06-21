using api.Data; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using api.DTOs.ProductR; 
using api.Service.UploadFileS;

namespace api.Service.ProductS
{
    public class ProductService : ControllerBase, IProductService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IUploadFileService _uploadFileService;

        public ProductService(Context context, IMapper mapper, IUploadFileService uploadFileService)
        {
            _context = context;
            _mapper = mapper;
            _uploadFileService = uploadFileService;
        }

        public async Task<List<Product>> GetProductAsync()
        {
            return await _context.Products.Include(x => x.ProductImages)
                .OrderByDescending(x => x.ID)
                .ToListAsync();
        }

        public async Task<object> CreateAndUpdateAsync(ProductRequest productRequest)
        {
            (string errorMessge, List<string> imageNames) =
                await UploadImageAsync(productRequest.FormFiles);

            (string errorMessgeMain, string imageName) =
                await UploadImageMainAsync(productRequest.Image);

            if (!string.IsNullOrEmpty(errorMessge)) return errorMessge;

            var result = await _context.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID == productRequest.ID);
             
            if (result == null)
            {
                var product = _mapper.Map<Product>(productRequest);
                 
                product.Image = imageName; 

                await _context.Products.AddAsync(product);

                if (imageNames.Count() > 0)
                {
                    var images = new List<ProductImage>();
                    imageNames.ForEach(imageName =>
                    {
                        images.Add(new ProductImage
                        {
                            ProductId = product.ID,
                            Image = imageName
                        });
                    });

                    await _context.ProductImages.AddRangeAsync(images);
                } 
            }
            else
            {
                var product = _mapper.Map<Product>(productRequest);

                product.Image = imageName;

                _context.Products.Update(product);
                 
                if (imageNames.Count() > 0)
                {
                    //Delete Old File
                    var productImage = await _context.ProductImages
                        .Where(x => x.ProductId.Equals(product.ID)).ToListAsync();

                    if (productImage is not null)
                    {
                        //Delete Db
                        _context.ProductImages.RemoveRange(productImage);
                        _context.SaveChangesAsync().Wait();

                        //Delete file
                        var file = productImage.Select(x => x.Image).ToList();
                        await _uploadFileService.DeleteFileImages(file);
                    }

                    var images = new List<ProductImage>();
                    imageNames.ForEach(imageName =>
                        images.Add(new ProductImage
                        {
                            ProductId = product.ID,
                            Image = imageName
                        })
                     );

                    await _context.ProductImages.AddRangeAsync(images);
                } 
            }

            var check = await _context.SaveChangesAsync();

            if (check > 0) return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        public async Task<object> RemoveAsync(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.ID == id);

            if (result == null) return NotFound();

            _context.Products.Remove(result);
            var check = await _context.SaveChangesAsync();

            if (check > 0) return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        public async Task<(string errorMessge, List<string> imageNames)> UploadImageAsync(IFormFileCollection formfiles)
        {
            var errorMessge = string.Empty;
            var imageNames = new List<string>();

            if (_uploadFileService.IsUpload(formfiles))
            {
                errorMessge = _uploadFileService.Validation(formfiles);
                if (errorMessge is null)
                {
                    imageNames = await _uploadFileService.UploadImages(formfiles);
                }
            }

            return (errorMessge, imageNames);
        }

        public async Task<(string errorMessge, string imageNames)> UploadImageMainAsync(IFormFile formfile)
        {
            var errorMessge = string.Empty;
            var imageName = string.Empty;

            if (_uploadFileService.IsUpload(formfile))
            {
                errorMessge = _uploadFileService.Validation(formfile);
                if (errorMessge is null)
                {
                    imageName = await _uploadFileService.UploadImage(formfile);
                }
            }

            return (errorMessge, imageName);
        }

    }
}
