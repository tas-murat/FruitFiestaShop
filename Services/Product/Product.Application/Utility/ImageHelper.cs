using Microsoft.AspNetCore.Http;
using Product.Application.Responses;
using Product.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Utility
{
    internal class ImageHelper
    {
        public static ProductDto CreateImage(ProductDto productDto)
        {
            if (productDto.Image != null)
            {

                string fileName = productDto.Id + Path.GetExtension(productDto.Image.FileName);
                string filePath = @"wwwroot\ProductImages\" + fileName;

                
                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                FileInfo file = new FileInfo(directoryLocation);
                if (file.Exists)
                {
                    file.Delete();
                }

                var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                {
                    productDto.Image.CopyTo(fileStream);
                }
                productDto.ImageUrl = productDto.BaseUrl + "/ProductImages/" + fileName;
                productDto.ImageLocalPath = filePath;
            }
            else
            {
                productDto.ImageUrl = "https://placehold.co/600x400";
            }
            return productDto;
        }
        public static ProductDto UpdateImage(ProductDto productDto)
        {
            if (productDto.Image != null)
            {
                if (!string.IsNullOrEmpty(productDto.ImageLocalPath))
                {
                    var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), productDto.ImageLocalPath);
                    FileInfo file = new FileInfo(oldFilePathDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }

                string fileName = productDto.Id + Path.GetExtension(productDto.Image.FileName);
                string filePath = @"wwwroot\ProductImages\" + fileName;
                var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                {
                    productDto.Image.CopyTo(fileStream);
                }
                productDto.ImageUrl = productDto.BaseUrl + "/ProductImages/" + fileName;
                productDto.ImageLocalPath = filePath;
            }

            return productDto;
        }
        public static void DeleteImage(ProductItem obj)
        {
            if (!string.IsNullOrEmpty(obj.ImageLocalPath))
            {
                var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), obj.ImageLocalPath);
                FileInfo file = new FileInfo(oldFilePathDirectory);
                if (file.Exists)
                {
                    file.Delete();
                }
            }

        }
    }
}
