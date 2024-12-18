using Microsoft.Data.SqlClient;
using System.Data;
using TestingApiADONET.Models;

namespace TestingApiADONET.Services
{
    public class ProductRepository(IConfiguration configuration) : DbFactory(configuration)
    {
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            ProductSps sps = new();
            var products = new List<Product>();
            using var cn = Create();
            cn.OpenAsync().Wait();
            using var command = new SqlCommand(sps.SpGetAllProduct, cn);
            command.CommandType = CommandType.StoredProcedure;
            using var reader = await command.ExecuteReaderAsync();
            while (reader.ReadAsync().Result)
            {
                products.Add(new Product
                {
                    Id = (long)reader["Id"],
                    Name = (string)reader["Name"],
                    Description = (string)reader["Description"],
                    Presentation = (int)reader["Presentation"],
                    Price = (decimal)reader["Price"],
                    Status = (bool)reader["Status"],
                    CreatedBy = reader["CreatedBy"] != DBNull.Value ? (string)reader["CreatedBy"] : "",
                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? (DateTime)reader["CreatedDate"] : new DateTime(1900, 01, 01),
                    ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? (string)reader["ModifiedBy"] : "",
                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? (DateTime)reader["ModifiedDate"] : new DateTime(1900, 01, 01)
                });
            }

            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            ProductSps sps = new();
            var product = new Product();
            using var cn = Create();
            cn.OpenAsync().Wait();
            using var command = new SqlCommand(sps.SpGetProduct, cn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (reader.ReadAsync().Result)
            {
                product = new()
                {
                    Id = (long)reader["Id"],
                    Name = (string)reader["Name"],
                    Description = (string)reader["Description"],
                    Presentation = (int)reader["Presentation"],
                    Price = (decimal)reader["Price"],
                    Status = (bool)reader["Status"],
                    CreatedBy = reader["CreatedBy"] != DBNull.Value ? (string)reader["CreatedBy"] : "",
                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? (DateTime)reader["CreatedDate"] : new DateTime(1900, 01, 01),
                    ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? (string)reader["ModifiedBy"] : "",
                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? (DateTime)reader["ModifiedDate"] : new DateTime(1900, 01, 01)
                };
            }

            return product;
        }

        public async Task AddProduct(Product product)
        {
            ProductSps sps = new();
            using var cn = Create();
            cn.OpenAsync().Wait();
            using var command = new SqlCommand(sps.SpAddProduct, cn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Presentation", product.Presentation);
            command.Parameters.AddWithValue("@Price", product.Price);
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            ProductSps sps = new();
            using var cn = Create();
            cn.OpenAsync().Wait();
            using var command = new SqlCommand(sps.SpUpdateProduct, cn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", product.Id);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Presentation", product.Presentation);
            command.Parameters.AddWithValue("@Price", product.Price);
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeactivateProduct(int id)
        {
            ProductSps sps = new();
            using var cn = Create();
            cn.OpenAsync().Wait();
            using var command = new SqlCommand(sps.SpDeactivateProduct, cn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync();
        }
    }
}
