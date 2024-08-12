using Entities.Concrete;

namespace Portfolyo.Models
{
    public class TestimonialListViewModel
    {
        public List<Testimonial> Testimonials { get; set; }
        public Testimonial Testimonial { get; set; }
        public IFormFile TestimonialImage { get; set; }
    }
}
