using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.PublicRequest;
using MyProject.Application.ModelRequestService.ServiceRequest.Product;
using MyProject.Common;
using MyProject.Data.EF;
using MyProject.Data.Entities;
using MyProject.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly MyProjectDbContext _context;
        private readonly IStorageService _storageService;
        public ProductService(MyProjectDbContext context,IStorageService service)
        {
            _context = context;
            _storageService = service;
        }
        #region Manage product
        public async Task<ResponseBase> CreateProduct(ProductCreateRequest request)
        {
            var response = new ResponseBase()
            {
                Successed = false,
                Errors = new Error() { Code = "add_product_failed",Description = CommonMessage.CreateproductFailed}
            };
            var product = new Product()
            {
                Name = request.Name,
                BrandId = request.BrandId,
                Description = request.Description,
                Details = request.Details,
                DateCreated = DateTime.Now.Date,
                UserCreated = request.UserCreated,
                ViewCount =0,
                Status = request.Status
            };

            // Save Image
            if (request.ImageProduct !=null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption= "thumbnail image",
                        DateCreated = DateTime.Now,
                        ImagePath = await this.SaveFile(request.ImageProduct),
                        IsDefault = true,
                        SortOrder=1,

                    }
                };
            }

            await _context.Products.AddAsync(product);
            var result = await _context.SaveChangesAsync();
            if (result >0)
            {
                var responseSuccess = new ResponseBase()
                {
                    Successed = true,
                    Errors = new Error() { Code = "add_product_success", Description = CommonMessage.CreateproductSuccessed }
                };
                return responseSuccess;
            }
            //_context.ProductImages.AddRange(product.ProductImages);
            return response;
        }

        public async Task<ResponseBase> CreateProductDetail(ProductDetailCreateRequest request)
        {
            var response = new ResponseBase()
            {
                Successed = false,
                Errors = new Error() { Code = "add_product_failed", Description = CommonMessage.CreateproductFailed }
            };
            var productDetail = new ProductDetail()
            {
                ProductId = request.ProductId,
                
                Price = request.Price,
                PromotionPrice = request.ProductId,
                Quantity = request.Quantity,
                Warranty= request.Warranty,
                Size = request.Size,
                DateCreated = DateTime.Now.Date,
                UserCreated = request.UserCreated,

                Status =request.Status,
            };

            await _context.ProductDetails.AddAsync(productDetail);
            var result =  await _context.SaveChangesAsync();
            if (result > 0)
            {
                var responseSuccess = new ResponseBase()
                {
                    Successed = true,
                    Errors = new Error() { Code = "add_detailproduct_success", Description = CommonMessage.CreateproductSuccessed }
                };
                return responseSuccess;
            }

            return response;
        }

        public async Task<ResponseBase> DeleteProduct(int id)
        {
            var response = new ResponseBase()
            {
                Successed = false,
                Errors = new Error() { Code = "delete_product_failed", Description = CommonMessage.DeleteproductFailed }
            };
            
            var product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                var productDetail = _context.ProductDetails.Where(p => p.ProductId == id);
                _context.ProductDetails.RemoveRange(productDetail);

                // remove image
                var image = _context.ProductImages.Where(p=>p.ProductId==id);
                foreach (var item in image)
                {
                    _context.ProductImages.Remove(item);
                     await _storageService.DeleteFileAsync(item.ImagePath);
                }
                

                _context.Products.Remove(product);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var responseSuccess = new ResponseBase()
                    {
                        Successed = true,
                        Errors = new Error() { Code = "delete_product_success", Description = CommonMessage.DeleteproductSuccessed }
                    };
                    return responseSuccess;
                }
                return response;
            }

            response.Errors.Description = CommonMessage.CannotFind;
            return response;
        }

        public async Task<ResponseBase> DeleteProductDetail(int idDetail)
        {
            var response = new ResponseBase()
            {
                Successed = false,
                Errors = new Error() { Code = "delete_product_failed", Description = CommonMessage.DeleteproductFailed }
            };

            var productDetail = await _context.ProductDetails.FindAsync(idDetail);
            if (productDetail != null)
            {
                _context.ProductDetails.Remove(productDetail);
                var result =  await _context.SaveChangesAsync();
                if (result > 0 )
                {
                    var responseSuccess = new ResponseBase()
                    {
                        Successed = true,
                        Errors = new Error() { Code = "delete_product_success", Description = CommonMessage.DeleteproductSuccessed }
                    };
                    return responseSuccess;
                }
            }

            response.Errors.Description = CommonMessage.CannotFind;

            return response;
            
        }

        

        public async Task<ResponseBase> UpdateProduct(ProductUpdateRequest request)
        {
            var response = new ResponseBase()
            {
                Successed = false,
                Errors = new Error() { Code = "update_product_failed", Description = CommonMessage.UpdateProductFailed }
            };
            var product = await _context.Products.FindAsync(request.Id);
            if(product !=null)
            {
                product.Name = request.Name;
                product.BrandId = request.BrandId;
                product.Description = request.Description;
                product.Details = request.Details;
                product.DateModified = DateTime.Now.Date;
                product.UserModified = request.UserModified;
                product.Status = request.Status;

                // update image

                if(request.ProductImage !=null)
                {
                    var thumbnailImage = _context.ProductImages.FirstOrDefault(p=>p.ProductId==request.Id && p.IsDefault==true);
                    if (thumbnailImage != null)
                    {
                        thumbnailImage.ImagePath = await this.SaveFile(request.ProductImage);
                        _context.ProductImages.Update(thumbnailImage);
                    }
                }    

                var result =  await _context.SaveChangesAsync();

                if (result > 0)
                {
                    var responseSuccess = new ResponseBase()
                    {
                        Successed = true,
                        Errors = new Error() { Code = "update_product_successed", Description = CommonMessage.UpdateProductSuccessed }
                    };
                    return responseSuccess;
                }

                return response;
            }

            response.Errors.Description = CommonMessage.CannotFind;
            return response;
            

        }

        public async Task<ResponseBase> UpdateProductDetail(ProductDetailUpdateRequest request)
        {
            var response = new ResponseBase()
            {
                Successed = false,
                Errors = new Error() { Code = "update_product_failed", Description = CommonMessage.UpdateProductSuccessed }
            };
            var productDetail = await _context.ProductDetails.FindAsync(request.Id);
            if (productDetail != null)
            {
                
                productDetail.Price = request.Price;
                productDetail.PromotionPrice = request.PromotionPrice;
                productDetail.Quantity = request.Quantity;
                productDetail.Warranty = request.Quantity;
                productDetail.Size = request.Size;
                productDetail.DateModified = request.DateModified;
                productDetail.UserModified = request.UserModified ;
                productDetail.Status =request.Status;

                var result =  await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var responseSuccess = new ResponseBase()
                    {
                        Successed = true,
                        Errors = new Error() { Code = "update_product_successed", Description = CommonMessage.UpdateProductSuccessed }
                    };
                    return responseSuccess;
                }
            }

            response.Errors.Description = CommonMessage.CannotFind;
            return response;
        }

        public async Task UpdateViewCount(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.ViewCount += 1;

                await _context.SaveChangesAsync();
            }
        }

        #endregion
        // Save Image
        #region Manage Image
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }



        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }
        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new MyProjectException($"Cannot find an image with id {imageId}");

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new MyProjectException($"Cannot find an image with id {imageId}");
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }



        public async Task<ProductImageViewModel> GetImageById(int id)
        {
            var image = await _context.ProductImages.FindAsync(id);
            if (image == null)
                throw new MyProjectException($"Cannot find an image with id {id}");

            var viewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault.GetValueOrDefault(false),
                ProductId = image.ProductId,
                SortOrder = image.SortOrder.GetValueOrDefault(0)
            };
            return viewModel;
        }
        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId)
                .Select(i => new ProductImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault.GetValueOrDefault(false),
                    ProductId = i.ProductId,
                    SortOrder = i.SortOrder.GetValueOrDefault(0)
                }).ToListAsync();
        }
        #endregion

        #region Public 

        public async Task<Pagination<Product>> GetAllPaging(SearchingBase request)
        {
            var product = _context.Products.OrderBy(p => p.Name);
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                product = product.Where(x => x.Name.Contains(request.Keyword) || x.Description.Contains(request.Keyword) ).OrderBy(x=>x.Name);
            }
            var totalRow = await product.CountAsync();
            var data = await product.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            var result = new Pagination<Product>()
            {
                CurrentPage = request.PageIndex,
                PageSize = request.PageSize,
                TotalPages = totalRow % request.PageSize == 0 ? totalRow / request.PageSize : totalRow / request.PageSize + 1,
                TotalRows = totalRow,
                Data =data
            };
           
            return result;
        }

        public async Task<Pagination<ProductDetail>> GetAllSize(int id,SearchingBase request)
        {
            var productDetails = _context.ProductDetails.Where(x=>x.ProductId == id);
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                productDetails = productDetails.Where(x => x.Size.ToString().Contains(request.Keyword));
            }
            var totalRow = await productDetails.CountAsync();
            var data = await productDetails.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            var result = new Pagination<ProductDetail>()
            {
                CurrentPage = request.PageIndex,
                PageSize = request.PageSize,
                TotalPages = totalRow % request.PageSize == 0 ? totalRow / request.PageSize : totalRow / request.PageSize + 1,
                TotalRows = totalRow,
                Data = data
            };

            return result;
        }

        //public async Task<PagedViewResult<ProductViewModel>> GetAllPaging(ProductPaingParam request)
        //{
        //    var product =(from a in _context.Products
        //                   join b in _context.ProductDetails on a.Id equals b.ProductId
        //                   join c in _context.ProductInCategories on a.Id equals c.ProductId
        //                   join d in _context.Categories on c.CategoyId equals d.Id
        //                   join e in _context.ProductImages on a.Id equals e.ProductId
        //                   join f in _context.Brands on a.BrandId equals f.Id
        //                   where request.CategoryId.Contains(d.Id)
        //                   group new { a, b, d, e, f } by new 
        //                   { a.Id, a.Name,BrandId=f.Id,CategoryId=d.Id, NameBrand=f.Name, e.ImagePath,a.Description,a.Details,a.DateCreated,a.ViewCount,a.Status } into gr
        //                   select new ProductViewModel()
        //                   {
        //                       Id = gr.Key.Id,
        //                       Name = gr.Key.Name,
        //                       BrandId = gr.Key.BrandId,
        //                       CategoryId = gr.Key.CategoryId,
        //                       BrandName = gr.Key.NameBrand,
        //                       PriceStart = gr.Min(p=>p.b.PromotionPrice),
        //                       PriceEnd = gr.Max(p=>p.b.PromotionPrice),
        //                       Description = gr.Key.Description,
        //                       Details = gr.Key.Details,
        //                       DateCreated = gr.Key.DateCreated,
        //                       ViewCount = gr.Key.ViewCount,
        //                       Status = gr.Key.Status
        //                   });
        //    if (!string.IsNullOrEmpty(request.Keyword))
        //    {
        //        product = product.Where(p => p.Name.Contains(request.Keyword) || p.Description.Contains(request.Keyword));
        //    }
        //    if(request.CategoryId.Count()>0)
        //    {
        //        product = product.Where(p => request.CategoryId.Contains(p.CategoryId));
                
        //    }
        //    int totalRow = await product.CountAsync();
        //    var data = await product.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize).ToListAsync();
        //    var result = new PagedViewResult<ProductViewModel>()
        //    {
        //        Items = data,
        //        TotalRecord = totalRow
        //    };

        //    return result;
        //}

        #endregion

    }
}
