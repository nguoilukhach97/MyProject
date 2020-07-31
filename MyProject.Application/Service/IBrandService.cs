using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.Service
{
    public interface IBrandService
    {
        Task<List<Brand>> GetBrands();
        
    }
}
