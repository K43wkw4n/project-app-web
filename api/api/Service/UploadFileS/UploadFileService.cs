namespace api.Service.UploadFileS
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UploadFileService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public Task DeleteFileImages(List<string> files)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            foreach (var item in files)
            {
                var file = Path.Combine("productImages", item);
                var oldImagePath = Path.Combine(wwwRootPath, file);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            return Task.CompletedTask;
        }

        public Task DeleteFileImage(string files)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
 
            var file = Path.Combine("productImage", files);
            var oldImagePath = Path.Combine(wwwRootPath, file);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            } 

            return Task.CompletedTask;
        }

        public bool IsUpload(IFormFileCollection formFiles)
        {
            return formFiles != null && formFiles?.Count > 0;
        }

        public bool IsUpload(IFormFile formFiles)
        {
            return formFiles != null;
        }

        public async Task<List<string>> UploadImages(IFormFileCollection formFiles)
        {
            var listFileName = new List<string>();

            //จัดการเส้นทาง
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var uploadPath = Path.Combine(wwwRootPath, "productImages");
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            foreach (var formFile in formFiles)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string fullName = Path.Combine(uploadPath, fileName);

                using (var stream = File.Create(fullName))
                {
                    await formFile.CopyToAsync(stream);
                }
                listFileName.Add(fileName);
            }

            return listFileName;
        }

        public async Task<string> UploadImage(IFormFile formFile)
        {
            string fileName = null;

            if (formFile != null && formFile.Length > 0)
            {
                // Handle file upload
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string uploadPath = Path.Combine(wwwRootPath, "productImage");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string filePath = Path.Combine(uploadPath, fileName);

                using (var stream = File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            return fileName;
        }

        public string Validation(IFormFileCollection formFiles)
        {
            foreach (var file in formFiles)
            {
                if (!ValidationExtension(file.FileName))
                {
                    return "Invalid File Extension";
                }

                if (!ValidationSize(file.Length))
                {
                    return "The file is too large";
                }
            }

            return null;
        }

        public string Validation(IFormFile formFile)
        {
            if (!ValidationExtension(formFile.FileName))
            {
                return "Invalid File Extension";
            }

            if (!ValidationSize(formFile.Length))
            {
                return "The file is too large";
            }

            return null;
        }

        public bool ValidationExtension(string filename)
        {
            string[] permittedExtensions = { ".jpg", ".png" }; // สามารถเพิ่มชื่อไฟล์ได้เลย
            string extension = Path.GetExtension(filename).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
            {
                return false;
            };
            return true;
        }

        public bool ValidationSize(long fileSize)
            => _configuration.GetValue<long>("FileSizeLimit") > fileSize;

    }
}
