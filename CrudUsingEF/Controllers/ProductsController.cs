using CrudUsingEF.Models;
using CrudUsingEF.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudUsingEF.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IRepository<Product> _productRepository;
        public ProductsController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            await _productRepository.AddAsync(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(id);
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _productRepository.UpdateAsync(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(id);
            }
            await _productRepository.DeleteAsync(product);
            return RedirectToAction("Index");
        }


        //private readonly AppDbContext _context;

        //public ProductsController(AppDbContext context)
        //{
        //    _context = context;
        //}

        //public async Task<IActionResult> Index()
        //{
        //    var products = await _context.Products.ToListAsync(); // Fetch all products
        //    return View(products);
        //}

        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(Product product)
        //{
        //    await _context.Products.AddAsync(product);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        //    if (product == null)
        //    {
        //        return NotFound(id);
        //    }

        //    return View(product);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(Product product)
        //{
        //    _context.Update(product);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var emplotee = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        //    if (emplotee == null)
        //    {
        //        return NotFound(id);
        //    }

        //    _context.Products.Remove(emplotee);

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Index");
        //}
    }
}
