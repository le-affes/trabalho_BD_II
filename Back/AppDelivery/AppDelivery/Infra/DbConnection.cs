using AppDelivery.Model;
using Npgsql;

public class DbConnection
{
    private readonly string _connectionString;

    public DbConnection(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Category> GetAllCategories()
    {
        var categories = new List<Category>();
        var sql = @"
                    SELECT c.category_id, c.name, c.created_at, c.updated_at, COUNT(m.merchant_id)
                    FROM public.category c
                    LEFT JOIN public.merchant_category mc on mc.category_id = c.category_id
                    LEFT JOIN public.merchant m on m.merchant_id = mc.merchant_id
                    group by c.category_id, c.name, c.created_at, c.updated_at
                    order by COUNT(m.merchant_id) DESC
                    ";

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var cmd = new NpgsqlCommand(sql, connection);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var category = new Category
            {
                CategoryId = reader.GetGuid(reader.GetOrdinal("category_id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
            };

            categories.Add(category);
        }

        return categories;
    }

    public List<Restaurant> GetRestaurants(double latitude, double longitude, string? categoria, int? pageSize)
    {
        var sql = @"
            select m.name as merchant_name, a.latitude, a.longitude, c.name as category, oh.opening_time, oh.closing_time
            from public.merchant m 
            inner join public.address a on m.address_id = a.address_id
            inner join public.merchant_category mc on m.merchant_id = mc.merchant_id
            inner join public.category c on mc.category_id = c.category_id
            inner join public.default_operating_hours oh on oh.merchant_id = m.merchant_id
            where m.name is not null
        ";
        if (!string.IsNullOrEmpty(categoria))
            sql += $" and c.name = '{categoria}'";

        sql += $"LIMIT {pageSize}";
        var restaurants = new List<Restaurant>();

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var cmd = new NpgsqlCommand(sql, connection);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            double restaurantLatitude = reader.GetDouble(reader.GetOrdinal("latitude"));
            double restaurantLongitude = reader.GetDouble(reader.GetOrdinal("longitude"));

            var distance = CalculateDistance(latitude, longitude, restaurantLatitude, restaurantLongitude);

            int openingTimeIndex = reader.GetOrdinal("opening_time");
            int closingTimeIndex = reader.GetOrdinal("closing_time");

            var startTime = !reader.IsDBNull(openingTimeIndex) ? reader.GetDateTime(openingTimeIndex) : default;
            var endTime = !reader.IsDBNull(closingTimeIndex) ? reader.GetDateTime(closingTimeIndex) : default;

            var restaurant = new Restaurant
            {
                Title = reader.GetString(reader.GetOrdinal("merchant_name")),
                Category = reader.GetString(reader.GetOrdinal("category")),
                Distance = distance,
                StartTime = startTime,
                EndTime = endTime
            };

            restaurants.Add(restaurant);
        }

        return restaurants;
    }

    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double EarthRadiusKm = 6371;

        double dLat = ToRadians(lat2 - lat1);
        double dLon = ToRadians(lon2 - lon1);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = EarthRadiusKm * c;

        return distance; // distância em quilômetros
    }

    private double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}