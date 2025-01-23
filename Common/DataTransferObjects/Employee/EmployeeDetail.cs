namespace Common.DataTransferObjects.ReferenceData
{
    public class EmployeeDetail
    {
        public int EmployeeId { get; set; }
        public int EmployeeNumber { get; set; }
        public string EmployeeEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SupervisorId { get; set; }
        public IEnumerable<EmployeeRoleDetail> EmployeeRoles { get; set; }

        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }
}