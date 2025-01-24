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
            var referenceDataDetails = await _hrmsDBUnitOfWork.EmployeeRepository.FindAsync(
                selector: e => new EmployeeDetail
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeNumber = e.EmployeeNumber,
                    EmployeeEmail = e.EmployeeEmail,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    SupervisorId = e.SupervisorId,
                    EmployeeRoles = e.EmployeeRoles.Select(er => new EmployeeRoleDetail
                    {
                        EmployeeRoleId = er.EmployeeRoleId,
                        EmployeeId = er.EmployeeId,
                        RoleDetail = new RoleDetail
                        {
                            RoleId = er.Role.RoleId,
                            RoleName = er.Role.RoleName,
                            RoleDescription = er.Role.RoleDescription,
                            Active = er.Role.Active,
                            CreatedDate = er.Role.CreatedDate,
                            CreatedBy = er.Role.CreatedBy,
                            UpdatedDate = er.Role.UpdatedDate,
                            UpdatedBy = er.Role.UpdatedBy
                        },
                        CreatedDate = er.CreatedDate,
                        CreatedBy = er.CreatedBy,
                    }).ToList(),
                    Active = e.Active,
                    CreatedDate = e.CreatedDate,
                    CreatedBy = e.CreatedBy,
                    UpdatedDate = e.UpdatedDate,
                    UpdatedBy = e.UpdatedBy
                },
                predicate: r => r.Active,
                orderBy: r => r.OrderBy(o => o.EmployeeId));
            return Ok(referenceDataDetails);
        }

        [HttpGet]
        [Route("GetEmployee")]
        [SwaggerOperation(Summary = "Get Employee Details by EmployeeNumber")]
        public async Task<ActionResult<EmployeeDetail>> Detail(int employeeNumber)
        {
            var emp = await _hrmsDBUnitOfWork.EmployeeRepository.SingleOrDefaultAsync(
                selector: e => new EmployeeDetail
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeNumber = e.EmployeeNumber,
                    EmployeeEmail = e.EmployeeEmail,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    SupervisorId = e.SupervisorId,
                    EmployeeRoles = e.EmployeeRoles.Select(er => new EmployeeRoleDetail
                    {
                        EmployeeRoleId = er.EmployeeRoleId,
                        EmployeeId = er.EmployeeId,
                        RoleDetail = new RoleDetail
                        {
                            RoleId = er.Role.RoleId,
                            RoleName = er.Role.RoleName,
                            RoleDescription = er.Role.RoleDescription,
                            Active = er.Role.Active,
                            CreatedDate = er.Role.CreatedDate,
                            CreatedBy = er.Role.CreatedBy,
                            UpdatedDate = er.Role.UpdatedDate,
                            UpdatedBy = er.Role.UpdatedBy
                        },
                        CreatedDate = er.CreatedDate,
                        CreatedBy = er.CreatedBy,
                    }).ToList(),
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

        [HttpGet]
        [Route("GetEmployeeRoles")]
        [SwaggerOperation(Summary = "Get Employee Roles")]
        public async Task<ActionResult<IEnumerable<EmployeeRoleDetail>>> GetEmployeeRoles()
        {
            var employeeRoles = await _hrmsDBUnitOfWork.EmployeeRoleRepository.FindAsync(
                selector: e => new EmployeeRoleDetail
                {
                    EmployeeRoleId = e.EmployeeRoleId,
                    EmployeeId = e.EmployeeId,
                    RoleDetail = new RoleDetail
                    {
                        RoleId = e.Role.RoleId,
                        RoleName = e.Role.RoleName,
                        RoleDescription = e.Role.RoleDescription,
                        Active = e.Role.Active,
                        CreatedDate = e.Role.CreatedDate,
                        CreatedBy = e.Role.CreatedBy,
                        UpdatedDate = e.Role.UpdatedDate,
                        UpdatedBy = e.Role.UpdatedBy
                    },
                    CreatedDate = e.CreatedDate,
                    CreatedBy = e.CreatedBy,
                },
                predicate: r => r.Role.Active,
                orderBy: q => q.OrderBy(r => r.Role.RoleName));
            return Ok(employeeRoles);
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

            var employee = new Employee
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

            var employeeRoles = employeeDetail.EmployeeRoles.Select(role => new EmployeeRole
            {
                EmployeeId = employee.EmployeeId,
                RoleId = role.RoleDetail.RoleId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "System"
            });

            await _hrmsDBUnitOfWork.EmployeeRoleRepository.AddRangeAsync(employeeRoles);
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

            var employee = await _hrmsDBUnitOfWork.EmployeeRepository.FirstOrDefaultAsync(predicate: e => e.EmployeeNumber == employeeDetail.EmployeeNumber);

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

            var employeeRoles = employeeDetail.EmployeeRoles.Select(role => new EmployeeRole
            {
                EmployeeId = employee.EmployeeId,
                RoleId = role.RoleDetail.RoleId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "System"
            });

            await _hrmsDBUnitOfWork.EmployeeRoleRepository.AddRangeAsync(employeeRoles);
            await _hrmsDBUnitOfWork.SaveChangesAsync("System");

            return Ok(employeeDetail);
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        [SwaggerOperation(Summary = "Delete Employee by EmployeeNumber")]
        public async Task<ActionResult<int>> DeleteEmployee(int employeeNumber)
        {
            var employee = await _hrmsDBUnitOfWork.EmployeeRepository.FirstOrDefaultAsync(predicate: e => e.EmployeeNumber == employeeNumber);
            if (employee == null)
            {
                return NotFound(new ErrorMessage(ErrorMessageTypeConstant.NotFound, $"Employee does not exist: {employeeNumber}"));
            }

            employee.Active = false;
            employee.UpdatedDate = DateTime.UtcNow;
            employee.UpdatedBy = "System";

            await _hrmsDBUnitOfWork.SaveChangesAsync("System");

            return Ok(employee.EmployeeNumber);
        }
    }
}