using TaxiBookingApp.DataAccess.Data;
using TaxiBookingApp.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiBookingApp.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUserRepository = new ApplicationUserRepository(_db);
            BookingRepository = new BookingRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public ISP_Call SP_Call { get; private set; }
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }
        public IBookingRepository BookingRepository { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
