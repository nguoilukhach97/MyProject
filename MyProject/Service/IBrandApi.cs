using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Service
{
    public interface IBrandApi
    {
        Task<List<Brand>> GetBrands();
    }
}
