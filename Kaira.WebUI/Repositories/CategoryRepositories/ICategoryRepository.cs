using Kaira.WebUI.DTOs.CategoryDtos;

namespace Kaira.WebUI.Repositories.CategoryRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<ResultCategoryDto>> GetAllAsync();
        Task<UpdateCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateCategoryDto dto);
        Task UpdateAsync(UpdateCategoryDto dto);
        Task DeleteAsync(int id);

    }
}
