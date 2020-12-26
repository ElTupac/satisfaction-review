using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using satisfaction_review.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace satisfaction_review.Controllers
{
    public class HomeController : Controller
    {
        private readonly ReviewContext db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ReviewContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public string AddReview(string review)
        {
            if(review == "good" || review == "bad" || review == "soso"){
                Review reviewObj = new Review{
                    Cualification = review
                };
                db.Reviews.Add(reviewObj);
                db.SaveChanges();
                List<Review> reviews = db.Reviews.ToList();
                
                return $@"{{""ok"": true, ""proms"": {{ {getProm(reviews)} }} }}";
                //Regresar el porcentaje de ratings
            }else{
                return @"{""ok"":false}";
            }
        }

        private string getProm(List<Review> reviews){
            int good = 0, soso = 0, bad = 0;
            foreach(Review review in reviews){
                switch(review.Cualification){
                    case "good":
                    good++;
                    break;
                    case "soso":
                    soso++;
                    break;
                    case "bad":
                    bad++;
                    break;
                }
            }

            return $@"""good"": {good}, ""soso"": {soso}, ""bad"": {bad}";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
