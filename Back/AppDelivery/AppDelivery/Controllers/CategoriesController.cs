using AppDelivery.Controllers.Dtos;
using AppDelivery.Model;
using Microsoft.AspNetCore.Mvc;

namespace AppDelivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger)
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
            //var result = new List<object>
            //{
            //    new {
            //        id = "1",
            //        title = "Mercado",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/groc_icon_alegre.png"
            //    },
            //    new {
            //        id = "2",
            //        title = "Promoções",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/Promo_cat.png"
            //    },
            //    new {
            //        id = "3",
            //        title = "Vale Refeição",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/20C1_VR_Test_AB.png"
            //    },
            //    new {
            //        id = "4",
            //        title = "Brasileira",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-brasileira-v2.jpg"
            //    },
            //    new {
            //        id = "5",
            //        title = "Saudável",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-saudavel-v2.jpg"
            //    },
            //    new {
            //        id = "7",
            //        title = "Lanches",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-lanches-v2.jpg"
            //    },
            //    new {
            //        id = "8",
            //        title = "Japonesa",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-japonesa.jpg"
            //    },
            //    new {
            //        id = "9",
            //        title = "Doces & Bolos",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-doces-e-bolos.jpg"
            //    },
            //    new {
            //        id = "10",
            //        title = "Italiana",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-italiana.jpg"
            //    },
            //    new {
            //        id = "12",
            //        title = "Pizza",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-pizza.jpg"
            //    },
            //    new {
            //        id = "13",
            //        title = "Chinesa",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-chinesa.jpg"
            //    },
            //    new {
            //        id = "14",
            //        title = "Cafeterias",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-cafeteria.jpg"
            //    },
            //    new {
            //        id = "15",
            //        title = "Vegetariana",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-vegetariana.jpg"
            //    },
            //    new {
            //        id = "16",
            //        title = "Pastel",
            //        image_url = "https://static-images.ifood.com.br/image/upload/t_low/discoveries/19C1-pasteis.jpg"
            //    }
            //};

            //return Ok(result);
        }

        public static List<CategoryDto> Map(List<Category> categories)
        {
            var response = new List<CategoryDto>();
            int generatedId = 1;

            foreach (var category in categories)
            {

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
