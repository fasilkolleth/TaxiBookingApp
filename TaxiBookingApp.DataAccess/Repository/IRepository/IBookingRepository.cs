using TaxiBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiBookingApp.DataAccess.Repository.IRepository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking booking);
    }
}
