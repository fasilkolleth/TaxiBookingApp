using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiBookingApp.DataAccess.Repository.IRepository
{
    public interface ISP_Call : IDisposable
    {
        T Single<T>(string StoredProcedure, DynamicParameters param = null);

        void Execute(string StoredProcedure, DynamicParameters param = null);

        T OneRecord<T>(string StoredProcedure, DynamicParameters param = null);

        IEnumerable<T> List<T>(string StoredProcedure, DynamicParameters param = null);

        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string StoredProcedure, DynamicParameters param = null);


    }
}
