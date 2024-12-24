using car_management_backend.DTOs.GarageDTOs;
using car_management_backend.Models;

namespace car_management_backend.Queries
{
    public class CarQueries
    {
        public string Make { get; set; } = string.Empty;
        public GarageDTOInFilterQuery? Garage { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }

    }
}
