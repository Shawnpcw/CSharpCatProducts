using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using productsCats.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace productsCats.Controllers
{
    public class HomeController : Controller
    {
        private productsCatsContext dbContext;
        public HomeController(productsCatsContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {

            return Redirect("Products");
        }
        [HttpGet("products")]
        public IActionResult Products()
        {
            ViewBag.Products = dbContext.products.ToList();
            return View();
        }
        [HttpPost("products")]
        public IActionResult CreateProduct(Product newProdct)
        {
            if(ModelState.IsValid)
            {
                dbContext.products.Add(newProdct);
                dbContext.SaveChanges();
                return Redirect("products");
            }
            return View("Products");
        }
        [HttpGet("categories")]
        public IActionResult Categories()
        {
            ViewBag.Categories = dbContext.categories.ToList();
            return View();
        }
        [HttpPost("categories")]
        public IActionResult CreateCategory(Category newCat)
        {
            if(ModelState.IsValid)
            {
                dbContext.categories.Add(newCat);
                dbContext.SaveChanges();
                return Redirect("categories");
            }
            return View("Categories");
        }
        [HttpGet("products/{id}")]
        public IActionResult singleProduct(int id)
        {
            ViewBag.Products = dbContext.products.SingleOrDefault(p=>p.ProductId == id);
            // ViewBag.ProdCat = "";
            // = dbContext.products.Include(p => p.Associations).ThenInclude(a => a.Category).FirstOrDefault(p => p.ProductId == id);
            Product ProductWithCategories = dbContext.products
                .Include(p => p.Associations)
                    .ThenInclude(a => a.Category)
                    .FirstOrDefault(f => f.ProductId == id);
            List<Category> UnassignedCategories = dbContext.categories
                .Where(c => !ProductWithCategories.Associations
                    .Any(a => a.Category.Name == c.Name))
                    .ToList();
            ViewBag.ProdCat = dbContext.categories
                .Where(c => ProductWithCategories.Associations
                    .Any(a => a.Category.Name == c.Name))
                    .ToList();
            ViewBag.Categories = UnassignedCategories;

            return View();
        }
        [HttpPost("products/{id}")]
        public IActionResult addCategory(int id, Association NewAssociation)
        {
            dbContext.Add(NewAssociation);
            dbContext.SaveChanges();
            return Redirect("/products/"+id);
        }
        [HttpGet("categories/{id}")]
        public IActionResult singleCategory(int id)
        {
ViewBag.Products = dbContext.products.SingleOrDefault(p=>p.ProductId == id);
            // ViewBag.ProdCat = "";
            // = dbContext.products.Include(p => p.Associations).ThenInclude(a => a.Category).FirstOrDefault(p => p.ProductId == id);
            Category CategoryWithProducts = dbContext.categories
                .Include(c => c.Associations)
                    .ThenInclude(a => a.Product)
                    .FirstOrDefault(f => f.CategoryId == id);
            List<Product> UnnasignedProducts = dbContext.products
                .Where(p => !CategoryWithProducts.Associations
                    .Any(a => a.Product.ProductId == p.ProductId))
                    .ToList();
            ViewBag.ProdCat = dbContext.products
                .Where(p => CategoryWithProducts.Associations
                    .Any(a => a.Product.ProductId == p.ProductId))
                    .ToList();
            ViewBag.Products = UnnasignedProducts;
            ViewBag.Category = CategoryWithProducts;
          
            return View();
        }
    }
}
