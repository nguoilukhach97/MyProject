using MyProject.Application;
using MyProject.Application.ModelRequestService.PublicRequest;
using MyProject.Application.ModelRequestService.ServiceRequest.Product;
using MyProject.Common;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Service
{
    public interface IProductApi
    {

        #region Manage
        Task<ResponseBase> CreateProduct(ProductCreateRequest request);
        Task<ResponseBase> CreateProductDetail(ProductDetailCreateRequest request);
        Task<ResponseBase> UpdateProduct(ProductUpdateRequest request);
        Task<ResponseBase> UpdateProductDetail(ProductDetailUpdateRequest request);
        Task<ResponseBase> DeleteProduct(int id);
        Task<ResponseBase> DeleteProductDetail(int idDetail);

        Task UpdateViewCount(int productId);


        // thêm ảnh

        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImage(int productId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        Task<ProductImageViewModel> GetImageById(int id);


        Task<List<ProductImageViewModel>> GetListImages(int productId);
        #endregion

        #region Public 


        Task<Pagination<Product>> GetAllPaging(SearchingBase request);

        Task<Pagination<ProductDetail>> GetAllSize(int id, SearchingBase request);
        #endregion

    }
}
