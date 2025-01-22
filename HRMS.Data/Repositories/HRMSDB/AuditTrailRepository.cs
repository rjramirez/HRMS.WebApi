using DataAccess.DBContexts.HRMSDB;
using DataAccess.DBContexts.HRMSDB.Models;
using DataAccess.Repositories.Base;
using DataAccess.Repositories.HRMSDB.Interfaces;

namespace DataAccess.Repositories.HRMSDB
{
    public class AuditTrailRepository : BaseRepository<AuditTrail>, IAuditTrailRepository
    {
        public AuditTrailRepository(HRMSDBContext context) : base(context)
        {

        }
    }
}
