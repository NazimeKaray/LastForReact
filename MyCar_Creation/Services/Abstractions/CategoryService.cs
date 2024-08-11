using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCar_Creation.Context;
using MyCar_Creation.Dtos;
using MyCar_Creation.Entities;
using MyCar_Creation.MappingProfile;
using MyCar_Creation.Services.Interfaces;


namespace MyCar_Creation.Services.Abstractions
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(ApplicationDbContext context, IMapper mapper) 
        {
            _mapper = mapper;
            _context = context;
        }
        public int AddCategory(CategoryDTO model)
        {
            var obj = _mapper.Map<Category>(model);
            _context.Categories.Add(obj);
             return _context.SaveChanges();

        }
        public int DeleteCategory(Guid categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
                _context.Categories.Remove(category);
                return _context.SaveChanges();
        }
        public async Task<CategoryDTO> UpdateCategoryAsync(string categoryId,CategoryDTO model)
        {
            var existingCategory = await _context.Categories.FindAsync(Guid.Parse(categoryId));
            if (existingCategory is not null)
            {
                Category obj = _mapper.Map<CategoryDTO, Category>(model, existingCategory);
                _context.Categories.Update(obj);
                await _context.SaveChangesAsync();
                return _mapper.Map<Category,CategoryDTO >(existingCategory,model);
            }
            else
            {
                throw new KeyNotFoundException("Category not found");
            }
           
        }
            

    }
}
