using Entities.Concrete;

namespace Portfolyo.Models
{
    public class PortfolioListViewModel
    {
        public List<Portfolio> Portfolios { get; set; }
        public Portfolio Portfolio { get; set; }
        public IFormFile PlatformImage { get; set; }
        public IFormFile ProjectImage { get; set; }
        public IFormFile ProjectImage2 { get; set; }
        public IFormFile Image1 { get; set; }
        public IFormFile Image2 { get; set; }
        public IFormFile Image3 { get; set; }
        public IFormFile Image4 { get; set; }
    }
}
