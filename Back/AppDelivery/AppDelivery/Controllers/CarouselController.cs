using AppDelivery.Controllers.Dtos;
using AppDelivery.Model;
using Microsoft.AspNetCore.Mvc;

namespace AppDelivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarouselController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CarouselController> _logger;

        public CarouselController(ILogger<CarouselController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<CategoryDto> Get()
        {
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=bd";

            var db = new DbConnection(connectionString);
            List<Category> categories = db.GetAllCategories();
            var response = Map(categories);
            return response;
        }

        public static List<CategoryDto> Map(List<Category> categories)
        {
            var response = new List<CategoryDto>();

            // dicionário agora usa o título (case-insensitive)
            var imageUrls = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Lanches Saudáveis",    "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-saudavel-v2.jpg" },
                { "Lanches",     "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-lanches-v2.jpg" },
                { "Japonesa",    "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-japonesa.jpg" },
                { "Doces & Bolos","https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-doces-e-bolos.jpg" },
                { "Italiana",    "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-italiana.jpg" },
                { "Pizza",       "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-pizza.jpg" },
                { "Chinesa",     "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-chinesa.jpg" },
                { "Cafeterias",  "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-cafeteria.jpg" },
                { "Vegetariana", "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-vegetariana.jpg" },
                { "Pastel",      "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-pasteis.jpg" }
            };

            int generatedId = 1;

            foreach (var category in categories)
            {
                imageUrls.TryGetValue(category.Name, out var imageUrl);

                response.Add(new CategoryDto
                {
                    Id = generatedId.ToString(),
                    Title = category.Name,
                });

                generatedId++;
            }

            return response;
        }

    }
}
