using DataAccess.DBContexts.HRMSDB;
using DataAccess.DBContexts.HRMSDB.Models;
using DataAccess.Repositories.Base;
using DataAccess.Repositories.HRMSDB.Interfaces;

namespace DataAccess.Repositories.HRMSDB
{
    public class ErrorLogRepository : BaseRepository<ErrorLog>, IErrorLogRepository
    {
        public ErrorLogRepository(HRMSDBContext context) : base(context)
        {

        }
    }
}