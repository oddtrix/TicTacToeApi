using Domain.Entities;

namespace Domain.DTOs.Services
{
    public class PaginationDTO
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<GamePlayerJunction> Items { get; set; } = null;
    }
}
