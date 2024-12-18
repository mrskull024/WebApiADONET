using Microsoft.Data.SqlClient;

namespace TestingApiADONET.Services
{
    public class DbFactory
    {
        private readonly string _connectionString;

        public DbFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbCn")!;
        }

        public SqlConnection Create() => new(_connectionString);
    }

    public class ProductSps
    {
        private const string _spGetAllProducts = "sp_GetAllProducts";
        private const string _spGetProduct = "sp_GetProduct";
        private const string _spAddProduct = "sp_AddProduct";
        private const string _spUpdateProduct = "sp_UpdateProduct";
        private const string _spDeactivateProduct = "sp_DeactivateProduct";

        public string SpGetAllProduct => _spGetAllProducts;
        public string SpGetProduct => _spGetProduct;
        public string SpAddProduct => _spAddProduct;
        public string SpUpdateProduct => _spUpdateProduct;
        public string SpDeactivateProduct => _spDeactivateProduct;
    }
}
