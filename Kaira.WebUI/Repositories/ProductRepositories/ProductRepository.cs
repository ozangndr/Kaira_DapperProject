using Dapper;
using Kaira.WebUI.Context;
using Kaira.WebUI.DTOs.CategoryDtos;
using Kaira.WebUI.DTOs.ProductDtos;
using System.Data;

namespace Kaira.WebUI.Repositories.ProductRepositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        private readonly IDbConnection _db = context.CreateConnection();
        public async Task CreateAsync(CreateProductDto dto)
        {
            string query = "Insert into Products (CategoryId,Name,ImageUrl,Description,Price) Values (@CategoryId,@Name,@ImageUrl,@Description,@Price)";
            var parameters = new DynamicParameters(dto);
            await _db.ExecuteAsync(query, parameters);
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete from Products where ProductId=@ProductId";
            var parameters = new DynamicParameters();
            parameters.Add("ProductId", id);
            await _db.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<ResultProductDto>> GetAllAsync()
        {
            string query = "select ProductId,Products.CategoryId,Products.Name,ImageUrl,Description,Price,Categories.Name as CategoryName from Products inner join Categories on Categories.CategoryId=Products.CategoryId";
            return await _db.QueryAsync<ResultProductDto>(query);
        }

        public Task<UpdateProductDto> GetByIdAsync(int id)
        {           

            string query = "select * from Products Where ProductId=@ProductId";
            var parameters = new DynamicParameters();
            parameters.Add("ProductId", id);
            return _db.QueryFirstOrDefaultAsync<UpdateProductDto>(query, parameters);
        }

        public async Task UpdateAsync(UpdateProductDto dto)
        {
            string query = "Update Products set CategoryId=@CategoryId,Name=@Name,ImageUrl=@Imageurl,Description=@Description,Price=@Price where ProductId=@ProductId";
            var parameters = new DynamicParameters(dto);
            await _db.ExecuteAsync(query, parameters);
        }
    }
}
