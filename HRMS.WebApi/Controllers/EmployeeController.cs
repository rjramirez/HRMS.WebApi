using Common.Constants;
using Common.DataTransferObjects.ErrorLog;
using Common.DataTransferObjects.ReferenceData;
using DataAccess.DBContexts.HRMSDB.Models;
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
        [SwaggerOperation(Summary = "Get Employee List")]
        public async Task<ActionResult<IEnumerable<EmployeeDetail>>> GetEmployees()
        {
            IEnumerable<EmployeeDetail> referenceDataDetails = await _hrmsDBUnitOfWork.EmployeeRepository.FindAsync(
                selector: e => new EmployeeDetail()
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeNumber = e.EmployeeNumber,
                    EmployeeEmail = e.EmployeeEmail,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    SupervisorId = e.SupervisorId,
                    Active = e.Active,
                    CreatedDate = e.CreatedDate,
                    CreatedBy = e.CreatedBy,
                    UpdatedDate = e.UpdatedDate,
                    UpdatedBy = e.UpdatedBy
                },
                predicate: r => r.Active == true,
                orderBy: r => r.OrderBy(o => o.EmployeeId));
            return Ok(referenceDataDetails);
        }


        [HttpGet]
        [Route("GetEmployee")]
        [SwaggerOperation(Summary = "Get Employee Details by EmployeeNumber")]
        public async Task<ActionResult<EmployeeDetail>> Detail(int employeeNumber)
        {
            EmployeeDetail emp = await _hrmsDBUnitOfWork.EmployeeRepository.SingleOrDefaultAsync(
                selector: e => new EmployeeDetail()
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeNumber = e.EmployeeNumber,
                    EmployeeEmail = e.EmployeeEmail,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    SupervisorId = e.SupervisorId,
                    Active = e.Active,
                    CreatedDate = e.CreatedDate,
                    CreatedBy = e.CreatedBy,
                    UpdatedDate = e.UpdatedDate,
                    UpdatedBy = e.UpdatedBy
                },
                predicate: r => r.Active && r.EmployeeNumber == employeeNumber
            );

            if (emp != null)
            {
                return Ok(emp);
            }
            else
            {
                return NotFound(new ErrorMessage(ErrorMessageTypeConstant.NotFound, $"Employee does not exist: {employeeNumber}"));
            }
        }

        [HttpPost]
        [Route("AddEmployee")]
        [SwaggerOperation(Summary = "Add Employee")]
        public async Task<ActionResult<EmployeeDetail>> AddEmployee(EmployeeDetail employeeDetail)
        {
            if (employeeDetail == null)
            {
                return BadRequest(new ErrorMessage(ErrorMessageTypeConstant.BadRequest, "EmployeeDetail is null"));
            }

            Employee employee = new Employee()
            {
                EmployeeNumber = employeeDetail.EmployeeNumber,
                EmployeeEmail = employeeDetail.EmployeeEmail,
                FirstName = employeeDetail.FirstName,
                LastName = employeeDetail.LastName,
                SupervisorId = employeeDetail.SupervisorId,
                Active = employeeDetail.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedDate = DateTime.UtcNow,
                UpdatedBy = "System"
            };

            await _hrmsDBUnitOfWork.EmployeeRepository.AddAsync(employee);
            await _hrmsDBUnitOfWork.SaveChangesAsync("System");
            return CreatedAtAction(nameof(Detail), new { employeeNumber = employee.EmployeeNumber }, employeeDetail);
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        [SwaggerOperation(Summary = "Update Employee by EmployeeNumber")]
        public async Task<ActionResult<EmployeeDetail>> UpdateEmployee(EmployeeDetail employeeDetail)
        {
            if (employeeDetail == null)
            {
                return BadRequest(new ErrorMessage(ErrorMessageTypeConstant.BadRequest, "EmployeeDetail is null"));
            }

            Employee employee = await _hrmsDBUnitOfWork.EmployeeRepository.FirstOrDefaultAsync(predicate: e => e.EmployeeNumber == employeeDetail.EmployeeNumber);

            if (employee == null)
            {
                return NotFound(new ErrorMessage(ErrorMessageTypeConstant.NotFound, $"Employee does not exist: {employeeDetail.EmployeeId}"));
            }
            employee.EmployeeNumber = employeeDetail.EmployeeNumber;
            employee.EmployeeEmail = employeeDetail.EmployeeEmail;
            employee.FirstName = employeeDetail.FirstName;
            employee.LastName = employeeDetail.LastName;
            employee.SupervisorId = employeeDetail.SupervisorId;
            employee.Active = employeeDetail.Active;
            employee.UpdatedDate = DateTime.UtcNow;
            employee.UpdatedBy = "System";

            await _hrmsDBUnitOfWork.SaveChangesAsync("System");

            return Ok(employeeDetail);
        }
    }
}
