using MyCar_Creation.Dtos;
using MyCar_Creation.Entities;

namespace MyCar_Creation.Services.Interfaces
{
    public interface ICategoryService
    {
       int  AddCategory( CategoryDTO model);
       int DeleteCategory(Guid categoryId);
        Task<CategoryDTO> UpdateCategoryAsync(string categoryId, CategoryDTO model);
    }
}
