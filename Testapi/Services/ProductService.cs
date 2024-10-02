using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Testapi.Model;
namespace Testapi.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> GetAsync() =>
            await _products.Find(p => true).ToListAsync();

        public async Task<Product> GetAsync(string id) =>
            await _products.Find<Product>(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateAsync(string id, Product product) =>
            await _products.ReplaceOneAsync(p => p.Id == id, product);

        public async Task DeleteAsync(string id) =>
            await _products.DeleteOneAsync(p => p.Id == id);
    }
}
