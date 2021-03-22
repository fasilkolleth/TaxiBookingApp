using TaxiBookingApp.DataAccess.Data;
using TaxiBookingApp.DataAccess.Repository.IRepository;
using TaxiBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxiBookingApp.DataAccess.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Booking booking)
        {
            var objFromDb = _db.Bookings.FirstOrDefault(s => s.Id == booking.Id);
            if (objFromDb != null)
            {
                if (booking.EmiratesID != null)
                {
                    objFromDb.EmiratesID = booking.EmiratesID;
                }

                objFromDb.ApplicationUserID = booking.ApplicationUserID;
                objFromDb.BookingDate = booking.BookingDate;
                objFromDb.BookingTime = booking.BookingTime;
                objFromDb.BookingType = booking.BookingType;
                objFromDb.Latitude = booking.Latitude;
                objFromDb.Longitude = booking.Longitude;
                objFromDb.LocationDescription = booking.LocationDescription;
            }
        }
    }
}
