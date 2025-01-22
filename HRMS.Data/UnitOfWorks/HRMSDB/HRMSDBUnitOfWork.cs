using Common.Constants;
using Common.DataTransferObjects.AuditTrail;
using DataAccess.DBContexts.HRMSDB;
using DataAccess.Repositories.HRMSDB;
using DataAccess.Repositories.HRMSDB.Interfaces;
using DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.UnitOfWorks.HRMSDB
{
    public sealed class HRMSDBUnitOfWork : IHRMSDBUnitOfWork
    {
        private readonly HRMSDBContext _context;
        private readonly IDbContextChangeTrackingService _dbContextChangeTrackingService;
        public HRMSDBUnitOfWork(HRMSDBContext context, IDbContextChangeTrackingService dbContextChangeTrackingService)
        {
            _context = context;
            _dbContextChangeTrackingService = dbContextChangeTrackingService;

            ErrorLogRepository = new ErrorLogRepository(_context);
            AuditTrailRepository = new AuditTrailRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
        }

        public IErrorLogRepository ErrorLogRepository { get; private set; }

        public IAuditTrailRepository AuditTrailRepository { get; private set; }
        public IEmployeeRepository EmployeeRepository { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync(string transactionBy)
        {
            List<Tuple<ContextChangeTrackingDetail, EntityEntry>> contextChangeTrackingDetail = _dbContextChangeTrackingService.TrackRevisionDetails(_context);
            int result = await _context.SaveChangesAsync();

            if (contextChangeTrackingDetail.Any())
            {
                await _dbContextChangeTrackingService.SaveAuditTrail(transactionBy, contextChangeTrackingDetail);
            }

            return result;
        }
    }
}
