using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCar_Creation.Context;
using MyCar_Creation.Dtos;
using MyCar_Creation.Entities;
using MyCar_Creation.Services.Abstractions;
using MyCar_Creation.Services.Interfaces;

namespace MyCar_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryController(ApplicationDbContext context,IMapper mapper,ICategoryService categoryService) 
        {
            _categoryService = categoryService;
            _mapper = mapper;   
            _context=context;
        }
        [HttpPost]
        public ActionResult CreateCategory(CategoryDTO model)
        {
            
            if (_categoryService.AddCategory(model)>0)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("{categoryId}")]
        public ActionResult DeleteCategory([FromRoute] Guid categoryId)
        {
            
                if (_categoryService.DeleteCategory(categoryId)>0)
                {
                    return Ok();
                }
                 return BadRequest();
        }
        [HttpGet]
        public ActionResult GetAllCategory()
        {
            List<Category> categories=_context.Categories.ToList();
            if(categories is not null)
            {
                return Ok(categories);
            }
            return BadRequest();
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] string categoryId, CategoryDTO model)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync( categoryId,model);
            if (await _context.SaveChangesAsync()> 0)
                {
                    return Ok(updatedCategory);
                }
                return BadRequest();
        }
        [HttpGet("GetVehicles/{categoryId}")]
        public ActionResult GetVehiclesByCategoryId([FromRoute] string categoryId)
        {
            if(categoryId is not null && categoryId != "undefined") 
            {
                var result = _context.Categories.Include(x => x.Vehicles).FirstOrDefault(x => x.Id == Guid.Parse(categoryId));
                if (result is not null)
                {
                    return Ok(result);
                }
            }
          
            return NotFound();
        }

    }
}
