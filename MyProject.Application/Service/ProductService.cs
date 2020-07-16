using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.PublicRequest;
using MyProject.Application.ModelRequestService.ServiceRequest;
using MyProject.Data.EF;
using MyProject.Data.Entities;
using MyProject.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly MyProjectDbContext _context;
        public ProductService(MyProjectDbContext context)
        {
            _context = context;
        }
        #region Manage
        public async Task<int> CreateProduct(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                BrandId = request.BrandId,
                Description = request.Description,
                Details = request.Details,
                DateCreated = request.DateCreated,
                UserCreated = request.UserCreated,
                ViewCount =0,
                Status = request.Status
            };
            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CreateProductDetail(ProductDetailCreateRequest request)
        {
            var productDetail = new ProductDetail()
            {
                ProductId = request.ProductId,
                
                Price = request.Price,
                PromotionPrice = request.ProductId,
                Quantity = request.Quantity,
                Warranty= request.Warranty,
                Size = request.Size,
                DateCreated = request.DateCreated,
                UserCreated = request.UserCreated,

                Status =request.Status,
            };

            await _context.ProductDetails.AddAsync(productDetail);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                var productDetail = _context.ProductDetails.Where(p => p.ProductId == id);
                _context.ProductDetails.RemoveRange(productDetail);
                _context.Products.Remove(product);
                return await _context.SaveChangesAsync();
            }

            throw new MyProjectException($"Cannot find a product : {id}");
        }

        public async Task<int> DeleteProductDetail(int idDetail)
        {
            var productDetail = await _context.ProductDetails.FindAsync(idDetail);
            if (productDetail != null)
            {
                _context.ProductDetails.Remove(productDetail);
                return await _context.SaveChangesAsync();
            }

            throw new MyProjectException($"Cannot find a detail Product with detail id : {idDetail}");
        }

        

        public async Task<int> UpdateProduct(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if(product !=null)
            {
                product.Name = request.Name;
                product.BrandId = request.BrandId;
                product.Description = request.Description;
                product.Details = request.Details;
                product.DateModified = request.DateModified;
                product.UserModified = request.UserModified;
                product.Status = request.Status;

                return await _context.SaveChangesAsync();
            }

            return 0;
            
        }

        public async Task<int> UpdateProductDetail(ProductDetailUpdateRequest request)
        {
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

                return await _context.SaveChangesAsync();
            }

            return 0;
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

        #region Public 

        public async Task<PagedViewResult<ProductViewModel>> GetAllByCategory(int CategoryId, int pageIndex, int pageSize)
        {
            var product = (from a in _context.Products join
                          b in _context.ProductDetails on a.Id equals b.ProductId
                           join c in _context.ProductInCategories on a.Id equals c.ProductId
                           join d in _context.Categories on c.CategoyId equals d.Id
                           select a
                          );
            throw new NotImplementedException();
        }

        public async Task<PagedViewResult<ProductViewModel>> GetAllPaging(ProductPaingParam request)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
