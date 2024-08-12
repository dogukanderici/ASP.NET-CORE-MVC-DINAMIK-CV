using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http;
using System.Xml.Linq;

namespace Portfolyo.Areas.User.Controllers
{
    [Area("User")]
    public class WriterDashboardController : Controller
    {
        private readonly UserManager<WriterUser> _userManager;
        private HttpClient _httpClient;
        private IMessageService _messageService;
        private ISkillService _skillService;
        private IAnnouncementService _announcementService;

        public WriterDashboardController(UserManager<WriterUser> userManager, HttpClient httpClient, IMessageService messageService, ISkillService skillService, IAnnouncementService announcementService)
        {

            _userManager = userManager;
            _httpClient = httpClient;
            _messageService = messageService;
            _skillService = skillService;
            _announcementService = announcementService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new WriterUser
            {
                Name = result.Name + ' ' + result.Surname
            };

            var messageResult = _messageService.TGetList(m => m.Receiver == result.Email).Data.Count();
            var skillResult = _skillService.TGetList().Data.Count();
            var announcementResult = _announcementService.TGetList().Data.Count();

            string apiKey = "your_api_key";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=eskisehir,tr&lang=tr&units=metric&appid=" + apiKey;

            // Dönen veri tipi xml ise
            //XDocument document = XDocument.Load(connection);

            ViewBag.incomingMessages = messageResult;
            ViewBag.announcemnets = announcementResult;
            ViewBag.userCount = 0;
            ViewBag.skillCount = skillResult;
            // Dönen veri tipi xml ise
            //ViewBag.weatherResult = document.Descendants("tempature").ElementAt(0).Attribute("value").Value;
            WeatherAPIResult weatherAPIResult = await _httpClient.GetFromJsonAsync<WeatherAPIResult>(connection);
            ViewBag.weatherResult = Convert.ToInt32(weatherAPIResult.Main.feels_like);

            return View(model);
        }
    }
    public class main
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public int sea_level { get; set; }
        public int grnd_level { get; set; }
    }

    public class WeatherAPIResult
    {
        public main Main { get; set; }
    }
}
