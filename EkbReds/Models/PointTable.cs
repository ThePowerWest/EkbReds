using ApplicationCore.Entities.Identity;

namespace Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PointTable
    {
        public User User { get; set; }

        public byte Points { get; set; }
    }
}