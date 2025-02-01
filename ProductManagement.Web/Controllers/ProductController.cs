using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Web.Data;
using ProductManagement.Web.Interfaces;
using ProductManagement.Web.Models;

namespace ProductManagement.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Product
        public IActionResult Index() => View();

        // GET: Fetch all products
        [HttpGet]
        public async Task<IActionResult> GetProducts() 
        {
            var products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            return Json(products);
        }

        // POST: Create Product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.GetRepository<Product>().AddAsync(product);
                await _unitOfWork.CommitAsync();
                return Json(new { success = true, message = "Product added successfully!" });
            }
            return Json(new { success = false, message = "Invalid data!" });
        }

        // GET: Get Product by ID
        [HttpGet]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);
            return product == null ? NotFound() : Json(product);
        }

        // PUT: Update Product
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.GetRepository<Product>().UpdateAsync(product);
                await _unitOfWork.CommitAsync();
                return Json(new { success = true, message = "Product updated successfully!" });
            }
            return Json(new { success = false, message = "Invalid data!" });
        }

        // DELETE: Delete Product
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);
            if (product == null) return NotFound();

            await _unitOfWork.GetRepository<Product>().DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            return Json(new { success = true, message = "Product deleted successfully!" });
        }
    }
}
