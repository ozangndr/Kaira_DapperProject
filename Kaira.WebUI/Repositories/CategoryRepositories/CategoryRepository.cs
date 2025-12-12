using Dapper;
using Kaira.WebUI.Context;
using Kaira.WebUI.DTOs.CategoryDtos;
using System.Data;

namespace Kaira.WebUI.Repositories.CategoryRepositories
{
    public class CategoryRepository(AppDbContext context) : ICategoryRepository
    {
        private readonly IDbConnection _db = context.CreateConnection();
        public async Task CreateAsync(CreateCategoryDto dto)
        {
            string query = "Insert into Categories (Name) Values (@Name)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", dto.Name);
            await _db.ExecuteAsync(query, parameters);
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete from Categories where CategoryId=@CategoryId";
            var parameters = new DynamicParameters();
            parameters.Add("CategoryId", id);
            await _db.ExecuteAsync(query, parameters);
        }

        public Task<IEnumerable<ResultCategoryDto>> GetAllAsync()
        {
            string query = "Select * from Categories";
            return _db.QueryAsync<ResultCategoryDto>(query);
        }

        public Task<UpdateCategoryDto> GetByIdAsync(int id)
        {
            string query = "Select * from Categories where CategoryId=@CategoryId";
            var parameters = new DynamicParameters();
            parameters.Add("CategoryId", id);
            return _db.QueryFirstOrDefaultAsync<UpdateCategoryDto>(query, parameters);
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            string query = "Update Categories set Name=@Name where CategoryId=@CategoryId";
            var parameters = new DynamicParameters(dto);
            await _db.ExecuteAsync(query, parameters);
        }
    }
}
