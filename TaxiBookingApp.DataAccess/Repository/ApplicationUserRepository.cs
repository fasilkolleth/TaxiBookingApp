using TaxiBookingApp.DataAccess.Data;
using TaxiBookingApp.DataAccess.Repository.IRepository;
using TaxiBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxiBookingApp.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
