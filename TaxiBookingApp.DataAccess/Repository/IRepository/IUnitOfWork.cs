using System;
using System.Collections.Generic;
using System.Text;
using TaxiBookingApp.DataAccess.Repository.IRepository;

namespace TaxiBookingApp.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserRepository ApplicationUserRepository { get; }
        IBookingRepository BookingRepository { get; }
        ISP_Call SP_Call { get; }

        void Save();
    }
}
