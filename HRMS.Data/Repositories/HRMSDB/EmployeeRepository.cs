using DataAccess.DBContexts.HRMSDB;
using DataAccess.DBContexts.HRMSDB.Models;
using DataAccess.Repositories.Base;
using DataAccess.Repositories.HRMSDB.Interfaces;

namespace DataAccess.Repositories.HRMSDB
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HRMSDBContext context) : base(context)
        {

        }
    }
}