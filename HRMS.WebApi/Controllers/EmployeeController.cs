using Common.DataTransferObjects.ReferenceData;
using DataAccess.UnitOfWorks.HRMSDB;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IHRMSDBUnitOfWork _hrmsDBUnitOfWork;
        public EmployeeController(IHRMSDBUnitOfWork hrmsDBUnitOfWork)
        {
            _hrmsDBUnitOfWork = hrmsDBUnitOfWork;
        }

        [HttpGet]
        [Route("GetEmployees")]
        [SwaggerOperation(Summary = "Get Employees")]
        public async Task<ActionResult<IEnumerable<EmployeeDetail>>> GetEmployees()
        {
            IEnumerable<EmployeeDetail> referenceDataDetails = await _hrmsDBUnitOfWork.EmployeeRepository.FindAsync(
                selector: e => new EmployeeDetail()
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeEmail = e.EmployeeEmail,
                },
                predicate: r => r.Active == true,
                orderBy: r => r.OrderBy(o => o.EmployeeId));
            return Ok(referenceDataDetails);
        }
    }
}
