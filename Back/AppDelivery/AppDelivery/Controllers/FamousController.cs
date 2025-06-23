using AppDelivery.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AppDelivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamousController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<FamousController> _logger;

        public FamousController(ILogger<FamousController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=bd";

            var result = new List<object>
            {
                new {
                    id = "1",
                    title = "Mcdonald's",
                    image_url = "https://static-images.ifood.com.br/image/upload/t_thumbnail/logosgde/201810231832_2b3b8c04-73fb-4f23-9c89-b75e538404c6.png",
                    category = "Lanches",
                    distance = 3.36,
                    start_time = 28,
                    end_time = 38,
                    rating = 4.9
                },
                new {
                    id = "2",
                    title = "Subway",
                    image_url = "https://static-images.ifood.com.br/image/upload/t_thumbnail/logosgde/201903142138_7ed60480-4895-4f67-ba3e-edf2366eadb3.png",
                    category = "Lanches",
                    distance = 1.15,
                    start_time = 15,
                    end_time = 25,
                    rating = 4.4
                },
                new {
                    id = "3",
                    title = "Burguer King",
                    image_url = "https://static-images.ifood.com.br/image/upload/t_thumbnail/logosgde/202009301458_d5736924-893e-42b2-89d1-ff37e58abc1e.jpg",
                    category = "Lanches",
                    distance = 3.3,
                    start_time = 38,
                    end_time = 48,
                    rating = 4.3
                },
                new {
                    id = "4",
                    title = "Sushi Boulevard",
                    image_url = "https://static-images.ifood.com.br/image/upload/t_thumbnail/logosgde/c48cbb89-0ddb-48ec-83cd-25946fe8c78f/201908131950_v914_i.png",
                    category = "Japonesa",
                    distance = 3.76,
                    start_time = 43,
                    end_time = 53,
                    rating = 4.8
                }
            };
            return Ok(result);
        }
    }
}
