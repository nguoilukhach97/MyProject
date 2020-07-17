using Microsoft.AspNetCore.Http;
using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.PublicRequest;
using MyProject.Application.ModelRequestService.ServiceRequest;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.Service
{
    public interface IProductService
    {
        #region Manage
        Task<int> CreateProduct(ProductCreateRequest request);
        Task<int> CreateProductDetail(ProductDetailCreateRequest request);
        Task<int> UpdateProduct(ProductUpdateRequest request);
        Task<int> UpdateProductDetail(ProductDetailUpdateRequest request);
        Task<int> DeleteProduct(int id);
        Task<int> DeleteProductDetail(int idDetail);

        Task UpdateViewCount(int productId);


        // thêm ảnh

        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImage(int productId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        Task<ProductImageViewModel> GetImageById(int id);
        Task<List<ProductImageViewModel>> GetListImages(int productId);
        #endregion

        #region Public 

        Task<PagedViewResult<ProductViewModel>> GetAllPaging(ProductPaingParam request);
        Task<PagedViewResult<Product>> GetAllByCategory();
        #endregion

    }
}
