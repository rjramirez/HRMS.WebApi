using DataAccess.DBContexts.HRMSDB;
using DataAccess.DBContexts.HRMSDB.Models;
using DataAccess.Repositories.Base;
using DataAccess.Repositories.HRMSDB.Interfaces;

namespace DataAccess.Repositories.HRMSDB
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(HRMSDBContext context) : base(context)
        {

        }
    }
}