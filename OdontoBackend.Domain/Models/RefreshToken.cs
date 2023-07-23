
using OdontoBackend.Domain.Models;

namespace OdontoBackend.Aplicacion.Entities
{
    public partial class RefreshToken
    {
        public Int64? Id { get; set; }
        public Int64? UserId { get; set; }
        public string? TokenHash { get; set; }
        public string? TokenSalt { get; set; }
        public DateTime Ts { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual User? User { get; set; }
    }
}
