using DataAccess.DBContexts.HRMSDB;
using DataAccess.DBContexts.HRMSDB.Models;
using DataAccess.Repositories.Base;
using DataAccess.Repositories.HRMSDB.Interfaces;

namespace DataAccess.Repositories.HRMSDB
{
    public class EmployeeRoleRepository : BaseRepository<EmployeeRole>, IEmployeeRoleRepository
    {
        public EmployeeRoleRepository(HRMSDBContext context) : base(context)
        {

        }
    }
}