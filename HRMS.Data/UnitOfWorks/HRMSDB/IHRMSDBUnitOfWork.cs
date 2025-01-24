using DataAccess.Repositories.HRMSDB.Interfaces;
using DataAccess.UnitOfWork.Base;

namespace DataAccess.UnitOfWorks.HRMSDB
{
    public interface IHRMSDBUnitOfWork : IBaseUnitOfWork
    {
        public IErrorLogRepository ErrorLogRepository { get; }
        public IAuditTrailRepository AuditTrailRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IEmployeeRoleRepository EmployeeRoleRepository { get; }
    }
}
