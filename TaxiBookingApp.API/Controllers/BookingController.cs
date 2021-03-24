using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxiBookingApp.DataAccess.Repository.IRepository;
using TaxiBookingApp.Models;

namespace TaxiBookingApp.API.Controllers
{

    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get(string type)
        {
            IEnumerable<Booking> bookings;

            if (type == "upcoming")
                bookings = _unitOfWork.BookingRepository.GetAll(includeProperties: "ApplicationUser").Where(u => u.BookingDate >= DateTime.Now);
            else if (type == "previous")
                bookings = _unitOfWork.BookingRepository.GetAll(includeProperties: "ApplicationUser").Where(u => u.BookingDate < DateTime.Now);
            else
                bookings = _unitOfWork.BookingRepository.GetAll(includeProperties: "ApplicationUser");

            return Ok(bookings);
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult Get(int Id)
        {
            IEnumerable<Booking> bookings;

            bookings = _unitOfWork.BookingRepository.GetAll()
                .Where(u => u.Id == Id);

            return Ok(bookings);
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetByUser(string userId, string type)
        {
            IEnumerable<Booking> bookings;

            if (type == "upcoming")
                bookings = _unitOfWork.BookingRepository.GetAll(includeProperties: "ApplicationUser")
                    .Where(u => u.ApplicationUserID == userId && u.BookingDate >= DateTime.Now);

            else if (type == "previous")
                bookings = _unitOfWork.BookingRepository.GetAll(includeProperties: "ApplicationUser")
                    .Where(u => u.ApplicationUserID == userId && u.BookingDate < DateTime.Now);

            else
                bookings = _unitOfWork.BookingRepository.GetAll(includeProperties: "ApplicationUser")
                    .Where(u => u.ApplicationUserID == userId);

            return Ok(bookings);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            var booking = _unitOfWork.BookingRepository.Get(id);

            if (booking != null)
            {
                _unitOfWork.BookingRepository.Remove(booking);
                _unitOfWork.Save();
                return Ok("Deleted");
            }

            return NotFound($"booking with Id {id} was not found!");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult Insert([FromBody] Booking booking)
        {
            if (ModelState.IsValid)
            {

                string BookingDateStr = booking.BookingDate.ToString("dd/MM/yyyy");
                string BookingTimeStr = booking.BookingTime;
                DateTime BookingDateWithTime = DateTime.ParseExact(BookingDateStr + " " + BookingTimeStr, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                booking.BookingDate = BookingDateWithTime;

                _unitOfWork.BookingRepository.Add(booking);

                _unitOfWork.Save();

                return Ok(booking.Id);
            }

            return Ok(0);
        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        public IActionResult UpdateBooking(int id, [FromBody] Booking booking)
        {
            try
            {
                string BookingDateStr = booking.BookingDate.ToString("dd/MM/yyyy");
                string BookingTimeStr = booking.BookingTime;
                DateTime BookingDateWithTime = DateTime.ParseExact(BookingDateStr + " " + BookingTimeStr, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                booking.BookingDate = BookingDateWithTime;

                var bookingFromDB = _unitOfWork.BookingRepository.Get(id);

                if (bookingFromDB == null)
                    return NotFound($"Booking with Id = {id} not found");


                booking.Id = id;
                _unitOfWork.BookingRepository.Update(booking);
                _unitOfWork.Save();

                return Ok(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

    }
}
