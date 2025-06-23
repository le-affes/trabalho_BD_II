using AppDelivery.Model;
using Microsoft.AspNetCore.Mvc;

namespace AppDelivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly ILogger<RestaurantsController> _logger;

        public RestaurantsController(ILogger<RestaurantsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(double latitude, double longitude, string? category, int pageSize = 30)
        {
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=bd";

            var db = new DbConnection(connectionString);
            List<Restaurant> result = db.GetRestaurants(latitude, longitude, category, pageSize);
            var response = Map(result, category);

            return Ok(response);
        }

        public static List<RestaurantDto> Map(List<Restaurant> restaurants, string? category)
        {
            if (!String.IsNullOrEmpty(category))
                restaurants = restaurants.Where(r => r.Category.ToString().Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

            var result = restaurants
                .Select(restaurant => new RestaurantDto
                {
                    Title = restaurant.Title,
                    Category = restaurant.Category.ToString(),
                    Distance = restaurant.Distance,
                    StartTime = restaurant.StartTime,
                    EndTime = restaurant.EndTime
                })
                .OrderBy(r => r.Distance)
                .ToList();
            return result;
        }
    }
}
