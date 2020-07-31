using Microsoft.EntityFrameworkCore;
using MyProject.Data.EF;
using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.Service
{
    public class BrandService : IBrandService
    {
        private readonly MyProjectDbContext _context;

        public BrandService(MyProjectDbContext context)
        {
            _context = context;
        }
        public async Task<List<Brand>> GetBrands()
        {
            var result = await _context.Brands.OrderByDescending(p => p.Name).ToListAsync();
            return result;
        }
    }
}
