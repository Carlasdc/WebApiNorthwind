using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiNorthwind.Models;
using System.Linq;

namespace WebApiNorthwind.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly NorthwindContext _context;
        public CategoryController(NorthwindContext context)
        {
            _context = context;

        }

        //GET /api/Category
        [HttpGet]
        public IEnumerable<CategoryName> Get()
        {
            //LinqtoEntities Todos los publishers

            List<string> names = (from c in _context.Categories
                                         select c.CategoryName).ToList();
            List<CategoryName> categoryNames = new List<CategoryName>();
            foreach (string name in names)
            {
                CategoryName nameCat = new CategoryName(name);
                categoryNames.Add(nameCat);
            }
            return categoryNames;
        }

        // GET /api/category/id
        [HttpGet("{id}")]
        public CategoryName Get(int id)
        {
            var categories = (from c in _context.Categories
                              where c.CategoryId == id
                              select c.CategoryName).SingleOrDefault();
            var name = new CategoryName(categories);
            return name;
        }

        

    }
}
