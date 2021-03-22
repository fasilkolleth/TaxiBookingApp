using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaxiBookingApp.DataAccess.Repository.IRepository;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TaxiBookingApp.Models;
using TaxiBookingApp.Utility;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TaxiBookingApp.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BookingController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ViewBag.Uid = claim.Value;

            return View();
        }

        public async Task<IActionResult> Upsert(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ViewBag.Uid = claim.Value;

            List<Booking> booking = new List<Booking>();
            string Baseurl = "http://localhost:45235/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetBookings using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Booking/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var ObjResponse = Res.Content.ReadAsStringAsync().Result;
                    booking = JsonConvert.DeserializeObject<List<Booking>>(ObjResponse);
                }
                
                if(booking.Count > 0)
                    return View(booking[0]);
                else
                    return View(new Booking());
            }
        }

    }
}
