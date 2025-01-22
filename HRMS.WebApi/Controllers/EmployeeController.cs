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
        [Route("GetBlackList")]
        [SwaggerOperation(Summary = "Get BlackList")]
        public async Task<ActionResult<IEnumerable<EmployeeDetail>>> GetBlackList()
        {
            IEnumerable<EmployeeDetail> referenceDataDetails = await _hrmsDBUnitOfWork.EmployeeRepository.FindAsync(
                selector: e => new EmployeeDetail()
                {
                    EmployeeId = e.EmployeeId,
                },
                predicate: r => r.Active == true,
                orderBy: r => r.OrderBy(o => o.EmployeeId));
            return Ok(referenceDataDetails);
        }
    }
}
