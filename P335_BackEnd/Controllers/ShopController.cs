using Microsoft.AspNetCore.Mvc;
using P335_BackEnd.Data;
using P335_BackEnd.Models;

namespace P335_BackEnd.Controllers
{
    public class ShopController : Controller
    {
        private AppDbContext _dbContext;

        public ShopController(AppDbContext dbContext)
        {
            _dbContext=dbContext;
        }
        public IActionResult Index(int page,string order="desc")
        {
            if (page <= 0) page = 1;

            int productsPerPage = 1;

            var productsCount =_dbContext.Products.Count();

            int totalPageCount = (int)Math.Ceiling((decimal)productsCount / productsPerPage);

            var products = order switch
            {
                "desc" => _dbContext.Products.OrderByDescending(x => x.Id),
                "asc" => _dbContext.Products.OrderBy(x => x.Id),
                _ => _dbContext.Products.OrderByDescending(x => x.Id)
            };

            var model = new ShopIndexVM
            {
     
                Products = products
                .Skip((page-1)*productsPerPage)
                .Take(productsPerPage)
                .ToList(),

                TotalPageCount = totalPageCount,
                CurrentPage = page

            };

            return View(model);
        }

        public IActionResult Filter(int page, string order = "desc")
        {
            if (page <= 0) page = 1;

            int productsPerPage = 1;
            var productCount = _dbContext.Products.Count();

            int totalPageCount = (int)Math.Ceiling(((decimal)productCount / productsPerPage));

            var products = order switch
            {
                "desc" => _dbContext.Products.OrderByDescending(x => x.Id),
                "asc" => _dbContext.Products.OrderBy(x => x.Id),
                _ => _dbContext.Products.OrderByDescending(x => x.Id)
            };

            var model = new ShopIndexVM
            {
                Products = products
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage)
                .ToList(),
                TotalPageCount = totalPageCount,
                CurrentPage = page
            };

            return ViewComponent("ShopProduct", model);
        }
    }
}
